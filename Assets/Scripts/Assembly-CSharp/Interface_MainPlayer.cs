using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Interface_MainPlayer : MonoBehaviour
{
	public GameObject myInventory;

	public GameObject caseInventory;

	public GameObject chestInventory;

	public GameObject interfaceDialogue;

	public GameObject thoughts;

	public GameObject task;

	[Header("Fast menu")]
	public GameObject fastMenu;

	public GameObject[] fastMenuCases;

	public BlackScreen bs;

	[HideInInspector]
	public ItemData[] itemsDataNow = new ItemData[100];

	[HideInInspector]
	public int itemCount;

	[HideInInspector]
	public bool dialogue;

	[HideInInspector]
	public bool inventory;

	[HideInInspector]
	public GameObject objCaseInventory;

	private GameObject objMyInventory;

	private GameObject objDialogue;

	private Player player;

	private AudioSource audioDialogue;

	private Image dialogueImg;

	private Text dialogueText;

	private float timeDialogue;

	private float timeTextPrint;

	private string textPrint;

	private bool playerDontMoveMemory;

	private bool fastMenuActive;

	private bool verticalKey;

	private bool horizontalKey;

	private int FMchangeCaseNow;

	private RectTransform FMchange;

	private void Start()
	{
		player = GameObject.FindWithTag("Player").GetComponent<Player>();
		dialogueImg = thoughts.GetComponent<Image>();
		dialogueText = thoughts.transform.Find("Text Dialogue").gameObject.GetComponent<Text>();
		audioDialogue = thoughts.GetComponent<AudioSource>();
		FMchange = fastMenu.transform.Find("Line Change").GetComponent<RectTransform>();
	}

	private void Update()
	{
		if (timeDialogue > 0f)
		{
			if (dialogueText.color.a < 1f)
			{
				dialogueText.color = Color.Lerp(dialogueText.color, new Color(1f, 1f, 1f, 1f), Time.deltaTime * 5f);
			}
			if (dialogueImg.color.a < 0.2f)
			{
				dialogueImg.color = Color.Lerp(dialogueImg.color, new Color(1f, 1f, 1f, 0.2f), Time.deltaTime * 5f);
			}
			if (dialogueText.text != textPrint)
			{
				timeTextPrint += Time.deltaTime * 20f;
				if (timeTextPrint >= 1f)
				{
					timeTextPrint = 0f;
					dialogueText.text += textPrint[dialogueText.text.Length];
					if (!GlobalGame.trailer)
					{
						audioDialogue.pitch = Random.Range(0.9f, 1.1f);
						audioDialogue.Play();
					}
				}
			}
			else
			{
				timeDialogue -= Time.deltaTime;
				if (timeDialogue < 0f)
				{
					timeDialogue = 0f;
				}
			}
		}
		else
		{
			if (dialogueText.color.a > 0f)
			{
				dialogueText.color = Color.Lerp(dialogueText.color, new Color(1f, 1f, 1f, 0f), Time.deltaTime * 10f);
			}
			if (dialogueImg.color.a > 0f)
			{
				dialogueImg.color = Color.Lerp(dialogueImg.color, new Color(1f, 1f, 1f, 0f), Time.deltaTime * 10f);
			}
		}
		if (Input.GetButtonDown("Cancel"))
		{
			FastMenu();
		}
		if (!fastMenuActive)
		{
			return;
		}
		if (Input.GetAxis("Vertical") < -0.2f && !verticalKey)
		{
			verticalKey = true;
			FMchangeCaseNow++;
			if (FMchangeCaseNow >= fastMenuCases.Length)
			{
				FMchangeCaseNow = 0;
			}
		}
		if (Input.GetAxis("Vertical") > 0.2f && !verticalKey)
		{
			verticalKey = true;
			FMchangeCaseNow--;
			if (FMchangeCaseNow < 0)
			{
				FMchangeCaseNow = fastMenuCases.Length - 1;
			}
		}
		if (Input.GetAxis("Vertical") == 0f)
		{
			verticalKey = false;
		}
		FMchange.anchoredPosition = new Vector2(0f, fastMenuCases[FMchangeCaseNow].GetComponent<RectTransform>().anchoredPosition.y);
		if (Input.GetAxis("Horizontal") < 0f && !horizontalKey)
		{
			horizontalKey = true;
			if (FMchangeCaseNow == 2 && PlayerPrefs.GetFloat("Volume", 1f) > 0f)
			{
				PlayerPrefs.SetFloat("Volume", PlayerPrefs.GetFloat("Volume", 1f) - 0.1f);
			}
			if (FMchangeCaseNow == 1 && PlayerPrefs.GetFloat("SensitivityMouse", 1f) > 0f)
			{
				PlayerPrefs.SetFloat("SensitivityMouse", PlayerPrefs.GetFloat("SensitivityMouse", 1f) - 0.1f);
			}
			UpdateFastMenu();
		}
		if (Input.GetAxis("Horizontal") > 0f && !horizontalKey)
		{
			horizontalKey = true;
			if (FMchangeCaseNow == 2 && PlayerPrefs.GetFloat("Volume", 1f) < 1f)
			{
				PlayerPrefs.SetFloat("Volume", PlayerPrefs.GetFloat("Volume", 1f) + 0.1f);
			}
			if (FMchangeCaseNow == 1 && PlayerPrefs.GetFloat("SensitivityMouse", 1f) < 1f)
			{
				PlayerPrefs.SetFloat("SensitivityMouse", PlayerPrefs.GetFloat("SensitivityMouse", 1f) + 0.1f);
			}
			UpdateFastMenu();
		}
		if (Input.GetAxis("Horizontal") == 0f)
		{
			horizontalKey = false;
		}
		if (Input.GetButtonDown("Space"))
		{
			if (FMchangeCaseNow == 0)
			{
				FastMenu();
			}
			if (FMchangeCaseNow == 3)
			{
				bs.nextLevel("SceneMenu");
				FastMenu();
			}
		}
	}

	private void FastMenu()
	{
		fastMenu.SetActive(!fastMenu.activeInHierarchy);
		if (fastMenu.activeInHierarchy)
		{
			playerDontMoveMemory = player.dontMove;
			player.dontMove = true;
			fastMenuActive = true;
			UpdateFastMenu();
		}
		if (!fastMenu.activeInHierarchy)
		{
			player.dontMove = playerDontMoveMemory;
			fastMenuActive = false;
		}
	}

	private void UpdateFastMenu()
	{
		fastMenuCases[2].transform.Find("Slider").GetComponent<Slider>().value = PlayerPrefs.GetFloat("Volume", 1f) * 10f;
		GlobalGame.VolumeGame = PlayerPrefs.GetFloat("Volume", 1f);
		fastMenuCases[1].transform.Find("Slider").GetComponent<Slider>().value = PlayerPrefs.GetFloat("SensitivityMouse", 0.5f) * 10f;
		player.UpdateSettings();
	}

	public void InventoryPlayer()
	{
		if (objMyInventory == null)
		{
			if (itemCount != 0)
			{
				player.dontMove = true;
			}
			inventory = true;
			objMyInventory = Object.Instantiate(myInventory, base.transform.Find("Interface/InventoryGeneral").transform);
		}
		else
		{
			Object.Destroy(objMyInventory);
			inventory = false;
			player.dontMove = false;
		}
	}

	public void InventoryCase(Interactive_Cases objCase, bool active)
	{
		if (active)
		{
			inventory = true;
			player.dontMove = true;
			objCaseInventory = Object.Instantiate(caseInventory, base.transform.Find("Interface/InventoryGeneral").transform);
			objCaseInventory.GetComponent<Interface_CaseInventory>().objectCase = objCase;
		}
		else if (objCaseInventory != null)
		{
			player.dontMove = false;
			inventory = false;
			Object.Destroy(objCaseInventory);
		}
	}

	public void InventoryChest(Interactive_Cases objCase, bool active)
	{
		if (active)
		{
			inventory = true;
			player.dontMove = true;
			objCaseInventory = Object.Instantiate(chestInventory, base.transform.Find("Interface/InventoryGeneral").transform);
			objCaseInventory.GetComponent<Interface_MainChestInventory>().objectCase = objCase;
		}
		else if (objCaseInventory != null)
		{
			player.dontMove = false;
			inventory = false;
			Object.Destroy(objCaseInventory);
		}
	}

	public void ItemAdd(GameObject _itemobj)
	{
		itemCount++;
		bool flag = false;
		for (int i = 0; i < 10; i++)
		{
			if (!flag && itemsDataNow[i].itemObject == null)
			{
				flag = true;
				itemsDataNow[i].itemObject = _itemobj;
			}
		}
	}

	public void ItemRemove(GameObject _itemobj)
	{
		itemCount--;
		bool flag = false;
		for (int i = 0; i < 10; i++)
		{
			if (!flag && itemsDataNow[i].itemObject == _itemobj)
			{
				flag = true;
				if (itemsDataNow[i].itemCount == 0)
				{
					itemsDataNow[i].itemCount = 0;
					itemsDataNow[i].itemObject = null;
				}
				if (itemsDataNow[i].itemCount > 1)
				{
					itemsDataNow[i].itemCount--;
				}
			}
		}
	}

	public void Dialogue(string _file, int _string)
	{
		textPrint = File.ReadAllLines("Data/Languages/" + GlobalGame.Language + "/" + _file + ".txt")[_string - 1];
		dialogueText.text = "";
		timeDialogue = 5f;
	}

	public void Task(string _file, int _string)
	{
		if (!GlobalGame.trailer)
		{
			task.GetComponent<AudioSource>().Play();
		}
		task.GetComponent<Animator>().SetTrigger("Show");
		task.transform.Find("Text Tasks").gameObject.GetComponent<Text>().text = File.ReadAllLines("Data/Languages/" + GlobalGame.Language + "/" + _file + ".txt")[_string - 1];
	}

	public void TaskEnd()
	{
		task.GetComponent<Animator>().SetTrigger("Hide");
	}
}
