#if N_CORE_TESTS
using UnityEngine;
using NUnit.Framework;
using N;
using N.Package.Core;
using N.Package.Core.Tests;

public class MonoBehaviourExtensionsTests : TestCase
{
  [Test]
  public void test_add_remove_component()
  {
    var obj = this.SpawnBlank();
    Assert(!obj.HasComponent<TestComponent>());

    var marker = obj.AddComponent<TestComponent>();
    Assert(obj.HasComponent<TestComponent>());
    obj.RemoveComponent(marker, true);

    Assert(!obj.HasComponent<TestComponent>());
    this.TearDown();
  }
}
#endif