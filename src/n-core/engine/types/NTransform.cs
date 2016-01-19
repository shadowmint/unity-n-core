using UnityEngine;

namespace N
{
    /// A cloneable simple representation of a Transform component
    public struct NTransform
    {
        /// The position
        public Vector3 position;

        /// The rotation
        public Quaternion rotation;

        /// The local scale
        public Vector3 scale;

        /// Create from game object
        public NTransform(GameObject target)
        {
            position = target.transform.position;
            scale = target.transform.localScale;
            rotation = target.transform.rotation;
        }

        /// Create from transform
        public NTransform(Transform transform)
        {
            position = transform.position;
            scale = transform.localScale;
            rotation = transform.rotation;
        }
    }
}
