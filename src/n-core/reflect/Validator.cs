using System.Collections.Generic;

namespace N.Package.Core.Reflect
{
  /// Applies validation rules to an object.
  /// By default some simple validators are applied.
  public class Validator
  {
    /// The set of validators to run
    private List<PropertyValidator> validators = new List<PropertyValidator>();

    /// Set of property names to globally ignore
    private List<string> ignored = new List<string>();

    /// Set of property types to globally ignore
    private List<System.Type> ignoredTypes = new List<System.Type>();

    /// Create a new validator and assign the default validators to it
    public Validator()
    {
      Add(new NullValidator());
      Add(new ArrayValidator());
    }

    /// Add a property name to ignore
    public Validator IgnoreProperty(string propertyName)
    {
      ignored.Add(propertyName);
      return this;
    }

    /// Add a property type to ignore
    public Validator IgnoreType(System.Type type)
    {
      ignoredTypes.Add(type);
      return this;
    }

    /// Add a property type to ignore
    public Validator IgnoreType<T>()
    {
      ignoredTypes.Add(typeof(T));
      return this;
    }

    /// A a new validator
    public Validator Add(PropertyValidator validator)
    {
      this.validators.Add(validator);
      return this;
    }

    /// Clear all validators
    public Validator Clear()
    {
      this.validators.Clear();
      return this;
    }

    /// Validate an object and return a set of validation errors on failure
    public Result<bool, ValidationError[]> Validate(object instance, string parent = "")
    {
      var errors = new List<ValidationError>();
      foreach (var validator in this.validators)
      {
        var fields = Type.Fields(instance.GetType());
        for (var i = 0; i < fields.Length; ++i)
        {
          if (!ignored.Contains(fields[i]))
          {
            var prop = Type.Field(instance.GetType(), fields[i]).Unwrap();
            if (!ignoredTypes.Contains(prop.FieldType))
            {
              // Console.Log("{0}: Validate field: {1}.{2} of type {3}", validator, parent, fields[i], prop.FieldType);
              var valid = validator.Validate(this, parent, prop, instance);
              if (valid.IsErr)
              {
                errors.AddRange(valid.Err.Unwrap());
              }
            }
          }
        }
      }
      if (errors.Count > 0)
      {
        return Result.Err<bool, ValidationError[]>(errors.ToArray());
      }
      return Result.Ok<bool, ValidationError[]>(true);
    }
  }

  /// A validation error
  public class ValidationError
  {
    /// The property that was invalid
    public string name;

    /// The problem with this propery
    public string error;
  }

  /// Generic interface for validation tests
  public abstract class PropertyValidator
  {
    /// Set of errors
    protected List<ValidationError> errors = new List<ValidationError>();

    /// Process the given property and return a set of validation errors if it fails.
    /// @param validator The top level validator, for recursive validation
    /// @param parent The name of the parent property or ''
    /// @param prop The property to validate
    /// @param instance The object instance to validate
    public abstract Result<bool, ValidationError[]> Validate(Validator validator, string parent, Prop prop, object instance);

    /// Add an error
    protected void Fail(string parent, string prop, string msg)
    {
      errors.Add(new ValidationError
      {
        name = parent == "" ? prop : string.Format("{0}.{1}", parent, prop),
        error = msg
      });
    }

    /// Return the appropriate result type and reset the errors
    public Result<bool, ValidationError[]> Errors()
    {
      var rtn = errors.Count > 0
        ? Result.Err<bool, ValidationError[]>(errors.ToArray())
        : Result.Ok<bool, ValidationError[]>(true);
      errors.Clear();
      return rtn;
    }
  }

  /// Return an error if the property is nullable and is null
  public class NullValidator : PropertyValidator
  {
    public override Result<bool, ValidationError[]> Validate(Validator validator, string parent, Prop prop, object instance)
    {
      if (prop.IsNullable && prop.IsNull(instance))
      {
        Fail(parent, prop.Name, "null value");
      }
      return Errors();
    }
  }

  /// If item is an array, returns errors if any elements of the array are invalid
  public class ArrayValidator : PropertyValidator
  {
    public override Result<bool, ValidationError[]> Validate(Validator validator, string parent, Prop prop, object instance)
    {
      if (prop.IsArray)
      {
        if (!prop.IsNull(instance))
        {
          var items = prop.Array<object>(instance);
          for (var j = 0; j < items.Length; ++j)
          {
            var name = parent == ""
              ? string.Format("{0}[{1}]", prop.Name, j)
              : string.Format("{0}.{1}[{2}]", parent, prop.Name, j);
            if (items[j] == null)
            {
              Fail(parent, string.Format("{0}[{1}]", prop.Name, j), "null array value");
            }
            else
            {
              var result = validator.Validate(items[j], name);
              if (result.IsErr)
              {
                var errors = result.Err.Unwrap();
                foreach (var err in errors)
                {
                  this.errors.Add(err);
                }
              }
            }
          }
        }
      }
      return Errors();
    }
  }
}