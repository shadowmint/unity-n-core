using System;
using System.Linq;
using System.Collections.Generic;
using N;
using N.Tests;

namespace N {

  /// Bind an event listener for a click on the target
  public delegate void EventHandler(Event item);

  /// Base type for events
  public abstract class Event {

    /// Return name of this event
    public string Name { get { return N.Reflect.Type.Name(this); } }

    /// Convert if valid, making easy api use, like:
    /// (ev) => { ev.As<FooEvent>().Then(fp) => { ... }}
    public Option<T> As<T>() where T: Event {
      var rtn = this as T;
      if (rtn != null) {
        return Option.Some(rtn);
      }
      return Option.None<T>();
    }
  }

  /// A collection of event handlers
  public class Events {

    /// The timer we use for this event handler, if any
    private bool hasTimer;

    /// Set of event delegates to invoke
    public List<EventHandler> handlers = new List<EventHandler>();

    /// Set of deferred events
    public List<DeferredEvent> deferred = new List<DeferredEvent>();

    /// Create a new event handler with no timer
    public Events() {
      hasTimer = false;
    }

    /// Create a new event handler with a timer
    public Events(Timer timer) {
      hasTimer = true;
      timer.OnUpdate((e) => {
        ProcessDeferredEvents((e as TimerEvent).delta);
      });
    }

    /// Attach a handler
    public static Events operator +(Events listener, EventHandler hp) {
      listener.handlers.Add(hp);
      return listener;
    }

    /// Trigger an event on this target, if it matches
    public void Trigger(Event item) {
      handlers.ForEach(delegate(EventHandler handler) { handler(item); });
    }

    /// Trigger an event which is deferred for some time
    /// @param item The event to trigger.
    /// @param interval The time after which to trigger the event.
    public void Deferred(Event item, float interval) {
      if (hasTimer) {
        deferred.Add(new DeferredEvent {
          item = item,
          interval = interval,
          elapsed = 0f
        });
      }
      else {
        throw new Exception("Cannot use deferred events without an assigned Timer");
      }
    }

    /// Manually trigger deferred events, for tests
    public void TriggerDeferred(float step) {
      if (step >= 0f) {
        ProcessDeferredEvents(step);
      }
    }

    /// Process and trigger deferred events
    private void ProcessDeferredEvents(float interval) {
      foreach (var def in deferred) {
        def.elapsed += interval;
      }
      var pending = deferred.Where(x => x.elapsed >= x.interval).ToList();
      deferred.RemoveAll(x => x.elapsed >= x.interval);
      foreach (var def in pending) {
        Trigger(def.item);
      }
    }

    /// Clear a specific event handler
    public void Remove(EventHandler handler) {
      handlers.RemoveAll(x => x == handler);
    }

    /// Clear everything
    public void Clear() {
      handlers.Clear();
      deferred.Clear();
    }
  }

  /// A deferred event
  public class DeferredEvent {
    public Event item;
    public float interval;
    public float elapsed;
  }

  /// Test implementations
  class TestEvent1 : Event {}
  class TestEvent2 : Event {}

  /// Test function
  public class EventsTests : TestSuite {

    public void test_event_name() {
      var instance = new TestEvent1();
      Assert(instance.Name == "N.TestEvent1");
    }

    public void test_trigger() {
      var events1 = 0;
      var events2 = 0;
      var instance = new Events();

      EventHandler handler = (Event e) => {
        if (e is TestEvent1) { events1 += 1; }
        if (e is TestEvent2) { events2 += 1; }
      };
      instance += handler;

      instance.Trigger(new TestEvent1());
      instance.Trigger(new TestEvent2());
      instance.Trigger(new TestEvent2());

      Assert(events1 == 1);
      Assert(events2 == 2);

      instance.Remove(handler);
      instance.Trigger(new TestEvent1());
      instance.Trigger(new TestEvent2());

      Assert(events1 == 1);
      Assert(events2 == 2);
    }

    public void test_deferred_no_timer() {
      var instance = new Events();
      try {
        instance.Deferred(new TestEvent1(), 1f);
        Unreachable();
      }
      catch(Exception) {
      }
    }

    public void test_deferred() {
      var events = 0;
      var timer = new Timer();
      var instance = new Events(timer);

      instance += (Event e) => { events += 1; };

      instance.Deferred(new TestEvent1(), 1f);
      Assert(events == 0);

      timer.Force(0.5f);
      timer.Step();
      Assert(events == 0);

      timer.Force(0.5f);
      timer.Step();
      Assert(events == 1);
    }
  }
}
