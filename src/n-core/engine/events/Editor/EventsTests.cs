#if N_CORE_TESTS
using NUnit.Framework;
using N;
using Foo;

/// Test implementations
namespace Foo {
  class TestEvent1 : Event { }
  class TestEvent2 : Event { }
}

/// Test function
public class EventsTests : N.Tests.Test
{
    [Test]
    public void test_event_name()
    {
        var instance = new TestEvent1();
        Assert(instance.Name == "Foo.TestEvent1");
    }

    [Test]
    public void test_trigger()
    {
        var events1 = 0;
        var events2 = 0;
        var instance = new Events();

        EventHandler handler = (Event e) =>
        {
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

    [Test]
    public void test_deferred_no_timer()
    {
        var instance = new Events();
        try
        {
            instance.Deferred(new TestEvent1(), 1f);
            Unreachable();
        }
        catch (System.Exception)
        {
        }
    }

    [Test]
    public void test_deferred()
    {
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
#endif
