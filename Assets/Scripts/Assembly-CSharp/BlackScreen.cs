using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BlackScreen : MonoBehaviour
{
	public float speedStart = 1f;

	public float timeStart;

	private Image img;

	private bool On;

	private bool blackBlack;

	private void Start()
	{
		img = GetComponent<Image>();
		img.color = new Vector4(0f, 0f, 0f, 1f);
	}

	private void Update()
	{
		if (timeStart > 0f)
		{
			timeStart -= Time.deltaTime;
			if (timeStart < 0f)
			{
				timeStart = 0f;
			}
		}
		if (timeStart != 0f)
		{
			return;
		}
		if (!blackBlack)
		{
			if (!On)
			{
				img.color = Vector4.Lerp(img.color, new Vector4(0f, 0f, 0f, 0f), Time.deltaTime * speedStart);
				AudioListener.volume = Mathf.Lerp(AudioListener.volume, GlobalGame.VolumeGame, Time.deltaTime * speedStart);
				return;
			}
			if (img.color.a < 1f)
			{
				img.color = new Vector4(0f, 0f, 0f, img.color.a + Time.deltaTime);
			}
			if (AudioListener.volume > 0f)
			{
				AudioListener.volume -= Time.deltaTime;
			}
			if (img.color.a >= 1f && AudioListener.volume <= 0f)
			{
				LevelOff();
			}
		}
		else
		{
			img.color = new Vector4(0f, 0f, 0f, 1f);
		}
	}

	public void nextLevel(string level)
	{
		GlobalGame.LoadingLevel = level;
		On = true;
	}

	public void sharplyLevel(string level)
	{
		GlobalGame.LoadingLevel = level;
		SceneManager.LoadScene("SceneLoading", LoadSceneMode.Single);
	}

	public void sharplyOnlyLevel(string level)
	{
		SceneManager.LoadScene(level, LoadSceneMode.Single);
	}

	private void LevelOff()
	{
		SceneManager.LoadScene("SceneLoading", LoadSceneMode.Single);
	}

	public void SaveFileActiveLoadLevel(string xFile)
	{
		File.Create("Data/Save/" + xFile);
		StreamWriter streamWriter = File.CreateText("Data/Save/Continue");
		streamWriter.WriteLine(xFile ?? "");
		streamWriter.Close();
	}

	public void OnlyBlack(bool x)
	{
		blackBlack = x;
		img.color = new Vector4(0f, 0f, 0f, 1f);
	}
}
