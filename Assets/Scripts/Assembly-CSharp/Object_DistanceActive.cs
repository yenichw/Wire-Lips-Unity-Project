using System;
using UnityEngine;

public class Object_DistanceActive : MonoBehaviour
{
	[Serializable]
	public class distanceObjects
	{
		public float distance;

		public GameObject[] obejcts;
	}

	public distanceObjects[] _distance;

	public Transform objectPlayer;

	private bool[] boolDistance;

	private void Start()
	{
		if (objectPlayer == null)
		{
			objectPlayer = GameObject.FindWithTag("Player").gameObject.transform;
		}
		boolDistance = new bool[_distance.Length];
	}

	private void Update()
	{
		if (_distance.Length == 0)
		{
			return;
		}
		for (int i = 0; i < _distance.Length; i++)
		{
			if (Vector3.Distance(objectPlayer.position, base.transform.position) <= _distance[i].distance && !boolDistance[i])
			{
				for (int j = 0; j < _distance[i].obejcts.Length; j++)
				{
					_distance[i].obejcts[j].SetActive(value: true);
				}
				boolDistance[i] = true;
			}
			if (Vector3.Distance(objectPlayer.position, base.transform.position) > _distance[i].distance && boolDistance[i])
			{
				for (int k = 0; k < _distance[i].obejcts.Length; k++)
				{
					_distance[i].obejcts[k].SetActive(value: false);
				}
				boolDistance[i] = false;
			}
		}
	}

	private void OnDrawGizmosSelected()
	{
		if (_distance.Length != 0)
		{
			Gizmos.color = new Color(1f, 1f, 1f, 0.2f);
			for (int i = 0; i < _distance.Length; i++)
			{
				Gizmos.DrawSphere(base.transform.position, _distance[i].distance);
			}
		}
	}
}
