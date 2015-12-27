using System;
using System.Threading;
using System.Collections.Generic;
using NUnit.Framework;
using N.Threads;
using N;

public class ChannelTests : N.Tests.Test
{
    [Test]
    public void test_channel()
    {
        var channel = new Channel<int, N.Tests.Test>();
        var remote = channel.remote;
        Assert(remote.Pop().IsNone);
        Assert(channel.Pop().IsNone);

        channel.Push(100);
        var val = remote.Pop();
        Assert(val.IsSome);
        Assert(val.Unwrap() == 100);

        remote.Push(this);
        var val2 = channel.Pop();
        Assert(val2.IsSome);
        Assert(val2.Unwrap() == this);
    }
}
