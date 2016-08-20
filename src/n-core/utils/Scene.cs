using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace N.Package.Core
{
  /// Global functions to modify the current scene
  public class Scene
  {
    /// Fetch a game object factory from a resource url
    /// @param resource The resource path to the instance
    /// @return A factory instance, for a prefab.
    public static Option<GameObject> Prefab(string resource)
    {
      try
      {
        var rtn = Resources.Load(resource, typeof(GameObject)) as GameObject;
        if (rtn != null)
        {
          return Option.Some(rtn);
        }
        else
        {
          Console.Error("Failed to load prefab path: " + resource + ": not found");
        }
      }
      catch (Exception e)
      {
        Console.Error("Failed to load prefab path: " + resource + ": " + e);
      }
      return Option.None<GameObject>();
    }

    /// Add a new resource prefab instance to the current scene and return it
    /// @param factory The factory type.
    /// @return A new world instance of factory.
    public static Option<GameObject> Spawn(GameObject factory)
    {
      try
      {
        var instance = Object.Instantiate(factory);
        return Option.Some(instance);
      }
      catch (Exception e)
      {
        Console.Error("Failed to load prefab: " + factory + ": " + e);
      }
      return Option.None<GameObject>();
    }

    /// Add a new resource prefab instance to the current scene and return it
    /// @param factory The factory type.
    /// @param parent The parent object for the new object.
    /// @return A new world instance of factory.
    public static Option<GameObject> Spawn(GameObject factory, GameObject parent)
    {
      var rtn = Spawn(factory);
      if (rtn)
      {
        parent.AddChild(rtn.Unwrap());
      }
      return rtn;
    }

    /// Add a new resource prefab instance to the current scene and return it
    /// @param factory The factory type.
    /// @param parent The parent object for the new object.
    /// @return A new world instance of factory.
    public static Option<GameObject> Spawn(GameObject factory, Component parent)
    {
      var rtn = Spawn(factory);
      if (rtn)
      {
        parent.gameObject.AddChild(rtn.Unwrap());
      }
      return rtn;
    }

    /// Find and return all GameObject instances which have the given component
    public static List<GameObject> Find<T>() where T : Component
    {
      var rtn = new List<GameObject>();
      foreach (var instance in Object.FindObjectsOfType(typeof(T)))
      {
        var tmp = instance as T;
        if (tmp != null)
        {
          rtn.Add(tmp.gameObject);
        }
      }
      return rtn;
    }

    /// Find and return the first GameObject instance which has the given component
    public static GameObject First<T>() where T : Component
    {
      var rtn = Object.FindObjectOfType(typeof(T));
      var tmp = rtn as Component;
      if (tmp != null)
      {
        return tmp.gameObject;
      }
      return null;
    }

    /// Find and return all instances of T in the scene.
    public static List<T> FindComponents<T>() where T : Component
    {
      var rtn = new List<T>();
      foreach (var instance in Object.FindObjectsOfType(typeof(T)))
      {
        rtn.Add(instance as T);
      }
      return rtn;
    }

    /// Find and return the first matching component
    public static T FindComponent<T>() where T : Component
    {
      return Object.FindObjectOfType(typeof(T)) as T;
    }

    /// Return first instance of T in the scene, use this sparingly and carefully.
    public static T Get<T>() where T : Component
    {
      return Object.FindObjectOfType(typeof(T)) as T;
    }

    /// Open a scene, dropping everything on this scene
    public static void Open(string sceneId, Deferred<bool, Exception> result, Option<Action<float>> onProgress, float wait = 5.0f)
    {
      var handle = new GameObject();
      var hook = handle.AddComponent<SceneLoader>();
      Object.DontDestroyOnLoad(handle);
      result.Promise.Then((r) => { Object.Destroy(handle); });
      hook.StartCoroutine(AsyncOpen(sceneId, result, onProgress, wait));
    }

    /// Open a scene, dropping everything on this scene
    /// Notice that a scene must be added in the 'Build Settings' menu to be added this way.
    /// Notice you do actually have to iterate over this enumerable to run the load;
    /// foreach (var s in Open(...)) { } def.Then(...);
    private static IEnumerator AsyncOpen(string sceneId, Deferred<bool, Exception> result, Option<Action<float>> onProgress, float maxWait = 5.0f)
    {
      AsyncOperation op = Application.CanStreamedLevelBeLoaded(sceneId) ? SceneManager.LoadSceneAsync(sceneId) : null;
      if (op != null)
      {
        op.allowSceneActivation = true;
        var isDone = false;
        var waited = 0f;
        var wait_step = 0.1f;
        while (!isDone)
        {
          // Determine if the StepPerSecond is finished; notice async load doesn't work properly.
          isDone = op.isDone;

          // Progress
          onProgress.Then((cb) => { cb(op.progress); });
          if (isDone)
          {
            result.Resolve(true);
          }
          else
          {
            // Not loaded yet? Wait for a bit and check again
            yield return new WaitForSeconds(wait_step);
            waited += wait_step;
            if (waited > maxWait)
            {
              result.Reject(new Exception(string.Format("Scene {0} failed to load after {1} seconds", sceneId, waited)));
              break;
            }
          }
        }
      }
      else
      {
        result.Reject(new Exception(string.Format("Unknown scene id: {0}", sceneId)));
      }
    }
  }

  /// Loader helper
  public class SceneLoader : MonoBehaviour
  {
  }
}