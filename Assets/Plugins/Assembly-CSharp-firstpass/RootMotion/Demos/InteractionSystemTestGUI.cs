using RootMotion.FinalIK;
using UnityEngine;

namespace RootMotion.Demos
{
	[RequireComponent(typeof(InteractionSystem))]
	public class InteractionSystemTestGUI : MonoBehaviour
	{
		[Tooltip("The object to interact to")]
		[SerializeField]
		private InteractionObject interactionObject;

		[Tooltip("The effectors to interact with")]
		[SerializeField]
		private FullBodyBipedEffector[] effectors;

		private InteractionSystem interactionSystem;

		private void Awake()
		{
			interactionSystem = GetComponent<InteractionSystem>();
		}

		private void OnGUI()
		{
			if (interactionSystem == null)
			{
				return;
			}
			if (GUILayout.Button("Start Interaction With " + interactionObject.name))
			{
				if (effectors.Length == 0)
				{
					Debug.Log("Please select the effectors to interact with.");
				}
				FullBodyBipedEffector[] array = effectors;
				foreach (FullBodyBipedEffector effectorType in array)
				{
					interactionSystem.StartInteraction(effectorType, interactionObject, interrupt: true);
				}
			}
			if (effectors.Length == 0)
			{
				return;
			}
			if (interactionSystem.IsPaused(effectors[0]) && GUILayout.Button("Resume Interaction With " + interactionObject.name))
			{
				interactionSystem.ResumeAll();
			}
			if (GUILayout.Button("Stop " + interactionObject.name))
			{
				for (int j = 0; j < effectors.Length; j++)
				{
					interactionSystem.StopInteraction(effectors[j]);
				}
			}
			for (int k = 0; k < effectors.Length; k++)
			{
				GUILayout.Label("Progress [" + k + "] :" + interactionSystem.GetProgress(effectors[k]));
			}
		}
	}
}
