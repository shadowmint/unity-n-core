#if N_CORE_TESTS
using UnityEngine;
using NUnit.Framework;
using N;
using N.Package.Core;
using N.Package.Core.Tests;

public class CameraExtensionsTests : TestCase
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
#endif