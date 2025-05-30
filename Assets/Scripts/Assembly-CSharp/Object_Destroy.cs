using UnityEngine;
using UnityEngine.Events;

[AddComponentMenu("Functions/Object/Object destroy")]
public class Object_Destroy : MonoBehaviour
{
	public UnityEvent eventDestroy;

	public void destroy()
	{
		ConsoleMain.ConsolePrint("Destroy (" + base.gameObject.name + ")");
		eventDestroy.Invoke();
		Object.Destroy(base.gameObject);
	}
}
