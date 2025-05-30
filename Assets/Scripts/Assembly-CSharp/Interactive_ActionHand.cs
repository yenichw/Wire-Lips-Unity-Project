using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Interactive_Action))]
public class Interactive_ActionHand : MonoBehaviour
{
	public float timeHoldEvent;

	public UnityEvent _event;

	public bool handRight;

	public Transform handWristRight;

	public bool handLeft;

	public Transform handWristLeft;

	[HideInInspector]
	public bool handRightUse;

	[HideInInspector]
	public bool handLeftUse;

	private void Start()
	{
		if (timeHoldEvent != 0f)
		{
			GetComponent<Interactive_Action>().EventHold(timeHoldEvent, _event);
		}
	}

	public void Pickup()
	{
		Player component = GameObject.FindWithTag("Player").GetComponent<Player>();
		Transform transform = null;
		Transform transform2 = null;
		if (!component.rightHandUseTool && handWristRight != null && handRight)
		{
			transform2 = handWristRight;
		}
		if (handRight && transform2 == null)
		{
			transform = handWristLeft;
		}
		if (!component.leftHandUseTool && handWristLeft != null && handLeft)
		{
			transform = handWristLeft;
		}
		if (handLeft && transform == null)
		{
			transform2 = handWristRight;
		}
		if (transform2 != null)
		{
			handRightUse = true;
		}
		if (transform != null)
		{
			handLeftUse = true;
		}
		GameObject.FindWithTag("GameController").gameObject.transform.Find("PlayerController/Hands Pickup").GetComponent<Player_HandsPickup>().HandsTarget(transform2, transform, _freePosition: false);
	}
}
