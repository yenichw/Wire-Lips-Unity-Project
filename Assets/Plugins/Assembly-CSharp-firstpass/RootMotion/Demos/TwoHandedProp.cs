using System;
using RootMotion.FinalIK;
using UnityEngine;

namespace RootMotion.Demos
{
	public class TwoHandedProp : MonoBehaviour
	{
		[Tooltip("The left hand target parented to the right hand.")]
		public Transform leftHandTarget;

		private FullBodyBipedIK ik;

		private Vector3 targetPosRelativeToRight;

		private Quaternion targetRotRelativeToRight;

		private void Start()
		{
			ik = GetComponent<FullBodyBipedIK>();
			IKSolverFullBodyBiped solver = ik.solver;
			solver.OnPostUpdate = (IKSolver.UpdateDelegate)Delegate.Combine(solver.OnPostUpdate, new IKSolver.UpdateDelegate(AfterFBBIK));
			ik.solver.leftHandEffector.positionWeight = 1f;
			ik.solver.rightHandEffector.positionWeight = 1f;
			if (ik.solver.rightHandEffector.target == null)
			{
				Debug.LogError("Right Hand Effector needs a Target in this demo.");
			}
		}

		private void LateUpdate()
		{
			targetPosRelativeToRight = ik.references.rightHand.InverseTransformPoint(leftHandTarget.position);
			targetRotRelativeToRight = Quaternion.Inverse(ik.references.rightHand.rotation) * leftHandTarget.rotation;
			ik.solver.leftHandEffector.position = ik.solver.rightHandEffector.target.position + ik.solver.rightHandEffector.target.rotation * targetPosRelativeToRight;
			ik.solver.leftHandEffector.rotation = ik.solver.rightHandEffector.target.rotation * targetRotRelativeToRight;
		}

		private void AfterFBBIK()
		{
			ik.solver.leftHandEffector.bone.rotation = ik.solver.leftHandEffector.rotation;
			ik.solver.rightHandEffector.bone.rotation = ik.solver.rightHandEffector.rotation;
		}

		private void OnDestroy()
		{
			if (ik != null)
			{
				IKSolverFullBodyBiped solver = ik.solver;
				solver.OnPostUpdate = (IKSolver.UpdateDelegate)Delegate.Remove(solver.OnPostUpdate, new IKSolver.UpdateDelegate(AfterFBBIK));
			}
		}
	}
}
