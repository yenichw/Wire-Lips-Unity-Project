using UnityEngine;

[ExecuteInEditMode]
public class Level_Lighting : MonoBehaviour
{
	public Color colorAmbient = new Color(1f, 1f, 1f, 1f);

	public float speedColor = 0.1f;

	private void Update()
	{
		if (!Application.isPlaying)
		{
			RenderSettings.ambientLight = colorAmbient;
		}
		else
		{
			RenderSettings.ambientLight = Color.Lerp(RenderSettings.ambientLight, colorAmbient, Time.deltaTime * speedColor);
		}
	}
}
