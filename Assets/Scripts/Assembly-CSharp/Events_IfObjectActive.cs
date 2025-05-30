using UnityEngine;
using UnityEngine.Events;

public class Events_IfObjectActive : MonoBehaviour
{
	public GameObject ifObjectActive;

	public UnityEvent _event;

	public UnityEvent __eventNo;

	public bool onStart;

	public bool DestroyAfter;

	public void Start()
	{
		if (onStart)
		{
			VokeEvent();
		}
	}

	public void VokeEvent()
	{
		if (ifObjectActive != null)
		{
			if (ifObjectActive.activeInHierarchy)
			{
				_event.Invoke();
			}
			if (!ifObjectActive.activeInHierarchy)
			{
				__eventNo.Invoke();
			}
		}
		if (ifObjectActive == null)
		{
			__eventNo.Invoke();
		}
		if (DestroyAfter)
		{
			Object.Destroy(this);
		}
	}
}
