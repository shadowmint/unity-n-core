using UnityEngine;

namespace N
{
	/// A cloneable simple representation of a Transform component
	[System.Serializable]
	public struct NTransform
	{
		/// The position
		public Vector3 position;

		/// The rotation
		public Quaternion rotation;

		/// The local scale
		public Vector3 scale;

		// the forward vector of this transform
		public Vector3 normal;

		/// Create from game object
		public NTransform(GameObject target)
		{
			position = target.transform.position;
			scale = target.transform.localScale;
			rotation = target.transform.rotation;
			normal = target.transform.forward;
		}

		/// Create from transform
		public NTransform(Transform transform)
		{
			position = transform.position;
			scale = transform.localScale;
			rotation = transform.rotation;
			normal = transform.forward;
		}

		/// Lerp
		public static NTransform Lerp(NTransform a, NTransform b, float value)
		{
			if (value == 0f)
			{
				return a;
			}
			else if (value == 1f)
			{
				return b;
			}
			else
			{
				var rtn = default(NTransform);
				rtn.position = Vector3.Lerp(a.position, b.position, value);
				rtn.scale = Vector3.Lerp(a.scale, b.scale, value);
				rtn.rotation = Quaternion.Lerp(a.rotation, b.rotation, value);
				rtn.normal = Vector3.Lerp(a.normal, b.normal, value);
				rtn.normal.Normalize();
				return rtn;
			}
		}
	}
}
