namespace N
{
    /// Bind an event listener for a click on the target
    public delegate bool ScopedEventHandler<T>(T item) where T : N.Event;

    /// This class exists to bind an event handler until a specific event
    /// occurs, and then drop the event handler.
    public class ScopedEvent<T> where T : N.Event
    {
        /// Create a new instance
        public ScopedEvent(Events events, ScopedEventHandler<T> handler)
        {
            EventHandler eventHandler = (evp) =>
            {
                evp.As<T>().Then((ep) =>
                {
                    if (handler(ep))
                    {
                        events.Remove(eventHandler);
                    }
                });
            };
            events += eventHandler;
        }
    }
}
