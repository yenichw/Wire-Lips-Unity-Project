using UnityEngine;
using UnityEngine.Events;

public class Interactive_ActionReEvent : MonoBehaviour
{
	public UnityEvent _event;

	public void ReEvent(Interactive_Action _obj)
	{
		_obj._event = _event;
	}
}
