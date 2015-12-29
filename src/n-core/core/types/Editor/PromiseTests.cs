#if N_CORE_TESTS
using System;
using System.Collections.Generic;
using NUnit.Framework;
using N;

public class PromiseTests : N.Tests.Test
{
    [Test]
    public void test_promise_resolve()
    {
        bool resolve = false;
        var def = new Deferred<int, int>();
        def.promise.Then((r) =>
        {
            if (r.IsOk) { resolve = r.Ok.Unwrap() == 1; }
        });

        Assert(!resolve);
        def.Resolve(1);
        Assert(resolve);
    }

    [Test]
    public void test_promise_reject()
    {
        bool rejected = false;
        var def = new Deferred<int, int>();
        def.promise.Then((r) =>
        {
            if (r.IsErr) { rejected = r.Err.Unwrap() == 1; }
        });

        Assert(!rejected);
        def.Reject(1);
        Assert(rejected);
    }
}
#endif
