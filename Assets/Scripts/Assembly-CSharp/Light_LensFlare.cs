using UnityEngine;

[AddComponentMenu("Functions/Light/Light Lens Flare")]
public class Light_LensFlare : MonoBehaviour
{
	public LensFlare lensf;

	public Light lg;

	private void Update()
	{
		if (!lg.gameObject.activeInHierarchy)
		{
			lensf.color = new Color(0f, 0f, 0f, 0f);
		}
		else
		{
			lensf.color = new Color(lg.intensity, lg.intensity, lg.intensity, 1f);
		}
	}

	private void Reset()
	{
		if (GetComponent<LensFlare>() != null)
		{
			lensf = GetComponent<LensFlare>();
		}
		if (GetComponent<Light>() != null)
		{
			lg = GetComponent<Light>();
		}
	}
}
