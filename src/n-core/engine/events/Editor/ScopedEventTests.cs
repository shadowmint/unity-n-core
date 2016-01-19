#if N_CORE_TESTS
using NUnit.Framework;
using N;

/// Test implementations
class ScopedTestEvent1 : Event { }
class ScopedTestEvent2 : Event { }

/// Test function
public class ScopedEventsTests : N.Tests.Test
{
    [Test]
    public void test_trigger()
    {
        var events = new Events();

        var events1 = 0;
        new ScopedEvent<ScopedTestEvent1>(events, (ep) =>
        {
            events1 += 1;
            return false;
        });

        events.Trigger(new ScopedTestEvent1());
        events.Trigger(new ScopedTestEvent1());
        events.Trigger(new ScopedTestEvent2());

        Assert(events1 == 2);
    }

    [Test]
    public void test_resolve()
    {
        var events = new Events();

        var events1 = 0;
        new ScopedEvent<ScopedTestEvent1>(events, (ep) =>
        {
            events1 += 1;
            return events1 == 2;
        });

        events.Trigger(new ScopedTestEvent1());
        events.Trigger(new ScopedTestEvent1());
        events.Trigger(new ScopedTestEvent1());

        Assert(events1 == 2);
    }
}
#endif
