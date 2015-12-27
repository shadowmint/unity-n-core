using System;
using N;
using NUnit.Framework;

public class ResultTests : N.Tests.Test
{
    [Test]
    public Result<int, string> testFixture(int value)
    {
        if (value > 0)
        {
            return Result.Ok<int, string>(value);
        }
        return Result.Err<int, string>("Invalid");
    }

    [Test]
    public void test_result_ok()
    {
        var x = testFixture(100);
        Assert(x.Ok.Unwrap() == 100);
        Assert(x.IsOk);
        Assert(!x.IsErr);
        if (x) { Assert(true); }
        else { Unreachable(); }
    }

    [Test]
    public void test_result_err()
    {
        var x = testFixture(-100);
        Assert(x.Err.Unwrap() == "Invalid");
        Assert(x.IsErr);
        Assert(!x.IsOk);
        if (x) { Unreachable(); }
        else { Assert(true); }
    }

    [Test]
    public void test_result_then()
    {
        var valid = 0;
        var invalid = 0;
        testFixture(100).Then((v) => { valid += 1; });
        testFixture(-100).Then((v) => { valid += 1; }, (v) => { invalid += 1; });
        Assert(valid == 1);
        Assert(invalid == 1);
    }
}
