using UnityEngine;

[ExecuteInEditMode]
public class Level_LightingFog : MonoBehaviour
{
	public Color colorFog = new Color(1f, 1f, 1f, 1f);

	[Range(0f, 1f)]
	public float speedColor = 0.05f;

	private void Update()
	{
		if (!Application.isPlaying)
		{
			RenderSettings.fogColor = colorFog;
		}
		else
		{
			RenderSettings.fogColor = Color.Lerp(RenderSettings.fogColor, colorFog, speedColor);
		}
	}

	public void FogActivation(bool x)
	{
		RenderSettings.fog = x;
	}
}
