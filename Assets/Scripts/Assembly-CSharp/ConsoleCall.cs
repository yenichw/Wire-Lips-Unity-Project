using UnityEngine;

public class ConsoleCall : MonoBehaviour
{
	public GameObject consoleObject;

	public bool keyO;

	private GameObject consoleObj;

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.BackQuote) && consoleObj == null)
		{
			if (keyO && Input.GetKey(KeyCode.O))
			{
				consoleObj = Object.Instantiate(consoleObject);
			}
			if (!keyO)
			{
				consoleObj = Object.Instantiate(consoleObject);
			}
		}
	}
}
