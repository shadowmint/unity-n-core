#if N_CORE_TESTS
using System;
using NUnit.Framework;
using N.Package.Core;
using N.Package.Core.Tests;
using Enum = N.Package.Core.Reflect.Enum;
using Type = N.Package.Core.Reflect.Type;

public class EnumTests : TestCase
{
  [Test]
  public void test_enumerate_enum_by_invalid()
  {
    var items = Enum.Enumerate("N.OptionTypes.ADSFF");
    Assert(items.Length == 0);
  }

  [Test]
  public void test_enumerate_enum_by_string()
  {
    var items = Enum.Enumerate("N.Package.Core.OptionType");
    Assert(Array.IndexOf(items, "Some") >= 0);
    Assert(Array.IndexOf(items, "None") >= 0);
  }

  [Test]
  public void test_enumerate_enum_by_type()
  {
    var etype = Type.Resolve("N.Package.Core.OptionType").Unwrap();
    var items = Enum.Enumerate(etype);
    Assert(Array.IndexOf(items, "Some") >= 0);
    Assert(Array.IndexOf(items, "None") >= 0);
  }

  [Test]
  public void test_parse()
  {
    var etype = Type.Resolve("N.Package.Core.OptionType").Unwrap();
    var some = Enum.Resolve<OptionType>(etype, "Some");
    var none = Enum.Resolve<OptionType>(etype, "None");
    var invalid = Enum.Resolve<OptionType>(etype, "ERROR");
    Assert(some.IsSome);
    Assert(none.IsSome);
    Assert(invalid.IsNone);
  }

  [Test]
  public void test_parse_with_string_type()
  {
    var some = Enum.Resolve<OptionType>(typeof(OptionType).AssemblyQualifiedName, "Some");
    var none = Enum.Resolve<OptionType>(typeof(OptionType).AssemblyQualifiedName, "None");
    var invalid = Enum.Resolve<OptionType>(typeof(OptionType).AssemblyQualifiedName, "ERROR");
    Assert(some.IsSome);
    Assert(none.IsSome);
    Assert(invalid.IsNone);
  }
}
#endif