using System;
using UnityEngine;

public class Animation_ReAnimationClipForAnimatorOver : MonoBehaviour
{
	[Serializable]
	public class animationClipRe
	{
		public string nameAnimationClip;

		public AnimationClip animationClip;
	}

	public animationClipRe[] reclips;

	[Header("OnStart")]
	public Animator animator;

	private AnimatorOverrideController animatorOverrideController;

	private void Start()
	{
		if (animator != null)
		{
			reAnimationClip(animator);
		}
	}

	public void reAnimationClip(Animator anim)
	{
		animatorOverrideController = new AnimatorOverrideController(anim.runtimeAnimatorController);
		anim.runtimeAnimatorController = animatorOverrideController;
		for (int i = 0; i < reclips.Length; i++)
		{
			animatorOverrideController[reclips[i].nameAnimationClip] = reclips[i].animationClip;
		}
	}
}
