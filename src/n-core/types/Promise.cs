using System.Collections.Generic;

namespace N.Package.Core
{
  /// Type of the internal data
  public enum PromiseType
  {
    None,
    Resolved,
    Rejected
  }

  /// The promise to invoke something if something
  public class Deferred<TOk, TErr>
  {
    /// Handlers
    private PromiseType _state;

    private PromiseDelegate<TOk, TErr> _invoker;

    /// The promise for this deferred state
    public Promise<TOk, TErr> Promise;

    /// The result value
    public Result<TOk, TErr> Result;

    /// Is this resolved yet?
    public bool Pending
    {
      get { return _state == PromiseType.None; }
    }

    public Deferred()
    {
      _state = PromiseType.None;
      Promise = new Promise<TOk, TErr>(this);
    }

    /// Set the invoker for this deferred
    public void Invoke(PromiseDelegate<TOk, TErr> invoker)
    {
      _invoker = invoker;
    }

    /// Resolve this promise
    public void Resolve(TOk value)
    {
      if (_state == PromiseType.None)
      {
        _state = PromiseType.Resolved;
        Result = Core.Result.Ok<TOk, TErr>(value);
        _invoker(Result);
      }
    }

    /// Reject this promise
    public void Reject(TErr value)
    {
      if (_state == PromiseType.None)
      {
        _state = PromiseType.Rejected;
        Result = Core.Result.Err<TOk, TErr>(value);
        _invoker(Result);
      }
    }
  }

  /// The promise for a value
  public class Promise<TOk, TErr>
  {
    /// The parent object
    private readonly Deferred<TOk, TErr> _parent;

    /// The set of deferred callbacks
    private readonly List<PromiseDelegate<TOk, TErr>> _callbacks;

    /// Invokation callback
    public Option<PromiseDelegate<TOk, TErr>> Invoker;

    /// Create a promise from a deferred
    public Promise(Deferred<TOk, TErr> parent)
    {
      this._parent = parent;
      _callbacks = new List<PromiseDelegate<TOk, TErr>>();
      parent.Invoke((r) =>
      {
        foreach (var callback in _callbacks)
        {
          callback(r);
        }
        _callbacks.Clear();
      });
    }

    /// Add a callback to invoke when the deferred is resolved or rejected
    /// Notice that if the promise is already resolved (eg. a stupid iterator)
    /// the callback is invoked as soon as it is called.
    public void Then(PromiseDelegate<TOk, TErr> callback)
    {
      if (_parent.Pending)
      {
        _callbacks.Add(callback);
      }
      else
      {
        callback(_parent.Result);
      }
    }
  }

  /// Delegate for resolving promises
  public delegate void PromiseDelegate<TOk, TErr>(Result<TOk, TErr> result);
}