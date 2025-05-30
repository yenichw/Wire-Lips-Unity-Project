using UnityEngine;

public class Transform_HoldPosition : MonoBehaviour
{
	public bool startTransformTarget = true;

	public Transform target;

	public Vector3 localPosition;

	public Quaternion localRotation;

	public TargetPosition[] otherTargets;

	private Transform myFirstParent;

	private Vector3 myFirstPosition;

	private Quaternion myFirstRotation;

	private bool fs;

	private void Start()
	{
		if (!fs)
		{
			fs = true;
			myFirstParent = base.transform.parent;
			myFirstPosition = base.transform.position;
			myFirstRotation = base.transform.rotation;
			if (startTransformTarget)
			{
				base.transform.parent = target;
				base.transform.localPosition = localPosition;
				base.transform.localRotation = localRotation;
			}
		}
	}

	public void ReTarget(int x)
	{
		Start();
		if (x != -1)
		{
			base.transform.parent = otherTargets[x].target;
			base.transform.localPosition = otherTargets[x].localPosition;
			base.transform.localRotation = otherTargets[x].localRotation;
		}
		else
		{
			base.transform.parent = target;
			base.transform.localPosition = localPosition;
			base.transform.localRotation = localRotation;
		}
	}

	[ContextMenu("Show local")]
	private void LocalShow()
	{
		localPosition = base.transform.localPosition;
		localRotation = base.transform.localRotation;
	}

	public void PositionStart()
	{
		base.transform.parent = myFirstParent;
		base.transform.position = myFirstPosition;
		base.transform.rotation = myFirstRotation;
	}
}
