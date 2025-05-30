using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class SwitchObjectEditor
{
	public GameObject[] deactive;

	public GameObject[] active;

	public UnityEvent _event;
}
