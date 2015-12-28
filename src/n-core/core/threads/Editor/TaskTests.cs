#if N_CORE_TESTS
using System;
using System.Threading;
using System.Collections.Generic;
using NUnit.Framework;
using N;
using N.Threads;

public class TestTask : Task<N.Tests.Test, int>
{
    public override void Run()
    {
        for (var i = 0; i < 10; ++i)
        {
            channel.Push(i);
        }
    }
}

public class TaskTests : N.Tests.Test
{
    [Test]
    public void test_task()
    {
        var chan = new TestTask().Spawn();
        Thread.Sleep(100);
        var counter = 0;
        for (var i = 0; i < 100; ++i)
        {
            if (chan.Pop())
            {
                counter += 1;
            }
            if (counter >= 10)
            {
                break;
            }
        }
        Assert(counter > 0);
    }
}
#endif
