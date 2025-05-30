using UnityEngine;

public class Interactive_Cases : MonoBehaviour
{
	public GameObject[] items;

	public void OpenCase()
	{
		GameObject.FindWithTag("GameController").GetComponent<Interface_MainPlayer>().InventoryCase(GetComponent<Interactive_Cases>(), active: true);
	}

	public void OpenChest()
	{
		GameObject.FindWithTag("GameController").GetComponent<Interface_MainPlayer>().InventoryChest(GetComponent<Interactive_Cases>(), active: true);
	}
}
