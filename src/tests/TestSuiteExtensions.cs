using System.Collections.Generic;
using UnityEngine;
using N.Tests;
using N;
#if UNITY_EDITOR
using UnityEditor;
using System.IO;
#endif

namespace N {

  /// Helpers
  public class TestSuiteExtensionsData {

    /// Set of added object
    public static List<GameObject> spawned;
  }

  /// Test extension methods for unity
  /// This allows the core test classes to be folded into a 'pure' C# package.
  public static class TestSuiteExtensions {

    /// Create an empty game object and return it.
    public static GameObject SpawnBlank(this TestSuite self)  {
      if (TestSuiteExtensionsData.spawned == null) {
        TestSuiteExtensionsData.spawned = new List<GameObject>();
      }
      var rtn = new GameObject();
      TestSuiteExtensionsData.spawned.Add(rtn);
      return rtn;
    }

    /// Create an empty game object and return it.
    public static GameObject SpawnObjectWithComponent<T>(this TestSuite self) where T : Component {
      var rtn = self.SpawnBlank();
      rtn.AddComponent<T>();
      return rtn;
    }

    /// Create an empty game object and return it.
    public static T SpawnComponent<T>(this TestSuite self) where T : Component {
      var obj = self.SpawnBlank();
      var rtn = obj.AddComponent<T>();
      return rtn;
    }

    /// Teardown created objects
    public static void TearDown(this TestSuite self) {
      if (TestSuiteExtensionsData.spawned != null) {
        foreach (var obj in TestSuiteExtensionsData.spawned) {
          Object.DestroyImmediate(obj);
        }
        TestSuiteExtensionsData.spawned = null;
      }
    }
  }

  #if UNITY_EDITOR
  public class TestExtensionsTests : TestSuite {
    public void test_screen_coordinates_by_position() {
      var obj = this.SpawnBlank();
      Assert(obj != null);
      this.TearDown();
    }
  } #endif
}
