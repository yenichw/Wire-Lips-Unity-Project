using RootMotion.FinalIK;
using UnityEngine;

namespace RootMotion.Demos
{
	[RequireComponent(typeof(Recoil))]
	public class RecoilTest : MonoBehaviour
	{
		public float magnitude = 1f;

		private Recoil recoil;

		private void Start()
		{
			recoil = GetComponent<Recoil>();
		}

		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.R) || Input.GetMouseButtonDown(0))
			{
				recoil.Fire(magnitude);
			}
		}

		private void OnGUI()
		{
			GUILayout.Label("Press R or LMB for procedural recoil.");
		}
	}
}
