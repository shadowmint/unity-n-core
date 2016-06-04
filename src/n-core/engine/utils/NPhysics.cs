using UnityEngine;

namespace N
{
    /// Physics helpers
    public class NPhysics
    {
        /// Check if there are any matching ground collisions from the root object.
        /// Literally sphere cast from the position of the query root maxDistance.
        /// If the sphere cast size is baseHalfSize.
        /// Layers is a list of layer ids; it is converted into a layer mask.
        public static bool IsGrounded(GameObject root, Vector3 down, float maxDistance, float baseHalfSize, int[] layers)
        {
            RaycastHit hitInfo;
            var layerMask = LayerMask(layers);
            return Physics.SphereCast(
                root.transform.position,
                baseHalfSize,
                down,
                out hitInfo,
                maxDistance,
                layerMask);
        }

        /// Get a layer mask from a layers array or 0
        public static int LayerMask(int[] layers)
        {
            var mask = 0;
            if ((layers != null) && (layers.Length > 0))
            {
                for (var i = 0; i < layers.Length; ++i)
                {
                    mask |= 1 << layers[i];
                }
            }
            return mask;
        }
    }
}
