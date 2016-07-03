using UnityEngine;
using N.Tests;
using N;
using System.Collections.Generic;

namespace N
{
    /// This component is for location and element in a UI heirarchy.
    [AddComponentMenu("N/Marker")]
    public class Marker : MonoBehaviour
    {

        [Tooltip("The tag on this element that makes it searchable")]
        public string markerTag;

        /// Find an element with a given tag
        public static Option<GameObject> Find(string tag, GameObject heirarchy)
        {
            if (heirarchy != null) {
                foreach (var instance in heirarchy.GetComponentsInChildren<Marker>())
                {
                    if (instance.markerTag == tag)
                    {
                        return Option.Some(instance.gameObject);
                    }
                }
            }
            return Option.None<GameObject>();
        }

        /// Find all elements with a given tag
        public static IEnumerable<GameObject> FindAll(string tag, GameObject heirarchy)
        {
            if (heirarchy != null) {
                foreach (var instance in heirarchy.GetComponentsInChildren<Marker>())
                {
                    if (instance.markerTag == tag)
                    {
                        yield return instance.gameObject;
                    }
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
                var instance = Marker.Find(tag, heirarchy);
                if (instance)
                {
                    rtn[tag] = instance.Unwrap();
                    count += 1;
                }
                else
                {
                    N.Console.Error("Some UI tags were missing: {0}", tag);
                }
            }
            if (count == tags.Length)
            {
                return Option.Some(rtn);
            }
            return Option.None<Dictionary<string, GameObject>>();
        }
    }
}
