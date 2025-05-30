using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Level_LoadingLevel : MonoBehaviour
{
	public AsyncOperation asyncLoad;

	public Text textNextEpisode;

	public Text textDescription;

	public Image blackScreen;

	public Animator animButton;

	[Header("Background Loading")]
	public GameObject objectBackground;

	public Sprite backgroundEpisode1;

	public Sprite backgroundEpisode2;

	public Sprite backgroundEpisode3;

	public Sprite backgroundEpisode4;

	public Sprite backgroundEpisode5;

	private bool go;

	private float timeGo;

	private bool buttonGo;

	private void Start()
	{
		textNextEpisode.text = "";
		textDescription.text = "";
		if (GlobalGame.LoadingLevel == "SceneEpisode 1 - Start")
		{
			textNextEpisode.text = File.ReadAllLines("Data/Languages/" + GlobalGame.Language + "/Episode.txt")[0];
			textDescription.text = File.ReadAllLines("Data/Languages/" + GlobalGame.Language + "/LoadEpisode.txt")[0];
			buttonGo = true;
			objectBackground.SetActive(value: true);
			objectBackground.GetComponent<Image>().sprite = backgroundEpisode1;
		}
		if (GlobalGame.LoadingLevel == "SceneEpisode 2")
		{
			textNextEpisode.text = File.ReadAllLines("Data/Languages/" + GlobalGame.Language + "/Episode.txt")[1];
			textDescription.text = File.ReadAllLines("Data/Languages/" + GlobalGame.Language + "/LoadEpisode.txt")[1];
			buttonGo = true;
			objectBackground.SetActive(value: true);
			objectBackground.GetComponent<Image>().sprite = backgroundEpisode2;
		}
		if (GlobalGame.LoadingLevel == "SceneEpisode 3")
		{
			textNextEpisode.text = File.ReadAllLines("Data/Languages/" + GlobalGame.Language + "/Episode.txt")[2];
			textDescription.text = File.ReadAllLines("Data/Languages/" + GlobalGame.Language + "/LoadEpisode.txt")[2];
			buttonGo = true;
			objectBackground.SetActive(value: true);
			objectBackground.GetComponent<Image>().sprite = backgroundEpisode3;
		}
		if (GlobalGame.LoadingLevel == "SceneEpisode 4")
		{
			textNextEpisode.text = File.ReadAllLines("Data/Languages/" + GlobalGame.Language + "/Episode.txt")[3];
			textDescription.text = File.ReadAllLines("Data/Languages/" + GlobalGame.Language + "/LoadEpisode.txt")[3];
			buttonGo = true;
			objectBackground.SetActive(value: true);
			objectBackground.GetComponent<Image>().sprite = backgroundEpisode4;
		}
		if (GlobalGame.LoadingLevel == "SceneEpisode 5")
		{
			textNextEpisode.text = File.ReadAllLines("Data/Languages/" + GlobalGame.Language + "/Episode.txt")[4];
			textDescription.text = File.ReadAllLines("Data/Languages/" + GlobalGame.Language + "/LoadEpisode.txt")[4];
			buttonGo = true;
			objectBackground.SetActive(value: true);
			objectBackground.GetComponent<Image>().sprite = backgroundEpisode5;
		}
		StartCoroutine(TimeStart());
	}

	private void LoadGo()
	{
		asyncLoad = SceneManager.LoadSceneAsync(GlobalGame.LoadingLevel);
		asyncLoad.allowSceneActivation = false;
	}

	private void Update()
	{
		if (go)
		{
			timeGo += Time.deltaTime;
			blackScreen.color = new Color(0f, 0f, 0f, timeGo);
			if (timeGo >= 1f)
			{
				asyncLoad.allowSceneActivation = true;
			}
		}
		if (asyncLoad == null || !(asyncLoad.progress >= 0.9f))
		{
			return;
		}
		if (!buttonGo)
		{
			go = true;
			return;
		}
		animButton.enabled = true;
		if (Input.GetButtonDown("Space"))
		{
			go = true;
		}
	}

	private IEnumerator TimeStart()
	{
		yield return new WaitForSeconds(0.5f);
		LoadGo();
	}
}
