using System;
using UnityEngine;

public class Character_Foot : MonoBehaviour
{
	[Serializable]
	public class FootSound
	{
		public string nameTag;

		public AudioClip[] sounds;
	}

	public FootSound[] footSounds;

	public int timeSilence = 15;

	private AudioSource au;

	private int tm;

	private void Start()
	{
		au = GetComponent<AudioSource>();
	}

	private void Update()
	{
		if (tm > 0)
		{
			tm--;
		}
	}

	public void foot()
	{
		if (!Physics.Raycast(base.transform.position + new Vector3(0f, 0.1f, 0f), -Vector3.up, out var hitInfo, 100f) || !(hitInfo.collider != null) || tm != 0)
		{
			return;
		}
		if (hitInfo.collider.gameObject.GetComponent<Triggers_FootTag>() != null)
		{
			for (int i = 0; i < footSounds.Length; i++)
			{
				if (hitInfo.collider.gameObject.GetComponent<Triggers_FootTag>().nameTagFoots == footSounds[i].nameTag)
				{
					au.clip = footSounds[i].sounds[UnityEngine.Random.Range(0, footSounds[i].sounds.Length)];
					au.pitch = UnityEngine.Random.Range(0.9f, 1.1f);
					au.Play();
					tm = timeSilence;
				}
			}
		}
		else
		{
			au.clip = footSounds[0].sounds[UnityEngine.Random.Range(0, footSounds[0].sounds.Length)];
			au.pitch = UnityEngine.Random.Range(0.9f, 1.1f);
			au.Play();
			tm = timeSilence;
		}
	}
}
