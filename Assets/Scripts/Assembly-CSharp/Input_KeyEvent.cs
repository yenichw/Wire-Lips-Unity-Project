using UnityEngine;
using UnityEngine.Events;

[AddComponentMenu("Functions/Input/Key Event")]
public class Input_KeyEvent : MonoBehaviour
{
	public UnityEvent eventDown;

	public UnityEvent eventIdle;

	public UnityEvent eventUp;

	public KeyCode key;

	private void Update()
	{
		if (Input.GetKeyDown(key))
		{
			eventDown.Invoke();
		}
		if (Input.GetKey(key))
		{
			eventIdle.Invoke();
		}
		if (Input.GetKeyUp(key))
		{
			eventUp.Invoke();
		}
	}
}
