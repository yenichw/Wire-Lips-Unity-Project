using UnityEngine;

public class Level_LightingReColor : MonoBehaviour
{
	public Color reColor = new Color(1f, 1f, 1f, 1f);

	public void Recolor()
	{
		(Object.FindObjectsOfType(typeof(Level_Lighting)) as Level_Lighting[])[0].colorAmbient = reColor;
	}

	public void RecolorFast()
	{
		Recolor();
		RenderSettings.ambientLight = reColor;
	}
}
