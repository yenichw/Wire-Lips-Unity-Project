using UnityEngine;

[AddComponentMenu("Functions/Audio/Foot Random")]
public class Audio_FootRandom : MonoBehaviour
{
	public AudioClip[] Foots;

	private float volStandart;

	private AudioSource au;

	private void Start()
	{
		au = GetComponent<AudioSource>();
		volStandart = au.volume;
	}

	public void StepFoot()
	{
		au.clip = Foots[Random.Range(0, Foots.Length)];
		au.pitch = Random.Range(0.85f, 1.15f);
		au.volume = volStandart + Random.Range(-0.05f, 0.05f);
		au.Play();
	}
}
