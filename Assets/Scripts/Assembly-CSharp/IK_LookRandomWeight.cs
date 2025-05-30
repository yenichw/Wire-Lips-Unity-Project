using RootMotion.FinalIK;
using UnityEngine;

public class IK_LookRandomWeight : MonoBehaviour
{
	public int random = 100;

	private bool lookActive;

	private LookAtIK ltIK;

	private void Start()
	{
		ltIK = GetComponent<LookAtIK>();
	}

	private void Update()
	{
		if (Random.Range(0, random) == 0)
		{
			lookActive = !lookActive;
		}
		if (lookActive)
		{
			ltIK.solver.IKPositionWeight = Mathf.Lerp(ltIK.solver.IKPositionWeight, 1f, Time.deltaTime * 5f);
		}
		else
		{
			ltIK.solver.IKPositionWeight = Mathf.Lerp(ltIK.solver.IKPositionWeight, 0f, Time.deltaTime * 5f);
		}
	}
}
