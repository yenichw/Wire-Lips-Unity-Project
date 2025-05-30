using UnityEngine;

public class Transform_RotationTarget : MonoBehaviour
{
	public Transform target;

	public bool xFreeze;

	public float xFreezeRotation;

	public bool yFreeze;

	public float yFreezeRotation;

	public bool zFreeze;

	public float zFreezeRotation;

	private void Update()
	{
		Quaternion quaternion = Quaternion.LookRotation(base.transform.position - target.position, Vector3.up);
		if (!xFreeze)
		{
			xFreezeRotation = quaternion.eulerAngles.x;
		}
		if (!yFreeze)
		{
			yFreezeRotation = quaternion.eulerAngles.y;
		}
		if (!zFreeze)
		{
			zFreezeRotation = quaternion.eulerAngles.z;
		}
		base.transform.rotation = Quaternion.Euler(xFreezeRotation, yFreezeRotation, zFreezeRotation);
	}
}
