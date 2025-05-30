using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuLoadGameNext : MonoBehaviour
{
	private string loadLevel;

	public void Start()
	{
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
	}

	public void LoadContinue()
	{
		loadLevel = "continue";
	}

	public void LoadNewGame()
	{
		loadLevel = "new game";
	}

	public void NextScene()
	{
		if (loadLevel == "continue")
		{
			GlobalGame.LoadingLevel = "SceneStartRoom";
			SceneManager.LoadScene("SceneLoading", LoadSceneMode.Single);
		}
		if (loadLevel == "new game")
		{
			GlobalGame.LoadingLevel = "SceneEpisode 1 - Start";
			SceneManager.LoadScene("SceneLoading", LoadSceneMode.Single);
		}
	}
}
