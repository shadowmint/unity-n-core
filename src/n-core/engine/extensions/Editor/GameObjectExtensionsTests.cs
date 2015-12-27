using UnityEngine;
using System;
using NUnit.Framework;
using N;

public class GameObjectExtensionsTests : N.Tests.Test
{
    [Test]
    public void test_get_camera()
    {
        var obj = this.SpawnBlank();
        obj.Camera();
        this.TearDown();
    }

    [Test]
    public void test_set_rotation()
    {
        var obj = this.SpawnBlank();
        obj.SetRotation(new Vector3(0f, 0f, 0f));
        this.TearDown();
    }

    [Test]
    public void test_screen_coordinates()
    {
        var obj = this.SpawnBlank();
        obj.ScreenCoordinates();
        this.TearDown();
    }

    [Test]
    public void test_relative_mouse_position_3d()
    {
        var obj = this.SpawnBlank();
        obj.RelativeMouseVector3();
        obj.RelativeMouseUnitVector3();
        this.TearDown();
    }

    [Test]
    public void test_relative_mouse_position_2d()
    {
        var obj = this.SpawnBlank();
        obj.RelativeMouseVector2();
        obj.RelativeMouseUnitVector2();
        this.TearDown();
    }

    [Test]
    public void test_add_remove_component()
    {
        var obj = this.SpawnBlank();
        obj.RemoveComponents<TestComponent>();
        var marker = obj.AddComponent<TestComponent>();
        obj.RemoveComponent(marker, true);
        Assert(!obj.HasComponent<TestComponent>());
        this.TearDown();
    }

    [Test]
    public void test_get_required_component()
    {
        var obj = this.SpawnBlank();
        try
        {
            obj.GetRequiredComponent<TestComponent>();
            Unreachable();
        }
        catch (Exception)
        {
            Assert(true);
        }
        var c = obj.GetRequiredComponent<TestComponent>(true);
        Assert(c != null);
        this.TearDown();
    }
}
