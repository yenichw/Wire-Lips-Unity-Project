using RootMotion.FinalIK;
using UnityEngine;

namespace RootMotion.Demos
{
	public class BipedIKvsAnimatorIK : MonoBehaviour
	{
		[LargeHeader("References")]
		public Animator animator;

		public BipedIK bipedIK;

		[LargeHeader("Look At")]
		public Transform lookAtTargetBiped;

		public Transform lookAtTargetAnimator;

		[Range(0f, 1f)]
		public float lookAtWeight = 1f;

		[Range(0f, 1f)]
		public float lookAtBodyWeight = 1f;

		[Range(0f, 1f)]
		public float lookAtHeadWeight = 1f;

		[Range(0f, 1f)]
		public float lookAtEyesWeight = 1f;

		[Range(0f, 1f)]
		public float lookAtClampWeight = 0.5f;

		[Range(0f, 1f)]
		public float lookAtClampWeightHead = 0.5f;

		[Range(0f, 1f)]
		public float lookAtClampWeightEyes = 0.5f;

		[LargeHeader("Foot")]
		public Transform footTargetBiped;

		public Transform footTargetAnimator;

		[Range(0f, 1f)]
		public float footPositionWeight;

		[Range(0f, 1f)]
		public float footRotationWeight;

		[LargeHeader("Hand")]
		public Transform handTargetBiped;

		public Transform handTargetAnimator;

		[Range(0f, 1f)]
		public float handPositionWeight;

		[Range(0f, 1f)]
		public float handRotationWeight;

		private void OnAnimatorIK(int layer)
		{
			animator.transform.rotation = bipedIK.transform.rotation;
			Vector3 vector = animator.transform.position - bipedIK.transform.position;
			lookAtTargetAnimator.position = lookAtTargetBiped.position + vector;
			bipedIK.SetLookAtPosition(lookAtTargetBiped.position);
			bipedIK.SetLookAtWeight(lookAtWeight, lookAtBodyWeight, lookAtHeadWeight, lookAtEyesWeight, lookAtClampWeight, lookAtClampWeightHead, lookAtClampWeightEyes);
			animator.SetLookAtPosition(lookAtTargetAnimator.position);
			animator.SetLookAtWeight(lookAtWeight, lookAtBodyWeight, lookAtHeadWeight, lookAtEyesWeight, lookAtClampWeight);
			footTargetAnimator.position = footTargetBiped.position + vector;
			footTargetAnimator.rotation = footTargetBiped.rotation;
			bipedIK.SetIKPosition(AvatarIKGoal.LeftFoot, footTargetBiped.position);
			bipedIK.SetIKRotation(AvatarIKGoal.LeftFoot, footTargetBiped.rotation);
			bipedIK.SetIKPositionWeight(AvatarIKGoal.LeftFoot, footPositionWeight);
			bipedIK.SetIKRotationWeight(AvatarIKGoal.LeftFoot, footRotationWeight);
			animator.SetIKPosition(AvatarIKGoal.LeftFoot, footTargetAnimator.position);
			animator.SetIKRotation(AvatarIKGoal.LeftFoot, footTargetAnimator.rotation);
			animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, footPositionWeight);
			animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, footRotationWeight);
			handTargetAnimator.position = handTargetBiped.position + vector;
			handTargetAnimator.rotation = handTargetBiped.rotation;
			bipedIK.SetIKPosition(AvatarIKGoal.LeftHand, handTargetBiped.position);
			bipedIK.SetIKRotation(AvatarIKGoal.LeftHand, handTargetBiped.rotation);
			bipedIK.SetIKPositionWeight(AvatarIKGoal.LeftHand, handPositionWeight);
			bipedIK.SetIKRotationWeight(AvatarIKGoal.LeftHand, handRotationWeight);
			animator.SetIKPosition(AvatarIKGoal.LeftHand, handTargetAnimator.position);
			animator.SetIKRotation(AvatarIKGoal.LeftHand, handTargetAnimator.rotation);
			animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, handPositionWeight);
			animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, handRotationWeight);
		}
	}
}
