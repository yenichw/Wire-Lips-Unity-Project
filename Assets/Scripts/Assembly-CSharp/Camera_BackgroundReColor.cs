using UnityEngine;

public class Camera_BackgroundReColor : MonoBehaviour
{
	public Color reColor = new Color(1f, 1f, 1f, 1f);

	public void Recolor()
	{
		(Object.FindObjectsOfType(typeof(Camera_Background)) as Camera_Background[])[0].color = reColor;
	}
}
