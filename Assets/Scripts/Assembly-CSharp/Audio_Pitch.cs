using UnityEngine;

[AddComponentMenu("Functions/Audio/Pitch")]
public class Audio_Pitch : MonoBehaviour
{
	[Range(-3f, 3f)]
	public float _pitch = 1f;

	public bool smooth = true;

	[Range(0f, 1f)]
	public float speed = 0.1f;

	public AudioSource audioSource;

	public void Pitch(float x)
	{
		_pitch = x;
	}

	public void ReSpeed(float x)
	{
		speed = x;
	}

	public void ReSmooth(bool x)
	{
		smooth = x;
	}

	private void Start()
	{
		if (audioSource == null)
		{
			audioSource = GetComponent<AudioSource>();
		}
	}

	private void FixedUpdate()
	{
		if (smooth)
		{
			audioSource.pitch = Mathf.Lerp(audioSource.pitch, _pitch, speed);
			return;
		}
		if (audioSource.pitch > _pitch)
		{
			audioSource.pitch -= speed;
		}
		if (audioSource.pitch < _pitch)
		{
			audioSource.pitch += speed;
		}
	}
}
