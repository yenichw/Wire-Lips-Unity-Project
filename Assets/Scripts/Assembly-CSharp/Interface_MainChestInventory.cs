using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Interface_MainChestInventory : MonoBehaviour
{
	[Header("General inventory")]
	public GameObject objectShowItem;

	public Text textUse;

	public Text textInspect;

	[Header("My inventory")]
	public RectTransform changeMyInventory;

	[Header("Case inventory")]
	public RectTransform changeCaseInventory;

	[HideInInspector]
	public Interactive_Cases objectCase;

	private GameObject[] casesMyItem = new GameObject[100];

	private GameObject[] casesCaseItem = new GameObject[100];

	private bool hKey;

	private bool vKey;

	private bool inventoryUp;

	private int itemChange;

	private Interface_MainPlayer scrIntMain;

	private void Start()
	{
		scrIntMain = GameObject.FindWithTag("GameController").GetComponent<Interface_MainPlayer>();
		inventoryUp = true;
		float num = 45f;
		float num2 = -25f;
		GameObject gameObject = base.transform.Find("InventoryMy/Case").gameObject;
		for (int i = 0; i < 10; i++)
		{
			casesMyItem[i] = Object.Instantiate(gameObject, base.transform.Find("InventoryMy").transform);
			casesMyItem[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(num, -22f);
			if (scrIntMain.itemsDataNow[i].itemObject != null)
			{
				casesMyItem[i].transform.Find("Icon").gameObject.GetComponent<Image>().sprite = scrIntMain.itemsDataNow[i].itemObject.GetComponent<Item_ItemInventory>().icon;
				if (scrIntMain.itemsDataNow[i].itemObject.GetComponent<Item_ItemInventory>().specialItem && !scrIntMain.itemsDataNow[i].itemObject.GetComponent<Item_ItemInventory>().canCombine)
				{
					Object.Destroy(casesMyItem[i].transform.Find("ImageNumber").gameObject);
				}
			}
			else
			{
				casesMyItem[i].transform.Find("Icon").gameObject.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0f);
				casesMyItem[i].transform.Find("ImageNumber").gameObject.SetActive(value: false);
			}
			num += 60.5f;
		}
		Object.Destroy(gameObject);
		GameObject gameObject2 = base.transform.Find("InventoryCase/Case").gameObject;
		num = 45f;
		for (int j = 0; j < objectCase.items.Length; j++)
		{
			casesCaseItem[j] = Object.Instantiate(gameObject2, base.transform.Find("InventoryMy").transform);
			casesCaseItem[j].transform.SetParent(base.transform.Find("InventoryCase").transform);
			casesCaseItem[j].GetComponent<RectTransform>().anchoredPosition = new Vector2(num, num2);
			if (objectCase.items[j] != null)
			{
				casesCaseItem[j].transform.Find("Icon").gameObject.GetComponent<Image>().sprite = objectCase.items[j].GetComponent<Item_ItemInventory>().icon;
				if (objectCase.items[j].GetComponent<Item_ItemInventory>().specialItem && !objectCase.items[j].GetComponent<Item_ItemInventory>().canCombine)
				{
					casesCaseItem[j].transform.Find("ImageNumber").gameObject.SetActive(value: false);
				}
			}
			else
			{
				casesCaseItem[j].transform.Find("ImageNumber").gameObject.SetActive(value: false);
				casesCaseItem[j].transform.Find("Icon").gameObject.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0f);
			}
			num += 60.5f;
			if (num > 590f)
			{
				num = 45f;
				num2 -= 70f;
			}
		}
		Object.Destroy(gameObject2);
		ChangeItem();
	}

	private void Update()
	{
		if (Input.GetAxis("Horizontal") > 0f && !hKey)
		{
			hKey = true;
			Right();
		}
		if (Input.GetAxis("Horizontal") < 0f && !hKey)
		{
			hKey = true;
			Left();
		}
		if (Input.GetAxis("Horizontal") == 0f)
		{
			hKey = false;
		}
		if (Input.GetAxis("Vertical") > 0f && !vKey)
		{
			vKey = true;
			Up();
		}
		if (Input.GetAxis("Vertical") < 0f && !vKey)
		{
			vKey = true;
			Down();
		}
		if (Input.GetAxis("Vertical") == 0f)
		{
			vKey = false;
		}
		if (Input.GetButtonDown("Show"))
		{
			ShowItem();
		}
		if (Input.GetButtonDown("Cancel"))
		{
			GameObject.FindWithTag("GameController").GetComponent<Interface_MainPlayer>().InventoryCase(null, active: false);
		}
		if (Input.GetButton("Redrop"))
		{
			if (inventoryUp)
			{
				if (objectCase.items[itemChange] != null)
				{
					changeCaseInventory.Find("Drop").GetComponent<Image>().fillAmount += Time.deltaTime;
					if (changeCaseInventory.Find("Drop").GetComponent<Image>().fillAmount >= 1f)
					{
						changeCaseInventory.Find("Drop").GetComponent<Image>().fillAmount = 0f;
						bool flag = false;
						for (int i = 0; i < 10; i++)
						{
							if (!flag && scrIntMain.itemsDataNow[i].itemObject == null)
							{
								scrIntMain.ItemAdd(objectCase.items[itemChange]);
								casesMyItem[i].transform.Find("Icon").gameObject.GetComponent<Image>().sprite = scrIntMain.itemsDataNow[i].itemObject.GetComponent<Item_ItemInventory>().icon;
								casesMyItem[i].transform.Find("Icon").gameObject.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
								objectCase.items[itemChange] = null;
								casesCaseItem[itemChange].transform.Find("Icon").gameObject.GetComponent<Image>().sprite = null;
								casesCaseItem[itemChange].transform.Find("Icon").gameObject.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0f);
								flag = true;
							}
						}
					}
				}
			}
			else if (scrIntMain.itemsDataNow[itemChange].itemObject != null)
			{
				changeMyInventory.Find("Drop").GetComponent<Image>().fillAmount += Time.deltaTime;
				if (changeMyInventory.Find("Drop").GetComponent<Image>().fillAmount >= 1f)
				{
					changeMyInventory.Find("Drop").GetComponent<Image>().fillAmount = 0f;
					bool flag2 = false;
					for (int j = 0; j < 10; j++)
					{
						if (!flag2 && objectCase.items[j] == null)
						{
							objectCase.items[j] = scrIntMain.itemsDataNow[itemChange].itemObject;
							casesCaseItem[j].transform.Find("Icon").gameObject.GetComponent<Image>().sprite = objectCase.items[j].GetComponent<Item_ItemInventory>().icon;
							casesCaseItem[j].transform.Find("Icon").gameObject.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
							scrIntMain.ItemRemove(scrIntMain.itemsDataNow[itemChange].itemObject);
							casesMyItem[itemChange].transform.Find("Icon").gameObject.GetComponent<Image>().sprite = null;
							casesMyItem[itemChange].transform.Find("Icon").gameObject.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0f);
							flag2 = true;
						}
					}
				}
			}
		}
		if (Input.GetButtonUp("Redrop"))
		{
			changeMyInventory.Find("Drop").GetComponent<Image>().fillAmount = 0f;
			changeCaseInventory.Find("Drop").GetComponent<Image>().fillAmount = 0f;
		}
	}

	private void Right()
	{
		int num = itemChange;
		itemChange++;
		if (!inventoryUp)
		{
			if (itemChange > 9)
			{
				itemChange = 0;
			}
		}
		else
		{
			if (num == 9)
			{
				itemChange = 0;
			}
			if (num == 19)
			{
				itemChange = 10;
			}
			if (num == 29)
			{
				itemChange = 20;
			}
			if (num == 39)
			{
				itemChange = 30;
			}
		}
		ChangeItem();
	}

	private void Left()
	{
		int num = itemChange;
		itemChange--;
		if (!inventoryUp)
		{
			if (itemChange < 0)
			{
				itemChange = 9;
			}
		}
		else
		{
			if (num == 0)
			{
				itemChange = 9;
			}
			if (num == 10)
			{
				itemChange = 19;
			}
			if (num == 20)
			{
				itemChange = 29;
			}
			if (num == 30)
			{
				itemChange = 39;
			}
		}
		ChangeItem();
	}

	private void Up()
	{
		bool flag = false;
		if (inventoryUp && itemChange < 10)
		{
			inventoryUp = false;
			ChangeItem();
			flag = true;
		}
		if (inventoryUp && itemChange >= 10)
		{
			itemChange -= 10;
			ChangeItem();
		}
		if (!inventoryUp && !flag)
		{
			itemChange += 30;
			inventoryUp = true;
			ChangeItem();
		}
	}

	private void Down()
	{
		bool flag = false;
		if (inventoryUp && itemChange >= 30)
		{
			itemChange -= 30;
			inventoryUp = false;
			ChangeItem();
			flag = true;
		}
		if (inventoryUp && itemChange < 30)
		{
			itemChange += 10;
			ChangeItem();
		}
		if (!inventoryUp && !flag)
		{
			inventoryUp = true;
			ChangeItem();
		}
	}

	private void ChangeItem()
	{
		if (!inventoryUp)
		{
			changeCaseInventory.gameObject.SetActive(value: false);
			changeMyInventory.gameObject.SetActive(value: true);
			changeMyInventory.anchoredPosition = new Vector2(casesMyItem[itemChange].GetComponent<RectTransform>().anchoredPosition.x - 2.5f, casesMyItem[itemChange].GetComponent<RectTransform>().anchoredPosition.y + 2.5f);
			casesMyItem[itemChange].GetComponent<RectTransform>();
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
		}
		else
		{
			changeCaseInventory.gameObject.SetActive(value: true);
			changeMyInventory.gameObject.SetActive(value: false);
			changeCaseInventory.anchoredPosition = new Vector2(casesCaseItem[itemChange].GetComponent<RectTransform>().anchoredPosition.x - 2.5f, casesCaseItem[itemChange].GetComponent<RectTransform>().anchoredPosition.y + 2.5f);
			casesCaseItem[itemChange].GetComponent<RectTransform>();
			if (objectCase.items[itemChange] != null)
			{
				if (!objectCase.items[itemChange].GetComponent<Item_ItemInventory>().specialItem)
				{
					textUse.color = new Color(1f, 1f, 1f, 1f);
				}
				else
				{
					textUse.color = new Color(1f, 1f, 1f, 0.5f);
				}
				if (objectCase.items[itemChange].GetComponent<Item_ItemInventory>().specialItem)
				{
					textInspect.color = new Color(1f, 1f, 1f, 1f);
				}
				else
				{
					textInspect.color = new Color(1f, 1f, 1f, 0.5f);
				}
			}
		}
		changeMyInventory.Find("Drop").GetComponent<Image>().fillAmount = 0f;
		changeCaseInventory.Find("Drop").GetComponent<Image>().fillAmount = 0f;
	}

	private void ShowItem()
	{
		if (!inventoryUp)
		{
			GameObject obj = Object.Instantiate(objectShowItem);
			obj.GetComponent<Interface_ShowItem>().objectShow = scrIntMain.itemsDataNow[itemChange].dataShow;
			obj.GetComponent<Interface_ShowItem>().backMyInventory = true;
			obj.GetComponent<Interface_ShowItem>().textItem = File.ReadAllLines("Data/Languages/" + GlobalGame.Language + "/" + scrIntMain.itemsDataNow[itemChange].itemObject.GetComponent<Item_ItemInventory>().fileText + ".txt")[scrIntMain.itemsDataNow[itemChange].itemObject.GetComponent<Item_ItemInventory>().nameItem - 1].ToUpper();
			Object.Destroy(base.gameObject);
		}
	}
}
