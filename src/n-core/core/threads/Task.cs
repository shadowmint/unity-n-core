using System;
using System.Threading;
using System.Collections.Generic;
using N.Tests;
using N;

namespace N.Threads
{
    /// A remote thread task
    public abstract class Task<U, V>
    {
        /// The thread instance
        private Thread thread;

        /// The remote channel instance
        protected Channel<V, U> channel;

        /// The function to overload with a task
        public abstract void Run();

        /// Actual runner
        private void Runner(object remoteChannel)
        {
            channel = remoteChannel as Channel<V, U>;
            Run();
        }

        /// Spawn a task
        public Channel<U, V> Spawn()
        {
            var rtn = new Channel<U, V>();
            thread = new Thread(this.Runner);
            thread.Start(rtn.remote);
            return rtn;
        }
    }
}
