using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;
using N;
using Object = UnityEngine.Object;

namespace N.Package.Core
{
  /// A pool of objects to use repeatedly in a scene
  public class ObjectPool
  {
    /// The prefab instance to use
    private readonly GameObject _factory;

    /// Set of instances
    private readonly List<GameObject> _instances = new List<GameObject>();

    /// Limit
    private Option<int> _limit;

    /// Create an instance and provide a prefab factory to it to use
    /// @param factory A wrapped gameobject, use Scene.Prefab()
    /// @param limit The limit, if any, to the count of objects to spawn.
    public ObjectPool(Option<GameObject> factory, int limit = -1)
    {
      if (factory)
      {
        _factory = factory.Unwrap();
        _limit = limit >= 0 ? Option.Some(limit) : Option.None<int>();
        return;
      }
      throw new Exception("Unable to create object pool from null prefab");
    }

    /// Get an instance
    /// To release an instance, simply set active to false.
    public Option<GameObject> Instance()
    {
      var stored = NextFree();
      return stored ? stored : NewInstance();
    }

    /// Create a new instance
    private Option<GameObject> NewInstance()
    {
      if (_limit)
      {
        if (_instances.Count >= _limit.Unwrap())
        {
          return Option.None<GameObject>();
        }
      }
      var rtn = Scene.Spawn(_factory);
      if (rtn)
      {
        _instances.Add(rtn.Unwrap());
      }
      return rtn;
    }

    /// Return the next free instance
    private Option<GameObject> NextFree()
    {
      foreach (var instance in _instances)
      {
        if (instance.activeSelf) continue;
        instance.SetActive(true);
        return Option.Some(instance);
      }
      return Option.None<GameObject>();
    }

    /// Drop all instances
    public void Clear()
    {
      foreach (var instance in _instances)
      {
        Object.Destroy(instance);
      }
      _instances.Clear();
    }

    /// Return a count of active objects
    public int Active
    {
      get
      {
        return _instances.Count(instance => instance.activeSelf);
      }
    }
  }
}