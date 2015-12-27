using System;
using System.Reflection;
using System.Collections.Generic;
using N.Tests;
using N;

namespace N.Reflect {

  /// A simple interface into a property or field type
  public class Prop {

    /// The field type
    public System.Type FieldType { get { return fieldType; } }
    private System.Type fieldType;

    /// Associated type
    public System.Type Type { get { return typeRef; } }
    private System.Type typeRef;

    /// Property info if any
    private PropertyInfo pinfo;
    private FieldInfo finfo;

    /// Cache the last generic method we made
    private MethodInfo genericGet = null;
    private MethodInfo genericSet = null;
    private Prop genericBinder = null;

    /// Create a new instance from a field
    public Prop(System.Type typeRef,  FieldInfo field) {
      this.typeRef = typeRef;
      fieldType = field.FieldType;
      finfo = field;
      pinfo = null;
    }

    /// Create a new instance from a property
    public Prop(System.Type typeRef, PropertyInfo prop) {
      this.typeRef = typeRef;
      fieldType = prop.PropertyType;
      pinfo = prop;
      finfo = null;
    }

    /// Get the value from a target
    public T Get<T>(object target) {
      if (Type.IsAssignableFrom(target.GetType())) {
        if (finfo != null) {
          return (T) finfo.GetValue(target);
        }
        if (pinfo != null) {
          return (T) pinfo.GetValue(target, null);
        }
      }
      return default(T);
    }

    /// Check if the value here is null or not
    public bool IsNull(object instance) {
      if (IsNullable) {
        return Get<object>(instance) == null;
      }
      return false;
    }

    /// Get the value from a target
    public T[] Array<T>(object target) {
      if (IsArray) {
        if (finfo != null) {
          return (T[]) finfo.GetValue(target);
        }
        if (pinfo != null) {
          return (T[]) pinfo.GetValue(target, null);
        }
        return new T[] {};
      }
      return null;
    }

    /// Set the value on a target
    public bool Set<T>(object target, T value) {
      if (Type.IsAssignableFrom(target.GetType())) {
        if (finfo != null) {
          finfo.SetValue(target, value);
          return true;
        }
        if (pinfo != null) {
          if (pinfo.CanWrite && pinfo.GetSetMethod(/*nonPublic*/ true).IsPublic) {
            pinfo.SetValue(target, value, null);
            return true;
          }
        }
      }
      return false;
    }

    /// Rebind the value of this property on source to targetProp on target
    public bool Bind(object source, Prop targetProp, object target) {
      if (FieldType == targetProp.FieldType) {
        GenerateBinder(targetProp);
        var value = genericGet.Invoke(this, new object[] { source });
        genericSet.Invoke(targetProp, new object[] { target, value });
        return true;
      }
      else {
        N.Console.Log(string.Format("Property binding {0} to {1} is not valid", FieldType, targetProp.FieldType));
      }
      return false;
    }

    /// Generate a new generic method for target
    private void GenerateBinder(Prop target) {
      if (genericBinder == target) {
        return;
      }
      genericBinder = target;

      // Get
      var method = this.GetType().GetMethod("Get");
      genericGet = method.MakeGenericMethod(FieldType);

      // Set
      method = target.GetType().GetMethod("Set");
      genericSet = method.MakeGenericMethod(FieldType);
    }

    /// Return true if this property is an array
    public bool IsArray {
      get {
        if (pinfo != null) {
          return pinfo.PropertyType.IsArray;
        }
        if (finfo != null) {
          return finfo.FieldType.IsArray;
        }
        return false;
      }
    }

    /// Check if this property is a nullable type
    public bool IsNullable {
      get {
        if (pinfo != null) {
          return !pinfo.PropertyType.IsValueType ||
                  pinfo.PropertyType.IsGenericType && pinfo.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>);
        }
        if (finfo != null) {
          return !finfo.FieldType.IsValueType ||
                  finfo.FieldType.IsGenericType && finfo.FieldType.GetGenericTypeDefinition() == typeof(Nullable<>);
        }
        return false;
      }
    }

    /// Return the name of the property
    public string Name {
      get {
        if (pinfo != null) {
          return pinfo.Name;
        }
        return finfo.Name;
      }
    }
  }
}
