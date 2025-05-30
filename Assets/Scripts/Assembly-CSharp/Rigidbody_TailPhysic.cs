using UnityEngine;

public class Rigidbody_TailPhysic : MonoBehaviour
{
	public bool activeOnStart = true;

	public GameObject[] tailObjects;

	private Vector3[] positionTail = new Vector3[25];

	private Quaternion[] rotationTail = new Quaternion[25];

	private void Start()
	{
		if (activeOnStart)
		{
			for (int i = 0; i < tailObjects.Length; i++)
			{
				positionTail[i] = tailObjects[i].transform.localPosition;
				rotationTail[i] = tailObjects[i].transform.localRotation;
			}
			for (int j = 0; j < tailObjects.Length; j++)
			{
				tailObjects[j].transform.SetParent(null);
			}
		}
	}

	public void ResetTail()
	{
		for (int i = 0; i < tailObjects.Length; i++)
		{
			if (i == 0)
			{
				tailObjects[i].transform.SetParent(base.transform);
			}
			if (i != 0)
			{
				tailObjects[i].transform.SetParent(tailObjects[i - 1].transform);
			}
		}
		for (int j = 0; j < tailObjects.Length; j++)
		{
			tailObjects[j].transform.localPosition = positionTail[j];
			tailObjects[j].transform.localRotation = rotationTail[j];
		}
		for (int k = 0; k < tailObjects.Length; k++)
		{
			tailObjects[k].transform.SetParent(null);
		}
	}

	public void ActiveStart()
	{
		activeOnStart = true;
		Start();
	}
}
