using UnityEngine;

public class Random_MaterialColorEmissionTime : MonoBehaviour
{
	public bool active;

	public Color colorStart;

	public Color colorFinish;

	public int timeRandom = 500;

	private Renderer rend;

	private bool colorOn;

	private void Start()
	{
		rend = GetComponent<Renderer>();
		if (Random.Range(0, 1000) <= 500)
		{
			colorOn = true;
		}
		else
		{
			colorOn = false;
		}
	}

	private void Update()
	{
		if (active)
		{
			if (Random.Range(0, timeRandom) == 0)
			{
				colorOn = !colorOn;
			}
			if (!colorOn)
			{
				rend.material.SetColor("_EmissionColor", Color.Lerp(rend.material.GetColor("_EmissionColor"), colorStart, 0.1f));
			}
			else
			{
				rend.material.SetColor("_EmissionColor", Color.Lerp(rend.material.GetColor("_EmissionColor"), colorFinish, 0.1f));
			}
		}
		else
		{
			rend.material.SetColor("_EmissionColor", Color.Lerp(rend.material.GetColor("_EmissionColor"), colorStart, 0.1f));
		}
	}

	public void Activation(bool x)
	{
		active = x;
	}
}
