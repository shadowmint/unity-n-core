using System.Collections.Generic;
using UnityEngine;
using N.Package.Core.Tests;

namespace N.Package.Core
{
  /// Helpers
  public class TestExtensionsData
  {
    /// Set of added object
    public static List<GameObject> spawned;
  }

  /// TestCase extension methods for unity
  /// This allows the core test classes to be folded into a 'pure' C# package.
  public static class TestExtensions
  {
    /// Create an empty game object and return it.
    public static GameObject SpawnBlank(this TestCase self)
    {
      if (TestExtensionsData.spawned == null)
      {
        TestExtensionsData.spawned = new List<GameObject>();
      }
      var rtn = new GameObject();
      TestExtensionsData.spawned.Add(rtn);
      return rtn;
    }

    /// Create an empty game object and return it.
    public static GameObject SpawnObjectWithComponent<T>(this TestCase self) where T : Component
    {
      var rtn = self.SpawnBlank();
      rtn.AddComponent<T>();
      return rtn;
    }

    /// Create an empty game object and return it.
    public static T SpawnComponent<T>(this TestCase self) where T : Component
    {
      var obj = self.SpawnBlank();
      var rtn = obj.AddComponent<T>();
      return rtn;
    }

    /// Teardown created objects
    public static void TearDown(this TestCase self)
    {
      if (TestExtensionsData.spawned == null) return;
      foreach (var obj in TestExtensionsData.spawned)
      {
        Object.DestroyImmediate(obj);
      }
      TestExtensionsData.spawned = null;
    }
  }
}