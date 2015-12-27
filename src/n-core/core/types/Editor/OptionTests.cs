using System;
using NUnit.Framework;
using N;

public class OptionTests : N.Tests.Test
{
    [Test]
    public void test_option_some()
    {
        var x = testFixture(100);
        Assert(x.IsSome);
        Assert(!x.IsNone);
        if (x) { Assert(true); }
        else { Unreachable(); }
    }

    [Test]
    public void test_option_none()
    {
        var x = testFixture(-100);
        Assert(x.IsNone);
        Assert(!x.IsSome);
        if (x) { Unreachable(); }
        else { Assert(true); }
    }

    [Test]
    public void test_option_then_with_some()
    {
        int count = 0;
        testFixture(100).Then((int value) =>
        {
            Assert(100 == value);
            count += 1;
        }, () =>
        {
            Unreachable();
        });
        Assert(count == 1);
    }

    [Test]
    public void test_option_then_simple()
    {
        int count = 0;
        testFixture(100).Then((int value) =>
        {
            Assert(100 == value);
            count += 1;
        });
        Assert(count == 1);
    }

    public void test_option_then_with_none()
    {
        int count = 0;
        testFixture(-100).Then((int value) =>
        {
            Unreachable();
        }, () =>
        {
            count += 1;
        });
        Assert(count == 1);
    }

    [Test]
    public void test_option_unwrap_some()
    {
        Assert(testFixture(100).Unwrap() == 100);
    }

    [Test]
    public void test_option_unwrap_none()
    {
        bool caught = false;
        try
        {
            testFixture(-100).Unwrap();
        }
        catch (Exception)
        {
            caught = true;
        }
        Assert(caught, "Failed to throw an exception on invalid unrwap");
    }

    [Test]
    public Option<int> testFixture(int value)
    {
        if (value > 0)
        {
            return Option.Some(value);
        }
        return Option.None<int>();
    }
}
