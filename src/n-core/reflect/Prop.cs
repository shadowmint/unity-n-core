using System;
using System.Reflection;

namespace N.Package.Core.Reflect
{
  /// A simple interface into a property or field type
  public class Prop
  {
    /// The field type
    public System.Type FieldType { get; private set; }

    /// Associated type
    public System.Type Type { get; private set; }

    /// Property info if any
    private readonly PropertyInfo _pinfo;

    private readonly FieldInfo _finfo;

    /// Cache the last generic method we made
    private MethodInfo _genericGet = null;

    private MethodInfo _genericSet = null;
    private Prop _genericBinder = null;

    /// Create a new instance from a field
    public Prop(System.Type typeRef, FieldInfo field)
    {
      this.Type = typeRef;
      FieldType = field.FieldType;
      _finfo = field;
      _pinfo = null;
    }

    /// Create a new instance from a property
    public Prop(System.Type typeRef, PropertyInfo prop)
    {
      this.Type = typeRef;
      FieldType = prop.PropertyType;
      _pinfo = prop;
      _finfo = null;
    }

    /// Get the value from a target
    public T Get<T>(object target)
    {
      if (Type.IsInstanceOfType(target))
      {
        if (_finfo != null)
        {
          return (T) _finfo.GetValue(target);
        }
        if (_pinfo != null)
        {
          return (T) _pinfo.GetValue(target, null);
        }
      }
      return default(T);
    }

    /// Check if the value here is null or not
    public bool IsNull(object instance)
    {
      if (IsNullable)
      {
        return Get<object>(instance) == null;
      }
      return false;
    }

    /// Get the value from a target
    public T[] Array<T>(object target)
    {
      if (IsArray)
      {
        if (_finfo != null)
        {
          return (T[]) _finfo.GetValue(target);
        }
        if (_pinfo != null)
        {
          return (T[]) _pinfo.GetValue(target, null);
        }
        return new T[] {};
      }
      return null;
    }

    /// Set the value on a target
    public bool Set<T>(object target, T value)
    {
      if (Type.IsInstanceOfType(target))
      {
        if (_finfo != null)
        {
          _finfo.SetValue(target, value);
          return true;
        }
        if (_pinfo != null)
        {
          if (_pinfo.CanWrite && _pinfo.GetSetMethod( /*nonPublic*/ true).IsPublic)
          {
            _pinfo.SetValue(target, value, null);
            return true;
          }
        }
      }
      return false;
    }

    /// Rebind the value of this property on source to targetProp on target
    public bool Bind(object source, Prop targetProp, object target)
    {
      if (FieldType == targetProp.FieldType)
      {
        GenerateBinder(targetProp);
        var value = _genericGet.Invoke(this, new[] {source});
        _genericSet.Invoke(targetProp, new[] {target, value});
        return true;
      }
      Console.Log(string.Format("Property binding {0} to {1} is not valid", FieldType, targetProp.FieldType));
      return false;
    }

    /// Generate a new generic method for target
    private void GenerateBinder(Prop target)
    {
      if (_genericBinder == target)
      {
        return;
      }
      _genericBinder = target;

      // Get
      var method = GetType().GetMethod("Get");
      _genericGet = method.MakeGenericMethod(FieldType);

      // Set
      method = target.GetType().GetMethod("Set");
      _genericSet = method.MakeGenericMethod(FieldType);
    }

    /// Return true if this property is an array
    public bool IsArray
    {
      get
      {
        if (_pinfo != null)
        {
          return _pinfo.PropertyType.IsArray;
        }
        if (_finfo != null)
        {
          return _finfo.FieldType.IsArray;
        }
        return false;
      }
    }

    /// Check if this property is a nullable type
    public bool IsNullable
    {
      get
      {
        if (_pinfo != null)
        {
          return !_pinfo.PropertyType.IsValueType ||
                 _pinfo.PropertyType.IsGenericType && _pinfo.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>);
        }
        if (_finfo != null)
        {
          return !_finfo.FieldType.IsValueType ||
                 _finfo.FieldType.IsGenericType && _finfo.FieldType.GetGenericTypeDefinition() == typeof(Nullable<>);
        }
        return false;
      }
    }

    /// Return the name of the property
    public string Name
    {
      get
      {
        if (_pinfo != null)
        {
          return _pinfo.Name;
        }
        return _finfo.Name;
      }
    }
  }
}