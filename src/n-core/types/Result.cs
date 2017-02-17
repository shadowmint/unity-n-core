using System;

namespace N.Package.Core
{
  /// Type of the internal data
  public enum ResultType
  {
    Ok,
    Err
  }

  /// Basic result type
  public struct Result<TOk, TErr>
  {
    /// Actual values
    private readonly ResultType _type;

    private readonly TOk _ok;
    private readonly TErr _err;

    /// Create a new instance with value
    public Result(TOk value)
    {
      _err = default(TErr);
      _ok = value;
      _type = ResultType.Ok;
    }

    /// Create a new instance without value
    public Result(TErr value)
    {
      _err = value;
      _ok = default(TOk);
      _type = ResultType.Err;
    }

    /// Get inner value
    public Option<TOk> Ok
    {
      get
      {
        if (_type == ResultType.Ok)
        {
          return Option.Some(_ok);
        }
        return Option.None<TOk>();
      }
    }

    /// Get inner value
    public Option<TErr> Err
    {
      get
      {
        if (_type == ResultType.Err)
        {
          return Option.Some(_err);
        }
        return Option.None<TErr>();
      }
    }

    /// Check if the value in this Result is Ok or Err
    public bool IsOk
    {
      get { return _type == ResultType.Ok; }
    }

    /// Check if the value in this Result is Ok or Err
    public bool IsErr
    {
      get { return _type == ResultType.Err; }
    }

    /// Bool operator for Result type
    public static bool operator true(Result<TOk, TErr> v)
    {
      return v.IsOk;
    }

    public static bool operator false(Result<TOk, TErr> v)
    {
      return v.IsErr;
    }

    /// Run some callback if the value is valid
    public void Then(Action<TOk> someHandler)
    {
      Then(someHandler, null);
    }

    /// Run some callback if the value is valid
    public void Then(Action<TOk> okHandler, Action<TErr> errHandler)
    {
      if (this)
      {
        if (okHandler != null)
        {
          okHandler(_ok);
        }
      }
      else
      {
        if (errHandler != null)
        {
          errHandler(_err);
        }
      }
    }
  }

  /// Helper methods for Result<TOk, TErr>
  public static class Result
  {
    /// Alias for new Result(foo)
    public static Result<TOk, TErr> Ok<TOk, TErr>(TOk value)
    {
      return new Result<TOk, TErr>(value);
    }

    /// Alias for new Result()
    public static Result<TOk, TErr> Err<TOk, TErr>(TErr value)
    {
      return new Result<TOk, TErr>(value);
    }
  }
}