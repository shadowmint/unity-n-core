#if N_CORE_TESTS
using UnityEngine;
using UnityEditor;
using NUnit.Framework;
using N.Reflect;

/// Tests
public class PropTestsDummy
{
    public int value;
}

/// Tests
public class PropTests : N.Tests.Test
{

    public int foo;
    public int Bar
    {
        get { return foo; }
        set { foo = value; }
    }
    public int BarReadOnly
    {
        get { return foo; }
    }

    public int[] array_field_empty = null;
    public int[] array_field = new int[] { 1, 2, 3, 4, 5, 6 };
    public int[] array_prop { get { return array_field; } set { } }

    public PropTests nullable_field;
    public PropTests nullable_prop { get { return nullable_field; } set { } }

    [Test]
    public void test_array()
    {
        var prop = Type.Field("PropTests", "array_field").Unwrap();
        var items = prop.Array<int>(this);
        Assert(items != null);
        Assert(items.Length == 6);

        var prop2 = Type.Field("PropTests", "array_field_empty").Unwrap();
        var items2 = prop2.Array<int>(this);
        Assert(items2 == null);
    }

    [Test]
    public void test_is_array()
    {
        Assert(Type.Field("PropTests", "array_field").Unwrap().IsArray);
        Assert(Type.Field("PropTests", "array_prop").Unwrap().IsArray);
    }

    [Test]
    public void test_is_nullable()
    {
        Assert(Type.Field("PropTests", "nullable_field").Unwrap().IsNullable);
        Assert(Type.Field("PropTests", "nullable_prop").Unwrap().IsNullable);
        Assert(!Type.Field("PropTests", "foo").Unwrap().IsNullable);
        Assert(!Type.Field("PropTests", "Bar").Unwrap().IsNullable);
    }

    [Test]
    public void test_get_set_field()
    {
        var prop = Type.Field("PropTests", "foo");
        if (prop)
        {
            var prop_ = prop.Unwrap();
            foo = 100;
            Assert(prop_.Get<int>(this) == 100);
            Assert(prop_.Set(this, 101));
            Assert(prop_.Get<int>(this) == 101);
            Assert(!prop_.IsArray);
        }
        else
        {
            Unreachable();
        }
    }

    [Test]
    public void test_get_set_property()
    {
        var prop = Type.Field("PropTests", "Bar");
        if (prop)
        {
            var prop_ = prop.Unwrap();
            foo = 100;
            Assert(prop_.Get<int>(this) == 100);
            Assert(prop_.Set(this, 101));
            Assert(prop_.Get<int>(this) == 101);
        }
        else
        {
            Unreachable();
        }
    }

    [Test]
    public void test_get_set_property_read_only()
    {
        var prop = Type.Field("PropTests", "BarReadOnly");
        if (prop)
        {
            var prop_ = prop.Unwrap();
            foo = 100;
            Assert(prop_.Get<int>(this) == 100);
            Assert(!prop_.Set(this, 101));
            Assert(prop_.Get<int>(this) == 100);
        }
        else
        {
            Unreachable();
        }
    }

    [Test]
    public void test_dynamic_rebind()
    {
        var source_prop = Type.Field("PropTests", "BarReadOnly").Unwrap();
        var target_prop = Type.Field("PropTestsDummy", "value").Unwrap();

        var other = new PropTestsDummy();
        other.value = 0;

        foo = 100;

        source_prop.Bind(this, target_prop, other);
        Assert(other.value == 100);
    }
}
#endif
