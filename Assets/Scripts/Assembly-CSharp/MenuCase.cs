using System.IO;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MenuCase : MonoBehaviour
{
	public enum MenuCases
	{
		NewGame = 0,
		Volume = 1,
		Language = 2,
		AO = 3,
		FullScreen = 4,
		MotionBlur = 5,
		Mirror = 6,
		Shadows = 7,
		Textures = 8,
		Resolution = 9,
		Next = 10,
		Exit = 11,
		SensitivityMouse = 12,
		Antialiasing = 13,
		Event = 14,
		LoadLevel = 15,
		None2 = 16,
		PixelGraphic = 17,
		Continue = 18
	}

	public MenuCases function;

	public bool active = true;

	[Header("Load level")]
	public string levelname;

	public int[] caseSave = new int[3];

	public string saveActiveLevel;

	[Header("Options")]
	public GameObject optionColor;

	public Color colorOn;

	public Color colorOff;

	[Header("Slider")]
	public Slider slider;

	[Header("Toggle")]
	public RectTransform toggle;

	[Header("Event")]
	public UnityEvent _eventEnter;

	public UnityEvent _eventLeft;

	public UnityEvent _eventRight;

	[Header("Text")]
	public Text text;

	[Space(10f)]
	[Header("Next Location")]
	public GameObject nextCase;

	public GameObject closeLocation;

	public int selectChange = -1;

	private MenuMain menuScr;

	private bool toggleBool;

	private bool rightEvent;

	private bool leftEvent;

	private bool canClick;

	private bool silverText;

	private string[] languages;

	private int intiA;

	private int intiResolution;

	private int numbersResolution;

	private Resolution[] resolutions;

	private BlackScreen bs;

	private float timeSilverText;

	private void Start()
	{
		menuScr = GameObject.FindWithTag("Interface").GetComponent<MenuMain>();
		bs = GameObject.FindWithTag("GameController").transform.Find("Interface/Interface Black/BlackScreen").gameObject.GetComponent<BlackScreen>();
		if (function == MenuCases.FullScreen && PlayerPrefs.GetString("FullScreen", "Yes") == "Yes")
		{
			toggleBool = true;
		}
		if (function == MenuCases.MotionBlur && PlayerPrefs.GetInt("MotionBlur", 1) == 1)
		{
			toggleBool = true;
		}
		if (function == MenuCases.AO && PlayerPrefs.GetInt("AO", 1) == 1)
		{
			toggleBool = true;
		}
		if (function == MenuCases.PixelGraphic && PlayerPrefs.GetString("PixelGraphic", "None") == "Active")
		{
			toggleBool = true;
		}
		if (function == MenuCases.Shadows && PlayerPrefs.GetString("Mirror", "Hight") == "Hight")
		{
			slider.value = 3f;
		}
		if (function == MenuCases.Shadows && PlayerPrefs.GetString("Mirror", "Hight") == "Medium")
		{
			slider.value = 2f;
		}
		if (function == MenuCases.Shadows && PlayerPrefs.GetString("Mirror", "Hight") == "Low")
		{
			slider.value = 1f;
		}
		if (function == MenuCases.Shadows && PlayerPrefs.GetString("Shadow", "Hight") == "Ultra")
		{
			slider.value = 4f;
		}
		if (function == MenuCases.Shadows && PlayerPrefs.GetString("Shadow", "Hight") == "Hight")
		{
			slider.value = 3f;
		}
		if (function == MenuCases.Shadows && PlayerPrefs.GetString("Shadow", "Hight") == "Medium")
		{
			slider.value = 2f;
		}
		if (function == MenuCases.Shadows && PlayerPrefs.GetString("Shadow", "Hight") == "Low")
		{
			slider.value = 1f;
		}
		if (function == MenuCases.Shadows && PlayerPrefs.GetString("Shadow", "Hight") == "Terrible")
		{
			slider.value = 0f;
		}
		if (function == MenuCases.Textures && PlayerPrefs.GetString("Texture", "Hight") == "Hight")
		{
			slider.value = 3f;
		}
		if (function == MenuCases.Textures && PlayerPrefs.GetString("Texture", "Hight") == "Medium")
		{
			slider.value = 2f;
		}
		if (function == MenuCases.Textures && PlayerPrefs.GetString("Texture", "Hight") == "Low")
		{
			slider.value = 1f;
		}
		if (function == MenuCases.Textures && PlayerPrefs.GetString("Texture", "Hight") == "Terrible")
		{
			slider.value = 0f;
		}
		if (function == MenuCases.Volume)
		{
			slider.value = PlayerPrefs.GetFloat("Volume", 1f) * 10f;
		}
		if (function == MenuCases.SensitivityMouse)
		{
			slider.value = PlayerPrefs.GetFloat("SensitivityMouse", 0.5f) * 10f;
		}
		if (function == MenuCases.Antialiasing)
		{
			if (PlayerPrefs.GetString("Antialiasing", "Normal") == "None")
			{
				slider.value = 0f;
			}
			if (PlayerPrefs.GetString("Antialiasing", "Normal") == "Normal")
			{
				slider.value = 1f;
			}
			if (PlayerPrefs.GetString("Antialiasing", "Normal") == "Hight")
			{
				slider.value = 2f;
			}
		}
		if (function == MenuCases.Language)
		{
			languages = File.ReadAllLines("Data/Languages/Languages.txt");
			text.text = PlayerPrefs.GetString("Language", "English");
		}
		if (function == MenuCases.Resolution)
		{
			resolutions = Screen.resolutions;
			numbersResolution = resolutions.Length;
			for (int i = 0; i < numbersResolution; i++)
			{
				if (resolutions[i].width == PlayerPrefs.GetInt("XScreen", resolutions[numbersResolution - 1].width) && resolutions[i].height == PlayerPrefs.GetInt("XScreen", resolutions[numbersResolution - 1].height))
				{
					intiResolution = i;
				}
			}
			text.text = PlayerPrefs.GetInt("XScreen", resolutions[intiResolution].width) + "x" + PlayerPrefs.GetInt("YScreen", resolutions[intiResolution].height);
		}
		if (function == MenuCases.Continue)
		{
			if (File.Exists("Data/Save/Continue"))
			{
				canClick = true;
			}
			else
			{
				silverText = true;
			}
		}
		if (function == MenuCases.LoadLevel)
		{
			if (File.Exists("Data/Save/" + saveActiveLevel))
			{
				canClick = true;
			}
			else
			{
				silverText = true;
			}
		}
		timeSilverText = 1f;
	}

	private void OnEnable()
	{
		if (!active)
		{
			timeSilverText = 1f;
			silverText = true;
		}
		else
		{
			silverText = false;
		}
		if (function == MenuCases.Language)
		{
			text.text = PlayerPrefs.GetString("Language", "English");
		}
		if (silverText)
		{
			timeSilverText = 1f;
		}
	}

	private void Update()
	{
		if (silverText)
		{
			timeSilverText -= Time.deltaTime;
			if (timeSilverText < 0f)
			{
				GetComponent<Text>().color = Color.Lerp(GetComponent<Text>().color, new Color(1f, 1f, 1f, 0.3f), Time.deltaTime * 10f);
			}
		}
		if (optionColor != null && !menuScr.active)
		{
			if (base.gameObject == menuScr.ChangeSelect[menuScr.caseSelected])
			{
				if (optionColor.GetComponent<Image>() != null)
				{
					optionColor.GetComponent<Image>().color = colorOn;
				}
				if (optionColor.GetComponent<Text>() != null)
				{
					optionColor.GetComponent<Text>().color = colorOn;
				}
			}
			if (base.gameObject != menuScr.ChangeSelect[menuScr.caseSelected])
			{
				if (optionColor.GetComponent<Image>() != null)
				{
					optionColor.GetComponent<Image>().color = colorOff;
				}
				if (optionColor.GetComponent<Text>() != null)
				{
					optionColor.GetComponent<Text>().color = colorOff;
				}
			}
		}
		if (toggle != null)
		{
			if (toggleBool)
			{
				toggle.anchoredPosition = Vector2.Lerp(toggle.anchoredPosition, new Vector2(5f, 0f), 0.2f);
				toggle.sizeDelta = Vector2.Lerp(toggle.sizeDelta, new Vector2(15f, 15f), 0.2f);
			}
			else
			{
				toggle.anchoredPosition = Vector2.Lerp(toggle.anchoredPosition, new Vector2(-5f, 0f), 0.2f);
				toggle.sizeDelta = Vector2.Lerp(toggle.sizeDelta, new Vector2(10f, 10f), 0.2f);
			}
		}
		if (base.gameObject == menuScr.ChangeSelect[menuScr.caseSelected] && function == MenuCases.Event)
		{
			if (Input.GetAxis("Horizontal") > 0f && !rightEvent)
			{
				_eventRight.Invoke();
				rightEvent = true;
				leftEvent = false;
			}
			if (Input.GetAxis("Horizontal") < 0f && !leftEvent)
			{
				_eventLeft.Invoke();
				leftEvent = true;
				rightEvent = false;
			}
			if (Input.GetAxis("Horizontal") == 0f)
			{
				leftEvent = false;
				rightEvent = false;
			}
			if ((Input.GetButtonDown("Space") || Input.GetButtonDown("Action")) && active)
			{
				_eventEnter.Invoke();
			}
		}
	}

	public void Click()
	{
		if (function == MenuCases.NewGame)
		{
			GlobalGame.saveCase1 = 0;
			GlobalGame.saveCase2 = 0;
			GlobalGame.saveCase3 = 0;
			NewGame();
		}
		if (function == MenuCases.Next)
		{
			_eventEnter.Invoke();
			nextCase.SetActive(value: true);
			nextCase.GetComponent<MenuCaseNextLocation>().NextLocation(selectChange);
			closeLocation.SetActive(value: false);
		}
		if (function == MenuCases.Exit)
		{
			Exit();
		}
		if (function == MenuCases.Continue && canClick)
		{
			Continue();
		}
		if (function == MenuCases.LoadLevel && canClick)
		{
			LoadLevelGame();
		}
	}

	public void Right()
	{
		if (toggle != null)
		{
			if (!toggleBool)
			{
				menuScr.PlaySidesChange();
			}
			toggleBool = true;
			if (function == MenuCases.AO)
			{
				PlayerPrefs.SetInt("AO", 1);
			}
			if (function == MenuCases.MotionBlur)
			{
				PlayerPrefs.SetInt("MotionBlur", 1);
			}
			if (function == MenuCases.FullScreen)
			{
				PlayerPrefs.SetString("FullScreen", "Yes");
				Screen.fullScreen = true;
			}
			if (function == MenuCases.PixelGraphic)
			{
				PixelGraphicActive();
			}
		}
		if (slider != null)
		{
			float value = slider.value;
			slider.value++;
			if (value != slider.value)
			{
				menuScr.PlaySidesChange();
			}
			if (function == MenuCases.Volume)
			{
				GlobalGame.VolumeGame = slider.value / 10f;
				AudioListener.volume = slider.value / 10f;
				PlayerPrefs.SetFloat("Volume", slider.value / 10f);
			}
			if (function == MenuCases.SensitivityMouse)
			{
				GlobalGame.SensitivityGame = slider.value / 10f;
				PlayerPrefs.SetFloat("SensitivityMouse", slider.value / 10f);
			}
			if (function == MenuCases.Textures)
			{
				if (slider.value == 1f)
				{
					QualitySettings.masterTextureLimit = 2;
					PlayerPrefs.SetString("Texture", "Low");
				}
				if (slider.value == 2f)
				{
					QualitySettings.masterTextureLimit = 1;
					PlayerPrefs.SetString("Texture", "Medium");
				}
				if (slider.value == 3f)
				{
					QualitySettings.masterTextureLimit = 0;
					PlayerPrefs.SetString("Texture", "Hight");
				}
			}
			if (function == MenuCases.Shadows)
			{
				if (slider.value == 4f)
				{
					GlobalGame.Shadow = 1;
					PlayerPrefs.SetString("Shadow", "Ultra");
					QualitySettings.shadowResolution = ShadowResolution.VeryHigh;
				}
				if (slider.value == 3f)
				{
					GlobalGame.Shadow = 1;
					PlayerPrefs.SetString("Shadow", "Hight");
					QualitySettings.shadowResolution = ShadowResolution.High;
				}
				if (slider.value == 2f)
				{
					GlobalGame.Shadow = 1;
					PlayerPrefs.SetString("Shadow", "Medium");
					QualitySettings.shadowResolution = ShadowResolution.Medium;
				}
				if (slider.value == 1f)
				{
					GlobalGame.Shadow = 1;
					PlayerPrefs.SetString("Shadow", "Low");
					QualitySettings.shadowResolution = ShadowResolution.Low;
				}
			}
			if (function == MenuCases.Mirror)
			{
				if (slider.value == 3f)
				{
					PlayerPrefs.SetString("Mirror", "Hight");
				}
				if (slider.value == 2f)
				{
					PlayerPrefs.SetString("Mirror", "Medium");
				}
				if (slider.value == 1f)
				{
					PlayerPrefs.SetString("Mirror", "Low");
				}
			}
			if (function == MenuCases.Antialiasing)
			{
				if (slider.value == 0f)
				{
					PlayerPrefs.SetString("Antialiasing", "None");
					QualitySettings.antiAliasing = 0;
				}
				if (slider.value == 1f)
				{
					PlayerPrefs.SetString("Antialiasing", "Normal");
					QualitySettings.antiAliasing = 2;
				}
				if (slider.value == 2f)
				{
					PlayerPrefs.SetString("Antialiasing", "Hight");
					QualitySettings.antiAliasing = 4;
				}
			}
		}
		if (function == MenuCases.Language)
		{
			intiA++;
			if (intiA >= languages.Length)
			{
				intiA = 0;
			}
			text.text = languages[intiA];
			GlobalGame.Language = text.text;
			PlayerPrefs.SetString("Language", languages[intiA]);
			LanguageUpdate();
			menuScr.PlaySidesChange();
		}
		if (function == MenuCases.Resolution)
		{
			intiResolution++;
			if (intiResolution >= numbersResolution)
			{
				intiResolution = 0;
			}
			text.text = resolutions[intiResolution].width + "x" + resolutions[intiResolution].height;
			PlayerPrefs.SetInt("XScreen", resolutions[intiResolution].width);
			PlayerPrefs.SetInt("YScreen", resolutions[intiResolution].height);
			Screen.SetResolution(resolutions[intiResolution].width, resolutions[intiResolution].height, Screen.fullScreen, 60);
			menuScr.PlaySidesChange();
		}
	}

	public void Left()
	{
		if (toggle != null)
		{
			if (toggleBool)
			{
				menuScr.PlaySidesChange();
			}
			toggleBool = false;
			if (function == MenuCases.AO)
			{
				PlayerPrefs.SetInt("AO", 0);
			}
			if (function == MenuCases.MotionBlur)
			{
				PlayerPrefs.SetInt("MotionBlur", 0);
			}
			if (function == MenuCases.FullScreen)
			{
				WindowedChange();
			}
			if (function == MenuCases.PixelGraphic)
			{
				PlayerPrefs.SetString("PixelGraphic", "None");
			}
		}
		if (slider != null)
		{
			float value = slider.value;
			slider.value--;
			if (value != slider.value)
			{
				menuScr.PlaySidesChange();
			}
			if (function == MenuCases.Volume)
			{
				GlobalGame.VolumeGame = slider.value / 10f;
				AudioListener.volume = slider.value / 10f;
				PlayerPrefs.SetFloat("Volume", slider.value / 10f);
			}
			if (function == MenuCases.SensitivityMouse)
			{
				GlobalGame.SensitivityGame = slider.value / 10f;
				PlayerPrefs.SetFloat("SensitivityMouse", slider.value / 10f);
			}
			if (function == MenuCases.Textures)
			{
				if (slider.value == 2f)
				{
					QualitySettings.masterTextureLimit = 1;
					PlayerPrefs.SetString("Texture", "Medium");
				}
				if (slider.value == 1f)
				{
					QualitySettings.masterTextureLimit = 2;
					PlayerPrefs.SetString("Texture", "Low");
				}
				if (slider.value == 0f)
				{
					QualitySettings.masterTextureLimit = 3;
					PlayerPrefs.SetString("Texture", "Terrible");
				}
			}
			if (function == MenuCases.Shadows)
			{
				if (slider.value == 3f)
				{
					GlobalGame.Shadow = 1;
					PlayerPrefs.SetString("Shadow", "Hight");
					QualitySettings.shadowResolution = ShadowResolution.High;
				}
				if (slider.value == 2f)
				{
					GlobalGame.Shadow = 1;
					PlayerPrefs.SetString("Shadow", "Medium");
					QualitySettings.shadowResolution = ShadowResolution.Medium;
				}
				if (slider.value == 1f)
				{
					GlobalGame.Shadow = 1;
					PlayerPrefs.SetString("Shadow", "Low");
					QualitySettings.shadowResolution = ShadowResolution.Low;
				}
				if (slider.value == 0f)
				{
					GlobalGame.Shadow = 0;
					PlayerPrefs.SetString("Shadow", "Terrible");
					QualitySettings.shadowResolution = ShadowResolution.Low;
				}
			}
			if (function == MenuCases.Antialiasing)
			{
				if (slider.value == 0f)
				{
					PlayerPrefs.SetString("Antialiasing", "None");
					QualitySettings.antiAliasing = 0;
				}
				if (slider.value == 1f)
				{
					PlayerPrefs.SetString("Antialiasing", "Normal");
					QualitySettings.antiAliasing = 2;
				}
				if (slider.value == 2f)
				{
					PlayerPrefs.SetString("Antialiasing", "Hight");
					QualitySettings.antiAliasing = 4;
				}
			}
		}
		if (function == MenuCases.Language)
		{
			intiA--;
			if (intiA < 0)
			{
				intiA = languages.Length - 1;
			}
			text.text = languages[intiA];
			GlobalGame.Language = text.text;
			PlayerPrefs.SetString("Language", languages[intiA]);
			LanguageUpdate();
			menuScr.PlaySidesChange();
		}
		if (function == MenuCases.Resolution)
		{
			intiResolution--;
			if (intiResolution < 0)
			{
				intiResolution = numbersResolution - 1;
			}
			text.text = resolutions[intiResolution].width + "x" + resolutions[intiResolution].height;
			PlayerPrefs.SetInt("XScreen", resolutions[intiResolution].width);
			PlayerPrefs.SetInt("YScreen", resolutions[intiResolution].height);
			Screen.SetResolution(resolutions[intiResolution].width, resolutions[intiResolution].height, Screen.fullScreen, 60);
			menuScr.PlaySidesChange();
		}
	}

	private void LanguageUpdate()
	{
		Localization_UIText[] array = new Localization_UIText[Object.FindObjectsOfType<Localization_UIText>().Length];
		array = Object.FindObjectsOfType<Localization_UIText>();
		for (int i = 0; i < array.Length; i++)
		{
			array[i].ResetLanguage();
		}
	}

	private void NewGame()
	{
		bs.nextLevel(levelname);
	}

	private void PixelGraphicActive()
	{
		PlayerPrefs.SetString("PixelGraphic", "Active");
		PlayerPrefs.SetString("FullScreen", "Yes");
		Screen.fullScreen = true;
		MenuCase[] array = new MenuCase[Object.FindObjectsOfType<MenuCase>().Length];
		array = Object.FindObjectsOfType<MenuCase>();
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i].function == MenuCases.FullScreen)
			{
				array[i].toggleBool = true;
			}
		}
	}

	private void WindowedChange()
	{
		PlayerPrefs.SetString("FullScreen", "No");
		Screen.fullScreen = false;
		MenuCase[] array = new MenuCase[Object.FindObjectsOfType<MenuCase>().Length];
		array = Object.FindObjectsOfType<MenuCase>();
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i].function == MenuCases.PixelGraphic)
			{
				array[i].toggleBool = false;
				PlayerPrefs.SetString("PixelGraphic", "None");
			}
		}
	}

	private void LoadLevelGame()
	{
		bs.nextLevel(levelname ?? "");
		StreamWriter streamWriter = File.CreateText("Data/Save/Continue");
		streamWriter.WriteLine(levelname ?? "");
		streamWriter.Close();
	}

	private void Continue()
	{
		bs.nextLevel(File.ReadAllLines("Data/Save/Continue")[0] ?? "");
	}

	private void Exit()
	{
		Application.Quit();
		Debug.Log("Quit");
	}

	public void ActivationCase(bool x)
	{
		active = x;
		if (!active)
		{
			timeSilverText = 1f;
			silverText = true;
		}
		else
		{
			silverText = false;
		}
	}
}
