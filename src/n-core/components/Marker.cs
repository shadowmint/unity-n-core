using UnityEngine;
using System.Collections.Generic;

namespace N.Package.Core
{
  /// This component is for location and element in a UI heirarchy.
  [AddComponentMenu("N/Marker")]
  public class Marker : MonoBehaviour
  {
    [Tooltip("The tag on this element that makes it searchable")]
    public string MarkerTag;

    /// Find an element with a given tag
    public static Option<GameObject> Find(string tag, GameObject heirarchy)
    {
      if (heirarchy == null) return Option.None<GameObject>();
      foreach (var instance in heirarchy.GetComponentsInChildren<Marker>())
      {
        if (instance.MarkerTag == tag)
        {
          return Option.Some(instance.gameObject);
        }
      }
      return Option.None<GameObject>();
    }

    /// Find all elements with a given tag
    public static IEnumerable<GameObject> FindAll(string tag, GameObject heirarchy)
    {
      if (heirarchy == null) yield break;
      foreach (var instance in heirarchy.GetComponentsInChildren<Marker>())
      {
        if (instance.MarkerTag == tag)
        {
          yield return instance.gameObject;
        }
      }
    }

    /// Populate a dictionary of objects from a heirarchy
    public static Option<Dictionary<string, GameObject>> Find(string[] tags, GameObject heirarchy)
    {
      var rtn = new Dictionary<string, GameObject>();
      var count = 0;
      foreach (var tag in tags)
      {
        var instance = Find(tag, heirarchy);
        if (instance)
        {
          rtn[tag] = instance.Unwrap();
          count += 1;
        }
        else
        {
          Console.Error("Some UI tags were missing: {0}", tag);
        }
      }
      return count == tags.Length ? Option.Some(rtn) : Option.None<Dictionary<string, GameObject>>();
    }
  }
}