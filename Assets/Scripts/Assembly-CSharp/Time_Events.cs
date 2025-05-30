using System.Collections;
using UnityEngine;

[AddComponentMenu("Functions/Time/Time event")]
public class Time_Events : MonoBehaviour
{
	public bool StartYield;

	public bool destroyAfter;

	[SerializeField]
	public TimePoint[] EventsOnTime;

	private int i;

	private int StartNumYield;

	private void Start()
	{
		if (StartYield)
		{
			for (i = 0; i < EventsOnTime.Length; i++)
			{
				StartCoroutine(OneYieldStart(i));
			}
		}
		if (StartNumYield != 0)
		{
			OneYieldStart(StartNumYield);
		}
	}

	private IEnumerator OneYieldStart(int num)
	{
		yield return new WaitForSeconds(EventsOnTime[num].time);
		EventsOnTime[num]._event.Invoke();
		if (destroyAfter && num == EventsOnTime.Length)
		{
			Object.Destroy(base.gameObject);
		}
	}

	public void YieldRestart()
	{
		for (i = 0; i < EventsOnTime.Length; i++)
		{
			StartCoroutine(OneYieldStart(i));
		}
	}

	public void YieldOne(int num)
	{
		StartCoroutine(OneYieldStart(num));
	}

	public void YieldOneLoadAwake(int num)
	{
		StartNumYield = num;
	}
}
