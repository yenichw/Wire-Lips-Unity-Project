using UnityEngine;
using UnityEngine.SceneManagement;

public class Level_Next : MonoBehaviour
{
	public void levelNext(string level)
	{
		SceneManager.LoadScene(level, LoadSceneMode.Single);
	}
}
