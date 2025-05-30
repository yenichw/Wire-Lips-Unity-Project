using UnityEngine;

public class Audio_Data : MonoBehaviour
{
	public AudioSource audioSound;

	public AudioClip[] sounds;

	[Header("Random")]
	[Range(-3f, 3f)]
	public float pitchMax = 1.05f;

	[Range(-3f, 3f)]
	public float pitchMin = 0.95f;

	private void Start()
	{
		if (GetComponent<AudioSource>() != null)
		{
			audioSound = GetComponent<AudioSource>();
		}
	}

	public void SoundPlay(int x)
	{
		audioSound.clip = sounds[x];
		audioSound.Play();
	}

	public void RandomPlay()
	{
		if (GetComponent<AudioSource>() != null)
		{
			audioSound = GetComponent<AudioSource>();
		}
		int num = Random.Range(0, sounds.Length);
		audioSound.pitch = 1f;
		audioSound.clip = sounds[num];
		audioSound.Play();
	}

	public void RandomPlayPitch()
	{
		if (GetComponent<AudioSource>() != null)
		{
			audioSound = GetComponent<AudioSource>();
		}
		int num = Random.Range(0, sounds.Length);
		audioSound.pitch = Random.Range(pitchMin, pitchMax);
		audioSound.clip = sounds[num];
		audioSound.Play();
	}

	public void SoundPlayPitch(int x)
	{
		if (GetComponent<AudioSource>() != null)
		{
			audioSound = GetComponent<AudioSource>();
		}
		audioSound.pitch = Random.Range(pitchMin, pitchMax);
		audioSound.clip = sounds[x];
		audioSound.Play();
	}
}
