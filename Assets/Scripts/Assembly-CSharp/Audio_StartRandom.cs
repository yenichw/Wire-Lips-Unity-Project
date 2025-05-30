using UnityEngine;

public class Audio_StartRandom : MonoBehaviour
{
	private AudioSource audioSource;

	public bool pitchRandom;

	[Range(-3f, 3f)]
	public float pitchMax;

	[Range(-3f, 3f)]
	public float pitchMin = 1f;

	public bool volumeRandom;

	[Range(-3f, 3f)]
	public float volumeMax;

	[Range(-3f, 3f)]
	public float volumeMin = 1f;

	private void Start()
	{
		if (audioSource == null)
		{
			audioSource = GetComponent<AudioSource>();
		}
		if (pitchRandom)
		{
			audioSource.pitch = Random.Range(pitchMin, pitchMax);
		}
		if (volumeRandom)
		{
			audioSource.volume = Random.Range(volumeMin, volumeMax);
		}
	}

	public void Play()
	{
		if (audioSource == null)
		{
			audioSource = GetComponent<AudioSource>();
		}
		if (pitchRandom)
		{
			audioSource.pitch = Random.Range(pitchMin, pitchMax);
		}
		if (volumeRandom)
		{
			audioSource.volume = Random.Range(volumeMin, volumeMax);
		}
		audioSource.Play();
	}
}
