#if N_CORE_TESTS
using NUnit.Framework;
using N.Package.Core.Tests;
using N.Package.Core.Threads;

public class ChannelTests : TestCase
{
  [Test]
  public void test_channel()
  {
    var channel = new Channel<int, TestCase>();
    var remote = channel.Remote;
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
#endif