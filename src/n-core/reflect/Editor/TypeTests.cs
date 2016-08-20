#if N_CORE_TESTS
using System;
using UnityEngine;
using UnityEditor;
using NUnit.Framework;
using System.Collections.Generic;
using N.Package.Core;
using N.Package.Core.Tests;
using Type = N.Package.Core.Reflect.Type;

/// Tests
public class TypeTests : TestCase
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
    TestCase a = this;
    Assert(Type.Is<TypeTests>(a));
  }

  [Test]
  public void test_name()
  {
    Assert(Type.Name<TypeTests>() == "TypeTests");
    Assert(Type.Name(this) == "TypeTests");
  }

  [Test]
  public void test_resolve()
  {
    Assert(Type.Resolve(typeof(Option).AssemblyQualifiedName).IsSome);
    Assert(Type.Resolve(typeof(Option).AssemblyQualifiedName).Unwrap() == typeof(Option));
    Assert(Type.Resolve("N.OptionADSFDSF").IsNone);
  }

  [Test]
  public void test_fields()
  {
    var f1 = Type.Fields(this);
    var f2 = Type.Fields<TypeTests>();
    var f3 = Type.Fields(typeof(TypeTests));
    Assert(Array.IndexOf(f1, "foo") != -1);
    Assert(Array.IndexOf(f2, "bar") != -1);
    Assert(Array.IndexOf(f3, "foo") != -1);
    Assert(Array.IndexOf(f3, "Bar3") != -1);

    // Filtered out invalid proeprties
    Assert(Array.IndexOf(f3, "Bar") == -1);
    Assert(Array.IndexOf(f3, "Bar2") == -1);

    // Only getters
    var f4 = Type.Fields(typeof(TypeTests), true, false);
    Assert(Array.IndexOf(f4, "Bar") != -1);
    Assert(Array.IndexOf(f4, "Bar2") == -1);

    // Only setters
    var f5 = Type.Fields(typeof(TypeTests), false, true);
    Assert(Array.IndexOf(f5, "Bar") == -1);
    Assert(Array.IndexOf(f5, "Bar2") != -1);
  }
}
#endif