using UnityEngine;

public class CameraStart : MonoBehaviour
{
	private void Start()
	{
		if (PlayerPrefs.GetString("PixelGraphic", "None") == "Active" && !GameObject.FindWithTag("ControllerOptionGame").gameObject.transform.Find("OptionGame").gameObject.GetComponent<OptionGame>().exceptionsLevel)
		{
			FunctionPixelGraphic();
		}
	}

	private void FunctionPixelGraphic()
	{
		GetComponent<Camera>().targetTexture = Resources.Load("Game/PixelGraphic/PixelGraphic_Texture") as RenderTexture;
	}
}
