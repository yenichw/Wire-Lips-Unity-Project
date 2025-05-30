using UnityEngine;

public class Audio_IsplayingDestroy : MonoBehaviour
{
	private void Update()
	{
		if (!GetComponent<AudioSource>().isPlaying)
		{
			Object.Destroy(base.gameObject);
		}
	}
}
