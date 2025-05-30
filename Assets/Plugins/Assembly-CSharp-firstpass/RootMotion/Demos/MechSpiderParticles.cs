using UnityEngine;

namespace RootMotion.Demos
{
	public class MechSpiderParticles : MonoBehaviour
	{
		public MechSpiderController mechSpiderController;

		private ParticleSystem particles;

		private void Start()
		{
			particles = (ParticleSystem)GetComponent(typeof(ParticleSystem));
		}

		private void Update()
		{
			float magnitude = mechSpiderController.inputVector.magnitude;
			float constant = Mathf.Clamp(magnitude * 50f, 30f, 50f);
			ParticleSystem.EmissionModule emission = particles.emission;
			emission.rateOverTime = new ParticleSystem.MinMaxCurve(constant);
			ParticleSystem.MainModule main = particles.main;
			main.startColor = new Color(particles.main.startColor.color.r, particles.main.startColor.color.g, particles.main.startColor.color.b, Mathf.Clamp(magnitude, 0.4f, 1f));
		}
	}
}
