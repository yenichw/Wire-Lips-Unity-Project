using UnityEngine;

public class Item_ItemInventory : MonoBehaviour
{
	public string fileText = "Item 1";

	public int nameItem = 1;

	public Sprite icon;

	[Header("Special")]
	public bool specialItem;

	public bool canCombine;
}
