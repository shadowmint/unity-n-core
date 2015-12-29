#if N_CORE_TESTS
using UnityEngine;
using UnityEditor;
using NUnit.Framework;
using System;
using System.Collections.Generic;

/// Tests
public class TypeTests : N.Tests.Test
{

    public int foo;
    public int bar;
    public int Bar
    {
        get { return 0; }
    }
    public int Bar2
    {
        set { }
    }
    public int Bar3
    {
        get { return 0; }
        set { }
    }

    [Test]
    public void test_is()
    {
        N.Tests.Test a = this;
        Assert(N.Reflect.Type.Is<TypeTests>(a));
    }

    [Test]
    public void test_name()
    {
        Assert(N.Reflect.Type.Name<TypeTests>() == "TypeTests");
        Assert(N.Reflect.Type.Name(this) == "TypeTests");
    }

    [Test]
    public void test_resolve()
    {
        Assert(N.Reflect.Type.Resolve("N.Option").IsSome);
        Assert(N.Reflect.Type.Resolve("N.Option").Unwrap() == typeof(N.Option));
        Assert(N.Reflect.Type.Resolve("N.OptionADSFDSF").IsNone);
    }

    [Test]
    public void test_fields()
    {
        var f1 = N.Reflect.Type.Fields(this);
        var f2 = N.Reflect.Type.Fields<TypeTests>();
        var f3 = N.Reflect.Type.Fields(typeof(TypeTests));
        Assert(Array.IndexOf(f1, "foo") != -1);
        Assert(Array.IndexOf(f2, "bar") != -1);
        Assert(Array.IndexOf(f3, "foo") != -1);
        Assert(Array.IndexOf(f3, "Bar3") != -1);

        // Filtered out invalid proeprties
        Assert(Array.IndexOf(f3, "Bar") == -1);
        Assert(Array.IndexOf(f3, "Bar2") == -1);

        // Only getters
        var f4 = N.Reflect.Type.Fields(typeof(TypeTests), true, false);
        Assert(Array.IndexOf(f4, "Bar") != -1);
        Assert(Array.IndexOf(f4, "Bar2") == -1);

        // Only setters
        var f5 = N.Reflect.Type.Fields(typeof(TypeTests), false, true);
        Assert(Array.IndexOf(f5, "Bar") == -1);
        Assert(Array.IndexOf(f5, "Bar2") != -1);
    }
}
#endif
