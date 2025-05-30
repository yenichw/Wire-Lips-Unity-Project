using System;
using RootMotion.FinalIK;
using UnityEngine;

namespace RootMotion.Demos
{
	[RequireComponent(typeof(AimIK))]
	[RequireComponent(typeof(FullBodyBipedIK))]
	public class AnimatorController3rdPersonIK : AnimatorController3rdPerson
	{
		[Range(0f, 1f)]
		public float headLookWeight = 1f;

		public Vector3 gunHoldOffset;

		public Vector3 leftHandOffset;

		public Recoil recoil;

		private AimIK aim;

		private FullBodyBipedIK ik;

		private Vector3 headLookAxis;

		private Vector3 leftHandPosRelToRightHand;

		private Quaternion leftHandRotRelToRightHand;

		private Vector3 aimTarget;

		private Quaternion rightHandRotation;

		protected override void Start()
		{
			base.Start();
			aim = GetComponent<AimIK>();
			ik = GetComponent<FullBodyBipedIK>();
			IKSolverFullBodyBiped solver = ik.solver;
			solver.OnPreRead = (IKSolver.UpdateDelegate)Delegate.Combine(solver.OnPreRead, new IKSolver.UpdateDelegate(OnPreRead));
			aim.enabled = false;
			ik.enabled = false;
			headLookAxis = ik.references.head.InverseTransformVector(ik.references.root.forward);
			animator.SetLayerWeight(1, 1f);
		}

		public override void Move(Vector3 moveInput, bool isMoving, Vector3 faceDirection, Vector3 aimTarget)
		{
			base.Move(moveInput, isMoving, faceDirection, aimTarget);
			this.aimTarget = aimTarget;
			Read();
			AimIK();
			FBBIK();
			HeadLookAt(aimTarget);
		}

		private void Read()
		{
			leftHandPosRelToRightHand = ik.references.rightHand.InverseTransformPoint(ik.references.leftHand.position);
			leftHandRotRelToRightHand = Quaternion.Inverse(ik.references.rightHand.rotation) * ik.references.leftHand.rotation;
		}

		private void AimIK()
		{
			aim.solver.IKPosition = aimTarget;
			aim.solver.Update();
		}

		private void FBBIK()
		{
			rightHandRotation = ik.references.rightHand.rotation;
			Vector3 vector = ik.references.rightHand.rotation * gunHoldOffset;
			ik.solver.rightHandEffector.positionOffset += vector;
			if (recoil != null)
			{
				recoil.SetHandRotations(rightHandRotation * leftHandRotRelToRightHand, rightHandRotation);
			}
			ik.solver.Update();
			if (recoil != null)
			{
				ik.references.rightHand.rotation = recoil.rotationOffset * rightHandRotation;
				ik.references.leftHand.rotation = recoil.rotationOffset * rightHandRotation * leftHandRotRelToRightHand;
			}
			else
			{
				ik.references.rightHand.rotation = rightHandRotation;
				ik.references.leftHand.rotation = rightHandRotation * leftHandRotRelToRightHand;
			}
		}

		private void OnPreRead()
		{
			Quaternion quaternion = ((recoil != null) ? (recoil.rotationOffset * rightHandRotation) : rightHandRotation);
			Vector3 vector = ik.references.rightHand.position + ik.solver.rightHandEffector.positionOffset + quaternion * leftHandPosRelToRightHand;
			ik.solver.leftHandEffector.positionOffset += vector - ik.references.leftHand.position - ik.solver.leftHandEffector.positionOffset + quaternion * leftHandOffset;
		}

		private void HeadLookAt(Vector3 lookAtTarget)
		{
			Quaternion b = Quaternion.FromToRotation(ik.references.head.rotation * headLookAxis, lookAtTarget - ik.references.head.position);
			ik.references.head.rotation = Quaternion.Lerp(Quaternion.identity, b, headLookWeight) * ik.references.head.rotation;
		}

		private void OnDestroy()
		{
			if (ik != null)
			{
				IKSolverFullBodyBiped solver = ik.solver;
				solver.OnPreRead = (IKSolver.UpdateDelegate)Delegate.Remove(solver.OnPreRead, new IKSolver.UpdateDelegate(OnPreRead));
			}
		}
	}
}
