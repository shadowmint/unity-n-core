using System;

namespace N.Package.Core
{
  /// Type of the internal data
  public enum OptionType
  {
    None,
    Some
  }

  /// Basic result type
  public struct Option<T>
  {
    /// Actual values
    private OptionType _type;

    private T _value;

    /// Create a new instance with value
    public Option(T value)
    {
      _value = value;
      _type = OptionType.Some;
    }

    /// Check if the value in this option is Some or None
    public bool IsSome
    {
      get { return _type == OptionType.Some; }
    }

    /// Check if the value in this option is Some or None
    public bool IsNone
    {
      get { return _type == OptionType.None; }
    }

    /// Bool operator for option type
    public static bool operator true(Option<T> v)
    {
      return v.IsSome;
    }

    public static bool operator false(Option<T> v)
    {
      return v.IsNone;
    }

    /// Run some callback if the value is valid
    public void Then(Action<T> someHandler)
    {
      Then(someHandler, null);
    }

    /// Run some callback if the value is valid
    public void Then(Action<T> someHandler, Action noneHandler)
    {
      if (this)
      {
        if (someHandler != null)
        {
          someHandler(_value);
        }
      }
      else
      {
        if (noneHandler != null)
        {
          noneHandler();
        }
      }
    }

    /// Get the inner value, panic if it is None
    public T Unwrap()
    {
      if (this)
      {
        return _value;
      }
      throw new Exception("Invalid attempt to dereference empty Option");
    }

    /// Take the value in this option, leaving it as None
    public T Take()
    {
      if (this)
      {
        _type = OptionType.None;
        var rtn = _value;
        _value = default(T);
        return rtn;
      }
      throw new Exception("Invalid attempt to dereference empty Option");
    }

    /// Render as a string
    public override string ToString()
    {
      if (this)
      {
        return string.Format("<Option({0})>", _value.ToString());
      }
      return "<Option(NONE)>";
    }
  }

  /// Helper methods for Option<T>
  public static class Option
  {
    /// Alias for new Option(foo)
    public static Option<U> Some<U>(U value)
    {
      return new Option<U>(value);
    }

    /// Alias for new Option()
    public static Option<U> None<U>()
    {
      return new Option<U>();
    }
  }
}