using UnityEngine;

namespace N {

  /// A cloneable simple representation of a Transform component
  public struct NTransform
  {
    /// The position
    public Vector3 position;

    /// The rotation
    public Quaternion rotation;

    /// The local scale
    public Vector3 scale;
  }
}
