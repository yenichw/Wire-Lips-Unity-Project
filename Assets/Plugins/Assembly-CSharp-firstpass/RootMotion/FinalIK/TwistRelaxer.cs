using UnityEngine;

namespace RootMotion.FinalIK
{
	public class TwistRelaxer : MonoBehaviour
	{
		[Tooltip("The weight of relaxing the twist of this Transform")]
		[Range(0f, 1f)]
		public float weight = 1f;

		[Tooltip("If 0.5, this Transform will be twisted half way from parent to child. If 1, the twist angle will be locked to the child and will rotate with along with it.")]
		[Range(0f, 1f)]
		public float parentChildCrossfade = 0.5f;

		private Vector3 twistAxis = Vector3.right;

		private Vector3 axis = Vector3.forward;

		private Vector3 axisRelativeToParentDefault;

		private Vector3 axisRelativeToChildDefault;

		private Transform parent;

		private Transform child;

		public void Relax()
		{
			if (!(weight <= 0f))
			{
				Vector3 a = parent.rotation * axisRelativeToParentDefault;
				Vector3 b = child.rotation * axisRelativeToChildDefault;
				Vector3 vector = Vector3.Slerp(a, b, parentChildCrossfade);
				vector = Quaternion.Inverse(Quaternion.LookRotation(base.transform.rotation * axis, base.transform.rotation * twistAxis)) * vector;
				float num = Mathf.Atan2(vector.x, vector.z) * 57.29578f;
				Quaternion rotation = child.rotation;
				base.transform.rotation = Quaternion.AngleAxis(num * weight, base.transform.rotation * twistAxis) * base.transform.rotation;
				child.rotation = rotation;
			}
		}

		private void Start()
		{
			parent = base.transform.parent;
			if (base.transform.childCount == 0)
			{
				Debug.LogError("The Transform of a TwistRelaxer has no children. Can not use TwistRelaxer on that bone.");
				return;
			}
			child = base.transform.GetChild(0);
			twistAxis = base.transform.InverseTransformDirection(child.position - base.transform.position);
			axis = new Vector3(twistAxis.y, twistAxis.z, twistAxis.x);
			Vector3 vector = base.transform.rotation * axis;
			axisRelativeToParentDefault = Quaternion.Inverse(parent.rotation) * vector;
			axisRelativeToChildDefault = Quaternion.Inverse(child.rotation) * vector;
		}

		private void LateUpdate()
		{
			Relax();
		}
	}
}
