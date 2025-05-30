using UnityEngine;

public class LoadingOptions : MonoBehaviour
{
	private void Awake()
	{
		GlobalGame.Language = PlayerPrefs.GetString("Language", "English");
		GlobalGame.VolumeGame = PlayerPrefs.GetFloat("Volume", 1f);
		Object.Destroy(this);
	}
}
