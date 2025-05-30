using UnityEngine;
using UnityEngine.Events;

public class Animation_EventTime : MonoBehaviour
{
	public bool startActive = true;

	public AnimationClip animationClip;

	public UnityEvent eventEndAniamtion;

	private float timeAnimation;

	private bool eventReady;

	private bool active;

	private void Start()
	{
		if (startActive)
		{
			active = true;
		}
	}

	private void Update()
	{
		if (active)
		{
			timeAnimation += Time.deltaTime;
			if (timeAnimation >= animationClip.length && !eventReady)
			{
				eventEndAniamtion.Invoke();
				eventReady = true;
			}
		}
	}

	public void StartAnimationAnimation(AnimationClip _animClip)
	{
		timeAnimation = 0f;
		animationClip = _animClip;
		active = true;
	}

	public void StartAnimation()
	{
		timeAnimation = 0f;
		active = true;
	}
}
