using UnityEngine;

public class Audio_LowPassFilterObjectActive : MonoBehaviour
{
	public GameObject objectsActive;

	public AudioLowPassFilter audioLPS;

	public float alpsMax = 10000f;

	public float alpsMin = 25f;

	private void Start()
	{
		if (audioLPS == null)
		{
			audioLPS = GetComponent<AudioLowPassFilter>();
		}
	}

	private void Update()
	{
		if (objectsActive.activeInHierarchy)
		{
			audioLPS.cutoffFrequency = Mathf.Lerp(audioLPS.cutoffFrequency, alpsMax, Time.deltaTime * 5f);
		}
		else
		{
			audioLPS.cutoffFrequency = Mathf.Lerp(audioLPS.cutoffFrequency, alpsMin, Time.deltaTime * 5f);
		}
	}
}
