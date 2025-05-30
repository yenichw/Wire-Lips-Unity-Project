using UnityEngine;

public class Audio_Functions : MonoBehaviour
{
	[Range(-3f, 3f)]
	public float pitchMaxRandom;

	[Range(-3f, 3f)]
	public float pitchMinRandom = 1f;

	public void PlayRandomPitch()
	{
		GetComponent<AudioSource>().pitch = Random.Range(pitchMinRandom, pitchMaxRandom);
		GetComponent<AudioSource>().Play();
	}
}
