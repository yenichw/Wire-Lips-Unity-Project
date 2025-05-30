using UnityEngine;

public class Particles_StopPlay : MonoBehaviour
{
	public ParticleSystem[] particles;

	public void Play()
	{
		for (int i = 0; i < particles.Length; i++)
		{
			particles[i].Play();
		}
	}

	public void Stop()
	{
		for (int i = 0; i < particles.Length; i++)
		{
			particles[i].Stop();
		}
	}
}
