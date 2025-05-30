using UnityEngine;

public class Light_Lerp : MonoBehaviour
{
	[Range(0f, 1f)]
	public float speed = 0.1f;

	public bool LightNow;

	public float intensityNow = 1f;

	private Light lg;

	private float intensity;

	private void OnEnable()
	{
		lg = GetComponent<Light>();
		intensity = lg.intensity;
		if (LightNow)
		{
			intensity = intensityNow;
		}
		lg.intensity = 0f;
	}

	private void Update()
	{
		lg.intensity = Mathf.Lerp(lg.intensity, intensity, speed);
	}
}
