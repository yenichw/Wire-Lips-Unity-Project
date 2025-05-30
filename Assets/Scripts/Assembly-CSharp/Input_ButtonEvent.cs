using UnityEngine;
using UnityEngine.Events;

[AddComponentMenu("Functions/Input/Button Event")]
public class Input_ButtonEvent : MonoBehaviour
{
	public bool active = true;

	public UnityEvent eventDown;

	public UnityEvent eventIdle;

	public UnityEvent eventUp;

	public string key = "Space";

	public bool one = true;

	private bool wasDown;

	private void Update()
	{
		if (!active)
		{
			return;
		}
		if (Input.GetButtonDown(key))
		{
			eventDown.Invoke();
			wasDown = true;
		}
		if (!wasDown)
		{
			return;
		}
		if (Input.GetButton(key))
		{
			eventIdle.Invoke();
		}
		if (Input.GetButtonUp(key))
		{
			eventUp.Invoke();
			if (one)
			{
				active = false;
			}
			wasDown = false;
		}
	}

	public void Activation(bool x)
	{
		active = x;
	}
}
