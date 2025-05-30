using UnityEngine;
using UnityEngine.Events;

public class Triggers_Mouse : MonoBehaviour
{
	public float time;

	public UnityEvent _event;

	public bool destroyAfter = true;

	private bool mouseActive;

	private bool eventActive;

	private void OnMouseEnter()
	{
		mouseActive = true;
	}

	private void OnMouseExit()
	{
		mouseActive = false;
	}

	private void Update()
	{
		if (!mouseActive || (!(time > 0f) && eventActive))
		{
			return;
		}
		time -= Time.deltaTime;
		if (time <= 0f)
		{
			time = 0f;
			_event.Invoke();
			eventActive = true;
			if (destroyAfter)
			{
				Object.Destroy(base.gameObject);
			}
		}
	}
}
