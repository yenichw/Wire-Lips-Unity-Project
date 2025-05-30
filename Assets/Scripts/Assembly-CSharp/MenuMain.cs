using System;
using UnityEngine;
using UnityEngine.UI;

public class MenuMain : MonoBehaviour
{
	[Serializable]
	public class UIAll
	{
		public GameObject[] UiObjects;

		[HideInInspector]
		public Color[] UIColorObjects;
	}

	public GameObject firstStartGameLocationOption;

	public GameObject firstLocationMenu;

	public GameObject Change;

	public AudioClip soundCase;

	public AudioClip soundSelect;

	public AudioClip soundSides;

	private AudioSource au;

	private int tmTables;

	private int iObjectUI;

	private Image imgChange;

	private Color changeColor;

	private float posHChange;

	private float stopAnimation;

	private bool buttonMoveMenuDown;

	private bool buttonMoveMenuUp;

	private bool buttonMoveMenuLeft;

	private bool buttonMoveMenuRight;

	[Header("info")]
	public int caseSelected;

	public GameObject[] ChangeSelect;

	public UIAll[] ObjectUI;

	public int firstSelect;

	public bool active;

	private void Start()
	{
		posHChange = Change.GetComponent<RectTransform>().anchoredPosition.x;
		au = GetComponent<AudioSource>();
		imgChange = Change.GetComponent<Image>();
		changeColor = imgChange.color;
		stopAnimation = 1f;
		for (int i = 0; i < ObjectUI.Length; i++)
		{
			ObjectUI[i].UIColorObjects = new Color[ObjectUI[i].UiObjects.Length];
		}
		for (int j = 0; j < ObjectUI.Length; j++)
		{
			for (int k = 0; k < ObjectUI[j].UiObjects.Length; k++)
			{
				if (ObjectUI[j].UiObjects[k].GetComponent<Text>() != null)
				{
					ObjectUI[j].UIColorObjects[k] = ObjectUI[j].UiObjects[k].GetComponent<Text>().color;
				}
				if (ObjectUI[j].UiObjects[k].GetComponent<Image>() != null)
				{
					ObjectUI[j].UIColorObjects[k] = ObjectUI[j].UiObjects[k].GetComponent<Image>().color;
				}
			}
		}
		if (PlayerPrefs.GetInt("FirstStartGame", 0) != 100)
		{
			firstLocationMenu.SetActive(value: false);
			PlayerPrefs.SetInt("FirstStartGame", 100);
			firstStartGameLocationOption.SetActive(value: true);
			firstStartGameLocationOption.GetComponent<MenuCaseNextLocation>().NextLocation(0);
		}
		else
		{
			firstLocationMenu.SetActive(value: true);
			firstLocationMenu.GetComponent<MenuCaseNextLocation>().NextLocation(-1);
		}
	}

	private void FixedUpdate()
	{
		if (active)
		{
			if (iObjectUI != ObjectUI.Length)
			{
				if (tmTables > 2)
				{
					iObjectUI++;
					au.PlayOneShot(soundCase, 0.4f);
					tmTables = 0;
				}
				tmTables++;
			}
			else
			{
				active = false;
			}
		}
		else
		{
			imgChange.color = Vector4.Lerp(imgChange.color, changeColor, Time.deltaTime * 5f);
		}
		if (stopAnimation > 0f)
		{
			for (int i = 0; i < iObjectUI; i++)
			{
				for (int j = 0; j < ObjectUI[i].UiObjects.Length; j++)
				{
					if (ObjectUI[i].UiObjects[j].GetComponent<Text>() != null)
					{
						ObjectUI[i].UiObjects[j].GetComponent<Text>().color = Color.Lerp(ObjectUI[i].UiObjects[j].GetComponent<Text>().color, ObjectUI[i].UIColorObjects[j], Time.deltaTime * 8f);
					}
					if (ObjectUI[i].UiObjects[j].GetComponent<Image>() != null)
					{
						ObjectUI[i].UiObjects[j].GetComponent<Image>().color = Color.Lerp(ObjectUI[i].UiObjects[j].GetComponent<Image>().color, ObjectUI[i].UIColorObjects[j], Time.deltaTime * 8f);
					}
				}
			}
		}
		if (iObjectUI == ObjectUI.Length)
		{
			stopAnimation -= Time.deltaTime;
		}
	}

	private void Update()
	{
		if (Input.GetButtonDown("Space") && ChangeSelect[caseSelected].GetComponent<MenuCase>() != null)
		{
			ChangeSelect[caseSelected].GetComponent<MenuCase>().Click();
		}
		if ((double)Input.GetAxis("Vertical") < -0.2)
		{
			if (!buttonMoveMenuDown)
			{
				au.PlayOneShot(soundSelect, 0.5f);
				caseSelected++;
				if (caseSelected > ChangeSelect.Length - 1)
				{
					caseSelected = 0;
				}
				Change.GetComponent<RectTransform>().anchoredPosition = new Vector4(posHChange, ChangeSelect[caseSelected].GetComponent<RectTransform>().anchoredPosition.y);
				buttonMoveMenuDown = true;
			}
		}
		else
		{
			buttonMoveMenuDown = false;
		}
		if ((double)Input.GetAxis("Vertical") > 0.2)
		{
			if (!buttonMoveMenuUp)
			{
				au.PlayOneShot(soundSelect, 0.5f);
				caseSelected--;
				if (caseSelected < 0)
				{
					caseSelected = ChangeSelect.Length - 1;
				}
				Change.GetComponent<RectTransform>().anchoredPosition = new Vector4(posHChange, ChangeSelect[caseSelected].GetComponent<RectTransform>().anchoredPosition.y);
				buttonMoveMenuUp = true;
			}
		}
		else
		{
			buttonMoveMenuUp = false;
		}
		if ((double)Input.GetAxis("Vertical") > -0.2 && (double)Input.GetAxis("Vertical") < 0.2)
		{
			buttonMoveMenuUp = false;
			buttonMoveMenuDown = false;
		}
		if ((double)Input.GetAxis("Horizontal") < -0.2)
		{
			if (!buttonMoveMenuLeft)
			{
				if (ChangeSelect[caseSelected].GetComponent<MenuCase>() != null)
				{
					ChangeSelect[caseSelected].GetComponent<MenuCase>().Left();
				}
				buttonMoveMenuLeft = true;
			}
		}
		else
		{
			buttonMoveMenuLeft = false;
		}
		if ((double)Input.GetAxis("Horizontal") > 0.2)
		{
			if (!buttonMoveMenuRight)
			{
				if (ChangeSelect[caseSelected].GetComponent<MenuCase>() != null)
				{
					ChangeSelect[caseSelected].GetComponent<MenuCase>().Right();
				}
				buttonMoveMenuRight = true;
			}
		}
		else
		{
			buttonMoveMenuRight = false;
		}
		if ((double)Input.GetAxis("Horizontal") > -0.2 && (double)Input.GetAxis("Horizontal") < 0.2)
		{
			buttonMoveMenuLeft = false;
			buttonMoveMenuRight = false;
		}
	}

	public void NextMenu(UIAll[] nextObjectUI, GameObject[] nextObjectSelect, int nextSelect)
	{
		for (int i = 0; i < ObjectUI.Length; i++)
		{
			for (int j = 0; j < ObjectUI[i].UiObjects.Length; j++)
			{
				if (ObjectUI[i].UiObjects[j].GetComponent<Text>() != null)
				{
					ObjectUI[i].UiObjects[j].GetComponent<Text>().color = ObjectUI[i].UIColorObjects[j];
				}
				if (ObjectUI[i].UiObjects[j].GetComponent<Image>() != null)
				{
					ObjectUI[i].UiObjects[j].GetComponent<Image>().color = ObjectUI[i].UIColorObjects[j];
				}
			}
		}
		stopAnimation = 1f;
		active = true;
		iObjectUI = 0;
		tmTables = 0;
		caseSelected = nextSelect;
		ChangeSelect = nextObjectSelect;
		ObjectUI = nextObjectUI;
		for (int k = 0; k < ObjectUI.Length; k++)
		{
			ObjectUI[k].UIColorObjects = new Color[ObjectUI[k].UiObjects.Length];
		}
		imgChange.color = new Vector4(imgChange.color.r, imgChange.color.g, imgChange.color.b, 0f);
		Change.GetComponent<RectTransform>().anchoredPosition = new Vector4(posHChange, nextObjectSelect[caseSelected].GetComponent<RectTransform>().anchoredPosition.y);
		for (int l = 0; l < ObjectUI.Length; l++)
		{
			for (int m = 0; m < ObjectUI[l].UiObjects.Length; m++)
			{
				if (ObjectUI[l].UiObjects[m].GetComponent<Text>() != null)
				{
					ObjectUI[l].UIColorObjects[m] = ObjectUI[l].UiObjects[m].GetComponent<Text>().color;
					ObjectUI[l].UiObjects[m].GetComponent<Text>().color = new Color(ObjectUI[l].UiObjects[m].GetComponent<Text>().color.r, ObjectUI[l].UiObjects[m].GetComponent<Text>().color.g, ObjectUI[l].UiObjects[m].GetComponent<Text>().color.b, 0f);
				}
				if (ObjectUI[l].UiObjects[m].GetComponent<Image>() != null)
				{
					ObjectUI[l].UIColorObjects[m] = ObjectUI[l].UiObjects[m].GetComponent<Image>().color;
					ObjectUI[l].UiObjects[m].GetComponent<Image>().color = new Color(ObjectUI[l].UiObjects[m].GetComponent<Image>().color.r, ObjectUI[l].UiObjects[m].GetComponent<Image>().color.g, ObjectUI[l].UiObjects[m].GetComponent<Image>().color.b, 0f);
				}
			}
		}
	}

	public void PlaySidesChange()
	{
		au.PlayOneShot(soundSides, 0.25f);
	}
}
