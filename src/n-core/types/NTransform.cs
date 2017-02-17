using System;
using UnityEngine;

namespace N.Package.Core
{
  /// A cloneable simple representation of a Transform component
  [System.Serializable]
  public struct NTransform
  {
    /// The position
    public Vector3 Position;

    /// The rotation
    public Quaternion Rotation;

    /// The local scale
    public Vector3 Scale;

    /// Create from game object
    public NTransform(GameObject target)
    {
      Position = target.transform.position;
      Scale = target.transform.localScale;
      Rotation = target.transform.rotation;
    }

    /// Create from transform
    public NTransform(Transform transform)
    {
      Position = transform.position;
      Scale = transform.localScale;
      Rotation = transform.rotation;
    }

    /// Lerp
    public static NTransform Lerp(NTransform a, NTransform b, float value)
    {
      const float tolerance = 0.0001f;
      if (Math.Abs(value) < tolerance)
      {
        return a;
      }
      if (Math.Abs(value - 1f) < tolerance)
      {
        return b;
      }

      var rtn = default(NTransform);
      rtn.Position = Vector3.Lerp(a.Position, b.Position, value);
      rtn.Scale = Vector3.Lerp(a.Scale, b.Scale, value);
      rtn.Rotation = Quaternion.Lerp(a.Rotation, b.Rotation, value);
      return rtn;
    }
  }
}