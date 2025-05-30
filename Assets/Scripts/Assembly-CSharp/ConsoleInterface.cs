using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ConsoleInterface : MonoBehaviour
{
	public GameObject inputTextObject;

	public RectTransform textContent;

	public Text textInfo;

	public Text textString;

	private GameObject settingsObject;

	private int iLastCodeNow;

	private InputField inputField;

	private bool settings;

	private void Start()
	{
		settingsObject = base.transform.Find("MainPanel/Settings").gameObject;
		inputField = inputTextObject.GetComponent<InputField>();
		StartCoroutine(ChangeInput());
	}

	private IEnumerator ChangeInput()
	{
		yield return new WaitForSeconds(0.1f);
		EventSystem.current.SetSelectedGameObject(null);
		EventSystem.current.SetSelectedGameObject(inputTextObject);
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.BackQuote))
		{
			Close();
		}
		if (Input.GetKeyDown(KeyCode.Return))
		{
			EnterConsole();
		}
		if (Input.GetKeyDown(KeyCode.UpArrow))
		{
			iLastCodeNow--;
			if (iLastCodeNow < 0)
			{
				iLastCodeNow = ConsoleMain.console_iLastCode;
			}
			inputField.text = ConsoleMain.console_lastCodes[iLastCodeNow];
		}
		if (Input.GetKeyDown(KeyCode.DownArrow))
		{
			iLastCodeNow++;
			if (iLastCodeNow > ConsoleMain.console_iLastCode)
			{
				iLastCodeNow = 0;
			}
			inputField.text = ConsoleMain.console_lastCodes[iLastCodeNow];
		}
		textInfo.text = ConsoleMain.consoleText;
		textContent.anchoredPosition -= new Vector2(0f, Input.GetAxis("Mouse ScrollWheel") * 200f);
		if (textContent.anchoredPosition.y < 0f)
		{
			textContent.anchoredPosition = new Vector2(0f, 0f);
		}
		if (textContent.anchoredPosition.y > 5000f)
		{
			textContent.anchoredPosition = new Vector2(0f, 5000f);
		}
		textString.text = "String: " + iLastCodeNow + "/" + ConsoleMain.console_iLastCode;
	}

	public void EnterConsole()
	{
		EventSystem.current.SetSelectedGameObject(null);
		EventSystem.current.SetSelectedGameObject(inputTextObject);
		if (inputField.text != "")
		{
			string text = inputField.text.ToLower();
			string text2 = inputField.text;
			ConsoleMain.console_lastCodes[ConsoleMain.console_iLastCode] = text;
			ConsoleMain.console_iLastCode++;
			if (ConsoleMain.console_iLastCode > 99)
			{
				ConsoleMain.console_iLastCode = 0;
			}
			iLastCodeNow = ConsoleMain.console_iLastCode;
			ConsoleMain.ConsolePrint("<color=#FFBB00>" + inputField.text + "</color>");
			inputField.text = "";
			if (text == "qiut")
			{
				Qiut();
			}
			if (text == "showhierarchy")
			{
				ShowHierarchy();
			}
			if (text == "mousehide")
			{
				MouseHide();
			}
			if (text == "help")
			{
				Help();
			}
			if (text == "clear")
			{
				Clear();
			}
			if (text == "trailer")
			{
				GlobalGame.trailer = !GlobalGame.trailer;
			}
			if (text == "playerprefsclear")
			{
				PlayerPrefs.DeleteAll();
			}
			if (text.Length > 5 && text.Substring(0, 5) == "time ")
			{
				Time.timeScale = float.Parse(text.Substring(5, text.Length - 5));
			}
			if (text.Length > 10 && text.Substring(0, 10) == "levelload ")
			{
				SceneManager.LoadScene(text.Substring(10, text.Length - 10) ?? "", LoadSceneMode.Single);
			}
			if (text.Length > 5 && text.Substring(0, 5) == "find ")
			{
				string @string = text.Substring(5, text.Length - 5);
				FindObject(@string);
			}
			if (text.Length > 8 && text.Substring(0, 8) == "findtag ")
			{
				string string2 = text2.Substring(8, text2.Length - 8);
				FindObjectTag(string2);
			}
			ConsoleCommandsA.CheckConsoleCommandA(text);
		}
	}

	public void Clear()
	{
		ConsoleMain.consoleText = "";
		inputField.text = "";
		base.transform.Find("MainPanel/Scroll View Information/Scrollbar Vertical").gameObject.GetComponent<Scrollbar>().value = 0f;
	}

	public void Close()
	{
		Object.Destroy(base.gameObject);
	}

	public void MouseHide()
	{
		CursorLockMode lockState;
		if (Cursor.visible)
		{
			lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		}
		else
		{
			lockState = CursorLockMode.None;
			Cursor.visible = true;
		}
		Cursor.lockState = lockState;
	}

	private void Qiut()
	{
		Application.Quit();
		Debug.Log("Quit");
	}

	private void ShowHierarchy()
	{
		Component[] array = Object.FindObjectsOfType<Transform>();
		Component[] array2 = array;
		for (int i = 0; i < array2.Length; i++)
		{
			if (array2[i].transform.parent == null)
			{
				ConsoleMain.ConsolePrint("<color=silver>" + array2[i].gameObject.name + "</color>");
			}
			else
			{
				ConsoleMain.ConsolePrint(array2[i].gameObject.name ?? "");
			}
		}
	}

	private void FindObject(string _string)
	{
		Component[] array = Object.FindObjectsOfType<Transform>();
		Component[] array2 = array;
		bool flag = false;
		for (int i = 0; i < array2.Length; i++)
		{
			if (array2[i].gameObject.name.ToLower() == _string)
			{
				ConsoleMain.ConsolePrint("<color=green>Object found.</color>");
				flag = true;
			}
		}
		if (!flag)
		{
			ConsoleMain.ConsolePrint("<color=red>Object not found.</color>");
		}
	}

	private void FindObjectTag(string _string)
	{
		if (GameObject.FindWithTag(_string) != null)
		{
			ConsoleMain.ConsolePrint("<color=green>Object found.</color>");
		}
		else
		{
			ConsoleMain.ConsolePrint("<color=red>Object not found.</color>");
		}
	}

	public void Settings()
	{
		settings = !settings;
		settingsObject.SetActive(settings);
	}

	public void SettingsClose()
	{
		settings = false;
		settingsObject.SetActive(value: false);
	}

	public void DropDownFunctions()
	{
		base.transform.Find("MainPanel/UpPanel/Drop Functions/Template").gameObject.SetActive(!base.transform.Find("MainPanel/UpPanel/Drop Functions/Template").gameObject.activeInHierarchy);
	}

	public void Help()
	{
		ConsoleMain.ConsolePrint("<color=#02E8E0>System-------------------------------------------</color>\nhelp = информация всех команд\nmousehide = включение и выключение курсора\nqiut = выключить игру\nplayerprefsclear = очистить реестр\nlevelload * = перейти на уровень *\ntime * = скорость *\nshowhierarchy = показать иерархию\nfind *= существует ли объект *\nfindtag *= существует ли объект по тегу *\ntrailer = режим трейлера\n(отключено) sv *= сохранить записи консоли в файл *\n(отключено) playerprefssave * * = сохранить реестр * со значением *\n(отключено) hideui = скрыть UI\n(отключено) listenervolume * = громкость *\n(отключено) destroyallscene = очистить всю сцену\n(отключено) createresource * = создать объект из ресурсов по пути *\n<color=#02E8E0>Game [WIRE LIPS] ----------------------------------------</color>\n");
		base.transform.Find("MainPanel/Scroll View Information/Scrollbar Vertical").gameObject.GetComponent<Scrollbar>().value = 0f;
	}
}
