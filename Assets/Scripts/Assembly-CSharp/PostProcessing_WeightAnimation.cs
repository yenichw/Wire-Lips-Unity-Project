using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessing_WeightAnimation : MonoBehaviour
{
	public bool active;

	public GameObject filter;

	public float speed = 1f;

	public AnimationCurve curveWeight;

	private PostProcessVolume filterPP;

	private float timeCurve;

	private float speedLerp;

	private void Start()
	{
		filterPP = filter.GetComponent<PostProcessVolume>();
	}

	private void Update()
	{
		if (active)
		{
			timeCurve += Time.deltaTime * speed;
			if (timeCurve > 1f)
			{
				timeCurve = 0f;
			}
			filterPP.weight = Mathf.Lerp(filterPP.weight, curveWeight.Evaluate(timeCurve), Time.deltaTime * speedLerp);
			if (speedLerp < 100f)
			{
				speedLerp += Time.deltaTime * 5f;
			}
		}
		else if (filterPP.weight > 0f)
		{
			filterPP.weight -= Time.deltaTime;
			if (filterPP.weight <= 0f)
			{
				filterPP.weight = 0f;
				filter.SetActive(value: false);
			}
		}
	}

	public void Activation(bool x)
	{
		active = x;
		if (x)
		{
			filter.SetActive(value: true);
		}
	}
}
