using UnityEngine;
using UnityEngine.Events;

[AddComponentMenu("Functions/Event/Data")]
public class Events_Data : MonoBehaviour
{
	[Header("eventVoke - int")]
	public UnityEvent[] _event;

	public void eventVoke(int x)
	{
		ConsoleMain.ConsolePrint("Invoke Event[" + x + "] (" + base.gameObject.name + ")");
		_event[x].Invoke();
	}
}
