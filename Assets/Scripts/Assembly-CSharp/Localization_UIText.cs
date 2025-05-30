using System.IO;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("Functions/Localization/Localization UI Text")]
public class Localization_UIText : MonoBehaviour
{
	public bool EveryEnable;

	public string NameFile;

	public int StringNumber;

	public bool GrandSymbol = true;

	public bool data;

	private int i;

	private bool StopEnable;

	private Text UIText;

	private string Stext;

	private string[] Strings;

	public void OnEnable()
	{
		if (!StopEnable || EveryEnable)
		{
			UIText = GetComponent<Text>();
			if (!data)
			{
				Strings = File.ReadAllLines("Data/Languages/" + GlobalGame.Language + "/" + NameFile + ".txt");
			}
			if (data)
			{
				Strings = File.ReadAllLines("Data/" + NameFile + ".txt");
			}
			if (GrandSymbol)
			{
				Stext = Strings[StringNumber - 1].ToUpper();
			}
			else
			{
				Stext = Strings[StringNumber - 1];
			}
			Stext = Stext.Replace("_", "\n");
			UIText.text = Stext;
			StopEnable = true;
		}
	}

	private void Start()
	{
		if (!data)
		{
			Strings = File.ReadAllLines("Data/Languages/" + GlobalGame.Language + "/" + NameFile + ".txt");
		}
		if (data)
		{
			Strings = File.ReadAllLines("Data/" + NameFile + ".txt");
		}
		if (GrandSymbol)
		{
			Stext = Strings[StringNumber - 1].ToUpper();
		}
		else
		{
			Stext = Strings[StringNumber - 1];
		}
		Stext = Stext.Replace("_", "\n");
		UIText.text = Stext;
	}

	public void ResetLanguage()
	{
		bool stopEnable = StopEnable;
		StopEnable = false;
		OnEnable();
		StopEnable = stopEnable;
	}

	public void ReString(int x)
	{
		StringNumber = x;
	}

	public void ReFile(string x)
	{
		NameFile = x;
	}

	public void DestroyObject()
	{
		Object.Destroy(base.gameObject);
	}
}
