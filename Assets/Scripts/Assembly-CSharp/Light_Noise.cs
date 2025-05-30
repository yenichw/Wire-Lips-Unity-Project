using UnityEngine;

[AddComponentMenu("Functions/Light/Light Noise")]
public class Light_Noise : MonoBehaviour
{
	public bool randomActive;

	public float RangeMax;

	public float RangeMin;

	public float SpeedRandomRange;

	[Range(0f, 1f)]
	public float speedLerpRange = 0.1f;

	[Space(10f)]
	public float IntensityMax;

	public float IntensityMin;

	public float SpeedRandomIntensity;

	[Range(0f, 1f)]
	public float speedLerpIntensity = 0.1f;

	private Light Li;

	private float LiRange;

	private float LiInten;

	private void Start()
	{
		Li = GetComponent<Light>();
		LiRange = Li.range;
	}

	private void Update()
	{
		if (randomActive)
		{
			LiRange += Random.Range(0f - SpeedRandomRange, SpeedRandomRange);
			if (LiRange > RangeMax)
			{
				LiRange -= SpeedRandomRange;
			}
			if (LiRange < RangeMin)
			{
				LiRange += SpeedRandomRange;
			}
			LiInten += Random.Range(0f - SpeedRandomIntensity, SpeedRandomIntensity);
			if (LiInten > IntensityMax)
			{
				LiInten -= SpeedRandomIntensity;
			}
			if (LiInten < IntensityMin)
			{
				LiInten += SpeedRandomIntensity;
			}
			Li.range = Mathf.Lerp(Li.range, LiRange, speedLerpRange);
			Li.intensity = Mathf.Lerp(Li.intensity, LiInten, speedLerpIntensity);
		}
	}

	public void activation(bool x)
	{
		randomActive = x;
	}
}
