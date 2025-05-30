using UnityEngine;

public class Level_LightingFogReColor : MonoBehaviour
{
	public Color reColor = new Color(1f, 1f, 1f, 1f);

	public void Recolor()
	{
		(Object.FindObjectsOfType(typeof(Level_LightingFog)) as Level_LightingFog[])[0].colorFog = reColor;
	}
}
