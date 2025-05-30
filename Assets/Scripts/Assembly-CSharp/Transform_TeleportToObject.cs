using UnityEngine;

public class Transform_TeleportToObject : MonoBehaviour
{
	public Transform toObject;

	public Vector3 posTransformForward;

	public Transform lookRotation;

	private void Start()
	{
		base.transform.position = toObject.position + toObject.right * posTransformForward.x + toObject.up * posTransformForward.y + toObject.forward * posTransformForward.z;
		base.transform.rotation = Quaternion.LookRotation(toObject.position - base.transform.position, Vector3.up);
	}
}
