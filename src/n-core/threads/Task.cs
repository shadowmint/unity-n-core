using System.Threading;

namespace N.Package.Core.Threads
{
  /// A remote thread task
  public abstract class Task<TIn, TOut>
  {
    /// The thread instance
    private Thread _thread;

    /// The remote channel instance
    protected Channel<TOut, TIn> Channel;

    /// The function to overload with a task
    public abstract void Run();

    /// Actual runner
    private void Runner(object remoteChannel)
    {
      Channel = remoteChannel as Channel<TOut, TIn>;
      Run();
    }

    /// Spawn a task
    public Channel<TIn, TOut> Spawn()
    {
      var rtn = new Channel<TIn, TOut>();
      _thread = new Thread(Runner);
      _thread.Start(rtn.Remote);
      return rtn;
    }
  }
}