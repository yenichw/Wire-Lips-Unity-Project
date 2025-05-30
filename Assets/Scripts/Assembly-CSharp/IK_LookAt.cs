using RootMotion.FinalIK;
using UnityEngine;

public class IK_LookAt : MonoBehaviour
{
	public bool active;

	[Range(0f, 1f)]
	public float speedLook = 0.1f;

	[Range(0f, 1f)]
	public float speedingLook;

	private float spd;

	private LookAtIK ltIK;

	private void Start()
	{
		ltIK = GetComponent<LookAtIK>();
		spd = speedLook;
	}

	private void Update()
	{
		if (active)
		{
			if (speedingLook == 0f)
			{
				ltIK.solver.IKPositionWeight = Mathf.Lerp(ltIK.solver.IKPositionWeight, 1f, spd);
				return;
			}
			spd = Mathf.Lerp(spd, speedLook, speedingLook);
			ltIK.solver.IKPositionWeight = Mathf.Lerp(ltIK.solver.IKPositionWeight, 1f, spd);
		}
		else if (speedingLook == 0f)
		{
			ltIK.solver.IKPositionWeight = Mathf.Lerp(ltIK.solver.IKPositionWeight, 0f, spd);
		}
		else
		{
			spd = Mathf.Lerp(spd, speedLook, speedingLook);
			ltIK.solver.IKPositionWeight = Mathf.Lerp(ltIK.solver.IKPositionWeight, 0f, spd);
		}
	}

	public void LookActivation(bool x)
	{
		active = x;
		if (speedingLook != 0f)
		{
			spd = 0f;
		}
	}
}
