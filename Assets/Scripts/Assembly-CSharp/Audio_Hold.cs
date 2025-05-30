using UnityEngine;

public class Audio_Hold : MonoBehaviour
{
	[Range(0f, 1f)]
	public float speed = 0.1f;

	public bool usePause = true;

	private int hold;

	private float volume;

	private AudioSource au;

	private bool On;

	private void Start()
	{
		au = GetComponent<AudioSource>();
		volume = au.volume;
		au.volume = 0f;
	}

	private void Update()
	{
		if (hold > 0)
		{
			hold--;
			if (!On)
			{
				au.Play();
				On = true;
			}
			au.volume = Mathf.Lerp(au.volume, volume, speed);
		}
		if (hold != 0)
		{
			return;
		}
		if (On && au.volume < 0.01f)
		{
			On = false;
			if (!usePause)
			{
				au.Stop();
			}
			else
			{
				au.Pause();
			}
		}
		au.volume = Mathf.Lerp(au.volume, 0f, speed);
	}

	public void Hold()
	{
		hold = 2;
	}
}
