using UnityEngine;

public class Audio_StepFoot : MonoBehaviour
{
	public AudioClip[] soundsFoot;

	[Range(0f, 1f)]
	public float volume = 0.5f;

	[HideInInspector]
	public bool active = true;

	private AudioSource audioS;

	private float timeStop;

	private void Start()
	{
		GameObject obj = new GameObject();
		obj.transform.parent = base.transform;
		obj.transform.localPosition = Vector3.zero;
		obj.name = "AudioSource Foots";
		AudioSource audioSource = obj.AddComponent(typeof(AudioSource)) as AudioSource;
		audioS = audioSource;
		audioS.spatialBlend = 1f;
		audioS.playOnAwake = false;
		audioS.dopplerLevel = 0f;
		audioS.volume = volume;
	}

	private void Update()
	{
		if (timeStop > 0f)
		{
			timeStop -= Time.deltaTime;
			if (timeStop < 0f)
			{
				timeStop = 0f;
			}
		}
	}

	public void SoundStepFoot()
	{
		if (timeStop == 0f && active)
		{
			audioS.pitch = Random.Range(0.85f, 1.15f);
			audioS.clip = soundsFoot[Random.Range(0, soundsFoot.Length)];
			audioS.Play();
			timeStop = 0.2f;
		}
	}
}
