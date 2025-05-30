using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Interface_MyInventory : MonoBehaviour
{
	public RectTransform change;

	public GameObject prefabInventoryEmpty;

	public Text textUse;

	public Text textInspect;

	public GameObject objectShowItem;

	private GameObject[] casesItem = new GameObject[100];

	private GameObject exampleCaseItem;

	private bool h;

	private int itemChange;

	private Interface_MainPlayer scrIntMain;

	private void OnEnable()
	{
		scrIntMain = GameObject.FindWithTag("GameController").GetComponent<Interface_MainPlayer>();
		if (scrIntMain.itemCount != 0)
		{
			exampleCaseItem = base.transform.Find("InventoryMy/Case").gameObject;
			for (int i = 0; i < 10; i++)
			{
				casesItem[i] = Object.Instantiate(exampleCaseItem, base.transform.Find("InventoryMy").transform);
				casesItem[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(45f + (float)i * 60.5f, -22f);
				if (scrIntMain.itemsDataNow[i].itemObject != null)
				{
					casesItem[i].transform.Find("Icon").gameObject.GetComponent<Image>().sprite = scrIntMain.itemsDataNow[i].itemObject.GetComponent<Item_ItemInventory>().icon;
					if (scrIntMain.itemsDataNow[i].itemObject.GetComponent<Item_ItemInventory>().specialItem)
					{
						casesItem[i].GetComponent<Image>().color = new Color(0.35f, 0.35f, 0.35f, 1f);
						if (!scrIntMain.itemsDataNow[i].itemObject.GetComponent<Item_ItemInventory>().canCombine)
						{
							casesItem[i].transform.Find("ImageNumber").gameObject.SetActive(value: false);
						}
					}
				}
				else
				{
					casesItem[i].transform.Find("Icon").gameObject.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0f);
					casesItem[i].transform.Find("ImageNumber").gameObject.SetActive(value: false);
				}
			}
			Object.Destroy(exampleCaseItem);
			ChangeItem();
		}
		else
		{
			Object.Instantiate(prefabInventoryEmpty, base.transform.parent);
			scrIntMain.inventory = false;
			Object.Destroy(base.gameObject);
		}
	}

	private void Update()
	{
		if (Input.GetAxis("Horizontal") > 0f && !h)
		{
			Right();
			h = true;
		}
		if (Input.GetAxis("Horizontal") < 0f && !h)
		{
			h = true;
			Left();
		}
		if (Input.GetAxis("Horizontal") == 0f)
		{
			h = false;
		}
		if (Input.GetButtonDown("Show"))
		{
			ShowItem();
		}
		if (Input.GetButtonDown("Cancel"))
		{
			Object.Destroy(base.gameObject);
		}
	}

	private void Right()
	{
		itemChange++;
		if (itemChange > 9)
		{
			itemChange = 0;
		}
		ChangeItem();
	}

	private void ChangeItem()
	{
		casesItem[itemChange].GetComponent<RectTransform>();
		if (scrIntMain.itemsDataNow[itemChange].itemObject != null)
		{
			if (!scrIntMain.itemsDataNow[itemChange].itemObject.GetComponent<Item_ItemInventory>().specialItem)
			{
				textUse.color = new Color(1f, 1f, 1f, 1f);
			}
			else
			{
				textUse.color = new Color(1f, 1f, 1f, 0.5f);
			}
			if (scrIntMain.itemsDataNow[itemChange].itemObject.GetComponent<Item_ItemInventory>().specialItem)
			{
				textInspect.color = new Color(1f, 1f, 1f, 1f);
			}
			else
			{
				textInspect.color = new Color(1f, 1f, 1f, 0.5f);
			}
		}
		change.anchoredPosition = new Vector2(casesItem[itemChange].GetComponent<RectTransform>().anchoredPosition.x - 2.5f, casesItem[itemChange].GetComponent<RectTransform>().anchoredPosition.y + 2.5f);
	}

	private void Left()
	{
		itemChange--;
		if (itemChange < 0)
		{
			itemChange = 9;
		}
		ChangeItem();
	}

	private void ShowItem()
	{
		GameObject obj = Object.Instantiate(objectShowItem);
		obj.GetComponent<Interface_ShowItem>().objectShow = scrIntMain.itemsDataNow[itemChange].dataShow;
		obj.GetComponent<Interface_ShowItem>().backMyInventory = true;
		obj.GetComponent<Interface_ShowItem>().textItem = File.ReadAllLines("Data/Languages/" + GlobalGame.Language + "/" + scrIntMain.itemsDataNow[itemChange].itemObject.GetComponent<Item_ItemInventory>().fileText + ".txt")[scrIntMain.itemsDataNow[itemChange].itemObject.GetComponent<Item_ItemInventory>().nameItem - 1].ToUpper();
		Object.Destroy(base.gameObject);
	}
}
