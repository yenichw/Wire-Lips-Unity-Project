using UnityEngine;

[AddComponentMenu("Functions/Audio/Volume")]
public class Audio_Volume : MonoBehaviour
{
	[Range(0f, 1f)]
	public float _volume = 1f;

	public bool smooth = true;

	public float speed = 1f;

	public AudioSource audioSource;

	private bool desAlph;

	private bool lerpStop;

	public void Volume(float x)
	{
		_volume = x;
		lerpStop = false;
	}

	public void ReSpeed(float x)
	{
		speed = x;
	}

	public void ReSmooth(bool x)
	{
		smooth = x;
	}

	public void destroyAlpha()
	{
		desAlph = true;
	}

	public void LerpStop()
	{
		lerpStop = true;
		_volume = 0f;
	}

	public void ReSoundPlay(AudioClip x)
	{
		audioSource.clip = x;
		audioSource.Play();
	}

	private void Start()
	{
		if (audioSource == null)
		{
			audioSource = GetComponent<AudioSource>();
		}
	}

	private void Update()
	{
		if (smooth)
		{
			audioSource.volume = Mathf.Lerp(audioSource.volume, _volume, Time.deltaTime * speed);
		}
		else
		{
			if (audioSource.volume > _volume)
			{
				audioSource.volume -= Time.deltaTime * speed;
				if (audioSource.volume < _volume)
				{
					audioSource.volume = _volume;
				}
			}
			if (audioSource.volume < _volume)
			{
				audioSource.volume += Time.deltaTime * speed;
				if (audioSource.volume > _volume)
				{
					audioSource.volume = _volume;
				}
			}
		}
		if (lerpStop && !desAlph)
		{
			audioSource.volume = Mathf.Lerp(audioSource.volume, 0f, Time.deltaTime * speed);
			if (audioSource.volume < 0.01f)
			{
				lerpStop = false;
				audioSource.volume = 0f;
				audioSource.Stop();
			}
		}
		if (desAlph)
		{
			_volume = 0f;
			if ((double)audioSource.volume <= 0.0001)
			{
				Object.Destroy(base.gameObject);
			}
		}
	}
}
