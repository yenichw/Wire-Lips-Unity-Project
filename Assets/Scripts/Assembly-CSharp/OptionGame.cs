using System.IO;
using UnityEngine;

public class OptionGame : MonoBehaviour
{
	public Camera[] cms;

	public Object[] qwe;

	public bool exceptionsLevel;

	private void Awake()
	{
		Application.targetFrameRate = 60;
		UpdateFull();
	}

	public void UpdateFull()
	{
		if (PlayerPrefs.GetString("Texture", "Hight") == "Hight")
		{
			QualitySettings.masterTextureLimit = 0;
		}
		if (PlayerPrefs.GetString("Texture", "Hight") == "Medium")
		{
			QualitySettings.masterTextureLimit = 1;
		}
		if (PlayerPrefs.GetString("Texture", "Hight") == "Low")
		{
			QualitySettings.masterTextureLimit = 2;
		}
		if (PlayerPrefs.GetString("Texture", "Hight") == "Terrible")
		{
			QualitySettings.masterTextureLimit = 3;
		}
		if (PlayerPrefs.GetString("Shadow", "Hight") == "Ultra")
		{
			QualitySettings.shadowResolution = ShadowResolution.VeryHigh;
			GlobalGame.Shadow = 1;
		}
		if (PlayerPrefs.GetString("Shadow", "Hight") == "Hight")
		{
			QualitySettings.shadowResolution = ShadowResolution.High;
			GlobalGame.Shadow = 1;
		}
		if (PlayerPrefs.GetString("Shadow", "Hight") == "Medium")
		{
			QualitySettings.shadowResolution = ShadowResolution.Medium;
			GlobalGame.Shadow = 1;
		}
		if (PlayerPrefs.GetString("Shadow", "Hight") == "Low")
		{
			QualitySettings.shadowResolution = ShadowResolution.Low;
			GlobalGame.Shadow = 1;
		}
		if (PlayerPrefs.GetString("Shadow", "Hight") == "Terrible")
		{
			QualitySettings.shadowResolution = ShadowResolution.Low;
			GlobalGame.Shadow = 0;
		}
		AudioListener.volume = PlayerPrefs.GetFloat("Volume", 1f);
		GlobalGame.VolumeGame = PlayerPrefs.GetFloat("Volume", 1f);
		if (PlayerPrefs.GetString("FullScreen", "Yes") == "Yes")
		{
			Screen.fullScreen = true;
		}
		if (PlayerPrefs.GetString("FullScreen", "Yes") == "No")
		{
			Screen.fullScreen = false;
		}
		GlobalGame.Language = PlayerPrefs.GetString("Language", "English");
		if (PlayerPrefs.GetString("Antialiasing", "None") == "None")
		{
			QualitySettings.antiAliasing = 0;
		}
		if (PlayerPrefs.GetString("Antialiasing", "None") == "Normal")
		{
			QualitySettings.antiAliasing = 1;
		}
		if (PlayerPrefs.GetString("Antialiasing", "None") == "Hight")
		{
			QualitySettings.antiAliasing = 2;
		}
		GlobalGame.SensitivityGame = PlayerPrefs.GetFloat("SensitivityMouse", 0.5f);
		if (PlayerPrefs.GetString("PixelGraphic", "None") == "Active" && !exceptionsLevel)
		{
			FunctionPixelGraphic();
		}
	}

	private void FunctionPixelGraphic()
	{
		Screen.fullScreen = true;
		Camera component = GameObject.FindWithTag("MainCamera").gameObject.GetComponent<Camera>();
		GameObject gameObject = GameObject.FindWithTag("Interface").gameObject;
		component.rect = new Rect(0f, 0f, 1f, 1f);
		RenderTexture renderTexture = Resources.Load("Game/PixelGraphic/PixelGraphic_Texture") as RenderTexture;
		renderTexture.width = Screen.width / 3;
		renderTexture.height = Screen.height / 3;
		component.targetTexture = renderTexture;
		Object.Instantiate(Resources.Load<GameObject>("Game/PixelGraphic/InterfacePixel"), gameObject.transform.parent).transform.SetSiblingIndex(0);
	}

	public void ItemsBonusFind()
	{
		if (File.Exists("Data/Save/BVCFDBr") && File.Exists("Data/Save/bDBg") && File.Exists("Data/Save/ETBTBgf") && File.Exists("Data/Save/423") && File.Exists("Data/Save/999") && File.Exists("Data/Save/VSDFV") && File.Exists("Data/Save/BVDFGB") && File.Exists("Data/Save/GFBe5t"))
		{
			File.Create("Data/Save/AllItems");
		}
	}
}
