using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class UI_TextCharTime : MonoBehaviour
{
	public float speedPrint = 1f;

	[Header("Text File")]
	public string NameFile;

	public int stringNum = 1;

	private string txt;

	private Text txtui;

	private float tmChar;

	private int iChar;

	private void Start()
	{
		txtui = GetComponent<Text>();
		if (NameFile != "")
		{
			txt = File.ReadAllLines("Data/Languages/" + GlobalGame.Language + "/" + NameFile + ".txt")[stringNum - 1];
		}
		else
		{
			txt = txtui.text;
		}
		txtui.text = "";
	}

	private void Update()
	{
		if (iChar < txt.Length)
		{
			tmChar += Time.deltaTime * speedPrint;
			if (tmChar >= 1f)
			{
				CharAdd();
				tmChar = 0f;
			}
		}
	}

	private void CharAdd()
	{
		txtui.text += txt[iChar];
		iChar++;
		if (GetComponent<AudioSource>() != null)
		{
			GetComponent<AudioSource>().Play();
		}
	}
}
