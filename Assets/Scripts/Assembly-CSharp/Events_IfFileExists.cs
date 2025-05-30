using System.IO;
using UnityEngine;
using UnityEngine.Events;

public class Events_IfFileExists : MonoBehaviour
{
	public string file;

	public UnityEvent eventStart;

	public bool none;

	public bool createIfNone;

	private void Start()
	{
		if (File.Exists("Data/Save/" + file) == !none)
		{
			eventStart.Invoke();
		}
		if (!File.Exists("Data/Save/" + file) && createIfNone)
		{
			File.Create("Data/Save/" + file);
		}
	}
}
