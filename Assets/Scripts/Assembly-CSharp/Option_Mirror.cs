using UnityEngine;

public class Option_Mirror : MonoBehaviour
{
	private void Start()
	{
		PIDI_PlanarReflection component = GetComponent<PIDI_PlanarReflection>();
		if (PlayerPrefs.GetString("Mirror", "Hight") == "Low")
		{
			component.v_resMultiplier = 0.25f;
		}
		if (PlayerPrefs.GetString("Mirror", "Hight") == "Medium")
		{
			component.v_resMultiplier = 0.5f;
		}
		if (PlayerPrefs.GetString("Mirror", "Hight") == "Hight")
		{
			component.v_resMultiplier = 1f;
		}
		Object.Destroy(this);
	}
}
