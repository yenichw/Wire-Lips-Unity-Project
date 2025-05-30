using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class Option_PostProcessing : MonoBehaviour
{
	private void Start()
	{
		PostProcessVolume component = GetComponent<PostProcessVolume>();
		if (PlayerPrefs.GetInt("MotionBlur", 1) == 1)
		{
			component.profile.GetSetting<MotionBlur>().active = true;
		}
		else
		{
			component.profile.GetSetting<MotionBlur>().active = false;
		}
		if (PlayerPrefs.GetInt("AO", 1) == 1)
		{
			component.profile.GetSetting<AmbientOcclusion>().active = true;
		}
		else
		{
			component.profile.GetSetting<AmbientOcclusion>().active = false;
		}
		Object.Destroy(this);
	}
}
