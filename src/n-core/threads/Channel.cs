using System.Threading;
using System.Collections.Generic;

namespace N.Package.Core.Threads
{
  /// A mutex controlled bidirection <TIn, TOut> pump to a remote thread.
  public class Channel<TIn, TOut>
  {
    /// The remote channel instance
    public Channel<TOut, TIn> Remote;

    /// Lock
    private readonly Mutex _lock;

    /// The U queue for the remote side
    private readonly Queue<TOut> _incoming = new Queue<TOut>();

    public Channel()
    {
      _lock = new Mutex();
      Remote = new Channel<TOut, TIn>(_lock, this);
    }

    public Channel(Mutex m, Channel<TOut, TIn> remote)
    {
      _lock = m;
      this.Remote = remote;
    }

    /// Add an object into our local incoming object pool
    public void Receive(TOut value)
    {
      _lock.WaitOne();
      _incoming.Enqueue(value);
      _lock.ReleaseMutex();
    }

    public void Push(TIn value)
    {
      Remote.Receive(value);
    }

    public Option<TOut> Pop()
    {
      _lock.WaitOne();
      if (_incoming.Count > 0)
      {
        return Option.Some<TOut>(_incoming.Dequeue());
      }
      _lock.ReleaseMutex();
      return Option.None<TOut>();
    }
  }
}