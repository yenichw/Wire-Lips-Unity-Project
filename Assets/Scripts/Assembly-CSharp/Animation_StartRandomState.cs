using UnityEngine;

public class Animation_StartRandomState : MonoBehaviour
{
	[Range(0f, 1f)]
	public float minTime;

	[Range(0f, 1f)]
	public float maxTime = 1f;

	public string[] nameStates;

	public bool dontDestroyStart;

	private void OnEnable()
	{
		int num = Random.Range(0, nameStates.Length);
		GetComponent<Animator>().Play(nameStates[num], -1, Random.Range(minTime, maxTime));
		if (!dontDestroyStart)
		{
			Object.Destroy(this);
		}
	}
}
