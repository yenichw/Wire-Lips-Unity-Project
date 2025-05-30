using UnityEngine;

public class UI_HideButtonDev : MonoBehaviour
{
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Z) && Input.GetKey(KeyCode.X))
		{
			GetComponent<Canvas>().enabled = !GetComponent<Canvas>().enabled;
		}
	}
}
