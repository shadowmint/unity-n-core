using UnityEngine;
using System.Collections.Generic;
using System;
using N;

namespace N {

  /// A pool of objects to use repeatedly in a scene
  public class ObjectPool {

    /// The prefab instance to use
    private GameObject factory;

    /// Set of instances
    private List<GameObject> instances = new List<GameObject>();

    /// Limit
    private Option<int> limit;

    /// Create an instance and provide a prefab factory to it to use
    /// @param factory A wrapped gameobject, use Scene.Prefab()
    /// @param limit The limit, if any, to the count of objects to spawn.
    public ObjectPool(Option<GameObject> factory, int limit = -1) {
      if (factory) {
        this.factory = factory.Unwrap();
        this.limit = limit >= 0 ? Option.Some(limit) : Option.None<int>();
        return;
      }
      throw new Exception("Unable to create object pool from null prefab");
    }

    /// Get an instance
    /// To release an instance, simply set active to false.
    public Option<GameObject> Instance() {
      var stored = NextFree();
      if (stored) { return stored; }
      return NewInstance();
    }

    /// Create a new instance
    private Option<GameObject> NewInstance() {
      if (limit) {
        if (instances.Count >= limit.Unwrap()) {
          return Option.None<GameObject>();
        }
      }
      var rtn = Scene.Spawn(factory);
      if (rtn) {
        instances.Add(rtn.Unwrap());
      }
      return rtn;
    }

    /// Return the next free instance
    private Option<GameObject> NextFree() {
      foreach (var instance in instances) {
        if (!instance.activeSelf) {
          instance.SetActive(true);
          return Option.Some(instance);
        }
      }
      return Option.None<GameObject>();
    }

    /// Drop all instances
    public void Clear() {
      foreach (var instance in instances) {
        GameObject.Destroy(instance);
      }
      instances.Clear();
    }

    /// Return a count of active objects
    public int Active {
      get {
        var count = 0;
        foreach (var instance in instances) {
          if (instance.activeSelf) {
            count += 1;
          }
        }
        return count;
      }
    }
  }
}
