#if N_CORE_TESTS
﻿using UnityEngine;
using UnityEditor;
using NUnit.Framework;
using System;
using System.Collections.Generic;

public class EnumTests : N.Tests.Test
{
    [Test]
    public void test_enumerate_enum_by_invalid()
    {
        var items = N.Reflect.Enum.Enumerate("N.OptionTypes.ADSFF");
        Assert(items.Length == 0);
    }

    [Test]
    public void test_enumerate_enum_by_string()
    {
        var items = N.Reflect.Enum.Enumerate("N.OptionType");
        Assert(Array.IndexOf(items, "SOME") >= 0);
        Assert(Array.IndexOf(items, "NONE") >= 0);
    }

    [Test]
    public void test_enumerate_enum_by_type()
    {
        var etype = N.Reflect.Type.Resolve("N.OptionType").Unwrap();
        var items = N.Reflect.Enum.Enumerate(etype);
        Assert(Array.IndexOf(items, "SOME") >= 0);
        Assert(Array.IndexOf(items, "NONE") >= 0);
    }

    [Test]
    public void test_parse()
    {
        var etype = N.Reflect.Type.Resolve("N.OptionType").Unwrap();
        var some = N.Reflect.Enum.Resolve<N.OptionType>(etype, "SOME");
        var none = N.Reflect.Enum.Resolve<N.OptionType>(etype, "NONE");
        var invalid = N.Reflect.Enum.Resolve<N.OptionType>(etype, "ERROR");
        Assert(some.IsSome);
        Assert(none.IsSome);
        Assert(invalid.IsNone);
    }

    [Test]
    public void test_parse_with_string_type()
    {
        var some = N.Reflect.Enum.Resolve<N.OptionType>("N.OptionType", "SOME");
        var none = N.Reflect.Enum.Resolve<N.OptionType>("N.OptionType", "NONE");
        var invalid = N.Reflect.Enum.Resolve<N.OptionType>("N.OptionType", "ERROR");
        Assert(some.IsSome);
        Assert(none.IsSome);
        Assert(invalid.IsNone);
    }
}
#endif
