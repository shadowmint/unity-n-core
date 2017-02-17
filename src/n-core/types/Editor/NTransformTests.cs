#if N_CORE_TESTS
using UnityEngine;
using NUnit.Framework;
using N.Package.Core;
using N.Package.Core.Tests;

/// Tests
public class NTransformTests : TestCase
{
  [Test]
  public void test_create_transform()
  {
    var blank = this.SpawnBlank();
    new NTransform(blank);
    new NTransform(blank.transform);
    this.TearDown();
  }

  [Test]
  public void test_creates_copy()
  {
    var blank = this.SpawnBlank();
    blank.Move(new Vector3(1f, 1f, 1f));
    var transform = new NTransform(blank);
    blank.Move(new Vector3(2f, 2f, 2f));
    Assert(transform.Position == new Vector3(1f, 1f, 1f));
    this.TearDown();
  }
}
#endif