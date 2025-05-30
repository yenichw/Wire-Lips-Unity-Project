using System;
using UnityEngine;

[Serializable]
public class DoorLockPrefab
{
	public GameObject prefabOnShoal;

	public GameObject prefabOnDoor;

	public bool onlyOne;

	public int numberStringText;

	public GameObject[] inputItem;

	[HideInInspector]
	public bool lockOnlyOneUsed;
}
