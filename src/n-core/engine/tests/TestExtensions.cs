using System.Collections.Generic;
using UnityEngine;
using N;

namespace N
{
    /// Helpers
    public class TestExtensionsData
    {
        /// Set of added object
        public static List<GameObject> spawned;
    }

    /// Test extension methods for unity
    /// This allows the core test classes to be folded into a 'pure' C# package.
    public static class TestExtensions
    {
        /// Create an empty game object and return it.
        public static GameObject SpawnBlank(this N.Tests.Test self)
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
        public static GameObject SpawnObjectWithComponent<T>(this N.Tests.Test self) where T : Component
        {
            var rtn = self.SpawnBlank();
            rtn.AddComponent<T>();
            return rtn;
        }

        /// Create an empty game object and return it.
        public static T SpawnComponent<T>(this N.Tests.Test self) where T : Component
        {
            var obj = self.SpawnBlank();
            var rtn = obj.AddComponent<T>();
            return rtn;
        }

        /// Teardown created objects
        public static void TearDown(this N.Tests.Test self)
        {
            if (TestExtensionsData.spawned != null)
            {
                foreach (var obj in TestExtensionsData.spawned)
                {
                    Object.DestroyImmediate(obj);
                }
                TestExtensionsData.spawned = null;
            }
        }
    }
}
