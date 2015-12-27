using UnityEngine;
using NUnit.Framework;
using N;

public class CameraExtensionsTests : N.Tests.Test
{
    [Test]
    public void test_screen_coordinates_by_position()
    {
        var obj = this.SpawnBlank();
        obj.Camera().ScreenCoordinates(obj.transform.position);
        this.TearDown();
    }

    [Test]
    public void test_screen_coordinates_by_object()
    {
        var obj = this.SpawnBlank();
        obj.Camera().ScreenCoordinates(obj);
        this.TearDown();
    }
}
