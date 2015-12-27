using System;
using System.Threading;
using System.Collections.Generic;
using N.Tests;
using N;

namespace N.Threads
{

    /// A mutex controlled bidirection <U, V> pump to a remote thread
    public class Channel<U, V>
    {

        /// The remote channel instance
        public Channel<V, U> remote;

        /// Lock
        private Mutex lock_;

        /// The U queue for the remote side
        private Queue<V> incoming = new Queue<V>();

        public Channel()
        {
            lock_ = new Mutex();
            remote = new Channel<V, U>(lock_, this);
        }

        public Channel(Mutex m, Channel<V, U> remote)
        {
            lock_ = m;
            this.remote = remote;
        }

        /// Add an object into our local incoming object pool
        public void Receive(V value)
        {
            lock_.WaitOne();
            incoming.Enqueue(value);
            lock_.ReleaseMutex();
        }

        public void Push(U value)
        {
            remote.Receive(value);
        }

        public Option<V> Pop()
        {
            lock_.WaitOne();
            if (incoming.Count > 0)
            {
                return Option.Some<V>(incoming.Dequeue());
            }
            lock_.ReleaseMutex();
            return Option.None<V>();
        }
    }
}