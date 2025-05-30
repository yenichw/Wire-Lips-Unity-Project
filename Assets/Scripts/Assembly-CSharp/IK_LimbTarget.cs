using RootMotion.FinalIK;
using UnityEngine;

public class IK_LimbTarget : MonoBehaviour
{
	public LimbIK limb;

	public bool action;

	public bool rotation = true;

	[Range(0f, 1f)]
	public float speed;

	[Range(0f, 1f)]
	public float weight;

	private void Start()
	{
		if (limb == null)
		{
			limb = GetComponent<LimbIK>();
		}
		if (weight == 0f)
		{
			weight = 1f;
		}
	}

	private void FixedUpdate()
	{
		if (action)
		{
			limb.solver.IKPositionWeight = Mathf.Lerp(limb.solver.IKPositionWeight, weight, speed);
			if (rotation)
			{
				limb.solver.IKRotationWeight = Mathf.Lerp(limb.solver.IKRotationWeight, weight, speed);
			}
		}
		else
		{
			limb.solver.IKPositionWeight = Mathf.Lerp(limb.solver.IKPositionWeight, 0f, speed);
			if (rotation)
			{
				limb.solver.IKRotationWeight = Mathf.Lerp(limb.solver.IKRotationWeight, 0f, speed);
			}
		}
	}

	public void Activation(bool x)
	{
		action = x;
	}
}
