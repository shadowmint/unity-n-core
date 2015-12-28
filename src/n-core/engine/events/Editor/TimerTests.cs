#if N_CORE_TESTS
using UnityEngine;
using NUnit.Framework;
using N;

public class TimerTests : N.Tests.Test
{
    [Test]
    public void test_custom_timer()
    {
        var timer = new Timer();

        var counter = 0;
        timer.OnUpdate((dt) => { counter += 1; });

        timer.Force(1f);
        Assert(timer.Step() == 1f);
        Assert(counter == 1);

        timer.Force(2f);
        Assert(timer.Step() == 2f);
        Assert(counter == 2);
    }
}
#endif
