using UnityEngine;
using UnityEngine.Events;

public class Interactive_PhysicThingOpen : MonoBehaviour
{
	public Interactive_Action myAction;

	public UnityEvent _eventOpen;

	public UnityEvent _eventClose;

	public Interactive_PhysicThingOpen[] anotherOnNull;

	public Transform playerLook;

	public float timeStop = 3f;

	[Header("Animator")]
	public string stateAnimation = "Animation";

	public float speedClose = 0.05f;

	[Header("Hands")]
	public bool anyHand = true;

	public bool interactionHands = true;

	public Transform objectTakeRightHand;

	public Transform objectTakeLeftHand;

	private Animator anim;

	private Player scrPlayer;

	private Interface_MainPlayer scrIntMain;

	private bool startOpen;

	[Header("Info")]
	public float timeAnimationState;

	public float timeOff;

	public bool close;

	public bool opened;

	public bool buttonDown;

	private void Start()
	{
		scrIntMain = GameObject.FindWithTag("GameController").GetComponent<Interface_MainPlayer>();
		scrPlayer = GameObject.FindWithTag("Player").gameObject.GetComponent<Player>();
		anim = GetComponent<Animator>();
		anim.speed = 0f;
		close = true;
	}

	private void Update()
	{
		if (((Input.GetButton("Action") && myAction.changePlayer) || opened) && anotherOnNull.Length != 0)
		{
			for (int i = 0; i < anotherOnNull.Length; i++)
			{
				anotherOnNull[i].timeOff = 0f;
				anotherOnNull[i].close = true;
				if (anotherOnNull[i].timeAnimationState > 0f)
				{
					anotherOnNull[i].timeAnimationState -= Time.deltaTime;
				}
			}
		}
		if (interactionHands)
		{
			if (Input.GetButtonDown("Action") && myAction.changePlayer)
			{
				close = false;
				buttonDown = true;
				scrPlayer.StartInteractiveHands(objectTakeRightHand, objectTakeLeftHand, _free: false);
				if (!startOpen)
				{
					if (playerLook != null)
					{
						scrPlayer.HeadTargetPlayer(playerLook);
					}
					startOpen = true;
				}
			}
			if (buttonDown)
			{
				if (scrPlayer.progressInteractionHand >= 0.49f)
				{
					timeOff = timeStop;
					if (timeAnimationState < 1f)
					{
						timeAnimationState += Time.deltaTime;
						if (timeAnimationState >= 1f)
						{
							OpenCase();
						}
					}
					anim.Play(stateAnimation, -1, timeAnimationState);
				}
				if (Input.GetButtonUp("Action") || !myAction.changePlayer)
				{
					close = true;
					opened = false;
					timeOff = timeStop;
					buttonDown = false;
					scrPlayer.StopInteractiveHands();
				}
			}
		}
		if (!interactionHands && Input.GetButton("Action") && myAction.changePlayer)
		{
			close = false;
			if (!startOpen)
			{
				if (playerLook != null)
				{
					scrPlayer.HeadTargetPlayer(playerLook);
				}
				startOpen = true;
			}
			if (anyHand)
			{
				if (!myAction.handRight_handLeft)
				{
					scrPlayer.handTargetRight.position = Vector3.Lerp(scrPlayer.handTargetRight.position, objectTakeRightHand.position, Time.deltaTime * 10f);
					scrPlayer.handRightIkTarget = 2;
					if (scrPlayer.rightHandTakeIk)
					{
						timeOff = timeStop;
						if (timeAnimationState < 1f)
						{
							timeAnimationState += Time.deltaTime;
							if (timeAnimationState >= 1f)
							{
								OpenCase();
							}
						}
						anim.Play(stateAnimation, -1, timeAnimationState);
					}
					else if (timeOff == 0f && timeAnimationState > 0f)
					{
						timeAnimationState -= Time.deltaTime * speedClose;
						anim.Play(stateAnimation, -1, timeAnimationState);
					}
				}
				if (myAction.handRight_handLeft)
				{
					scrPlayer.handTargetLeft.position = Vector3.Lerp(scrPlayer.handTargetLeft.position, objectTakeRightHand.position, Time.deltaTime * 10f);
					scrPlayer.handLeftIkTarget = 2;
					if (scrPlayer.leftHandTakeIk)
					{
						timeOff = timeStop;
						if (timeAnimationState < 1f)
						{
							timeAnimationState += Time.deltaTime;
							if (timeAnimationState >= 1f)
							{
								OpenCase();
							}
						}
						anim.Play(stateAnimation, -1, timeAnimationState);
					}
					else if (timeOff == 0f && timeAnimationState > 0f)
					{
						timeAnimationState -= Time.deltaTime * speedClose;
						anim.Play(stateAnimation, -1, timeAnimationState);
					}
				}
			}
			else
			{
				if (objectTakeRightHand != null)
				{
					scrPlayer.handTargetRight.position = Vector3.Lerp(scrPlayer.handTargetRight.position, objectTakeRightHand.position, Time.deltaTime * 10f);
					scrPlayer.handRightIkTarget = 2;
					if (scrPlayer.rightHandTakeIk)
					{
						timeOff = timeStop;
						if (timeAnimationState < 1f)
						{
							timeAnimationState += Time.deltaTime;
							if (timeAnimationState >= 1f)
							{
								OpenCase();
							}
						}
						anim.Play(stateAnimation, -1, timeAnimationState);
					}
					else if (timeOff == 0f && timeAnimationState > 0f)
					{
						timeAnimationState -= Time.deltaTime * speedClose;
						anim.Play(stateAnimation, -1, timeAnimationState);
					}
				}
				if (objectTakeLeftHand != null)
				{
					scrPlayer.handTargetRight.position = Vector3.Lerp(scrPlayer.handTargetRight.position, objectTakeLeftHand.position, Time.deltaTime * 10f);
					scrPlayer.handRightIkTarget = 2;
					if (scrPlayer.leftHandTakeIk)
					{
						timeOff = timeStop;
						if (timeAnimationState < 1f)
						{
							timeAnimationState += Time.deltaTime;
							if (timeAnimationState >= 1f)
							{
								OpenCase();
							}
						}
						anim.Play(stateAnimation, -1, timeAnimationState);
					}
					else if (timeOff == 0f && timeAnimationState > 0f)
					{
						timeAnimationState -= Time.deltaTime * speedClose;
						anim.Play(stateAnimation, -1, timeAnimationState);
					}
				}
			}
		}
		if (!Input.GetButton("Action") || !myAction.changePlayer)
		{
			if (startOpen)
			{
				startOpen = false;
				scrPlayer.HeadTargetPlayer(null);
			}
			if (scrIntMain.objCaseInventory == null && opened)
			{
				timeOff = 0f;
				close = true;
				opened = false;
			}
			if (scrIntMain.objCaseInventory == null)
			{
				myAction.active = true;
			}
			if (scrIntMain.objCaseInventory != null)
			{
				timeOff = timeStop;
			}
			if (timeOff > 0f)
			{
				timeOff -= Time.deltaTime;
				if (timeOff <= 0f)
				{
					close = true;
					opened = false;
				}
			}
		}
		if (close && timeAnimationState > 0f)
		{
			timeOff = 0f;
			timeAnimationState -= Time.deltaTime * speedClose;
			anim.Play(stateAnimation, -1, timeAnimationState);
			if (timeAnimationState <= 0f)
			{
				CloseCase();
			}
		}
	}

	public void OpenCase()
	{
		myAction.active = false;
		_eventOpen.Invoke();
		opened = true;
	}

	public void CloseCase()
	{
		_eventClose.Invoke();
	}
}
