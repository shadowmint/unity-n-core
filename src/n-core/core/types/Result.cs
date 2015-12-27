using System;
using N.Tests;

namespace N
{

    /// Type of the internal data
    public enum ResultType
    {
        OK,
        ERR
    }

    /// Basic result type
    public struct Result<TOk, TErr>
    {

        /// Actual values
        private ResultType type;
        private TOk ok;
        private TErr err;

        /// Create a new instance with value
        public Result(TOk value)
        {
            err = default(TErr);
            ok = value;
            type = ResultType.OK;
        }

        /// Create a new instance without value
        public Result(TErr value)
        {
            err = value;
            ok = default(TOk);
            type = ResultType.ERR;
        }

        /// Get inner value
        public Option<TOk> Ok
        {
            get
            {
                if (type == ResultType.OK)
                {
                    return Option.Some(ok);
                }
                return Option.None<TOk>();
            }
        }

        /// Get inner value
        public Option<TErr> Err
        {
            get
            {
                if (type == ResultType.ERR)
                {
                    return Option.Some(err);
                }
                return Option.None<TErr>();
            }
        }

        /// Check if the value in this Result is Ok or Err
        public bool IsOk
        {
            get
            {
                return type == ResultType.OK;
            }
        }

        /// Check if the value in this Result is Ok or Err
        public bool IsErr
        {
            get
            {
                return type == ResultType.ERR;
            }
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
        public void Then(ResultDelegate<TOk> someHandler)
        {
            Then(someHandler, null);
        }

        /// Run some callback if the value is valid
        public void Then(ResultDelegate<TOk> okHandler, ResultDelegate<TErr> errHandler)
        {
            if (this)
            {
                if (okHandler != null)
                {
                    okHandler(ok);
                }
            }
            else
            {
                if (errHandler != null)
                {
                    errHandler(err);
                }
            }
        }
    }

    /// Event handler
    public delegate void ResultDelegate<T>(T value);

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
