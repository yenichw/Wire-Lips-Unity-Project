using UnityEngine;
using UnityEngine.Events;

public class Interactive_Action : MonoBehaviour
{
	public enum EnumTypeSpriteInteractive
	{
		hand = 0,
		eye = 1
	}

	public EnumTypeSpriteInteractive spriteInteractive;

	public bool active = true;

	public UnityEvent _event;

	public UnityEvent _eventNoBsanim;

	public bool deactiveAfterEvent;

	public float distancePlayer = 1f;

	public bool rayCastCollider = true;

	public bool BlackscreenAnimation;

	[HideInInspector]
	public bool handRight_handLeft;

	[HideInInspector]
	public bool hold;

	[HideInInspector]
	public bool handIkEndPlayer;

	[HideInInspector]
	public bool changePlayer;

	[HideInInspector]
	public bool onlyRightHandIK;

	[HideInInspector]
	public bool onlyLeftHandIK;

	private Vector3 positionStart;

	private Item_ShowHand showItemScr;

	private Vector3 localPosition;

	private Transform headPlayer;

	private bool enExInter;

	private bool handsUseNow;

	private bool keyDown;

	private bool fs;

	private bool eventDone;

	private Player scrPlayer;

	private int casePlayerInteractive;

	private Transform Tplayer;

	private Interface_MainPlayer scrIntMain;

	private Interactive_ActionHand handsUse;

	private float timeStart;

	private float timeStartKey;

	private float timeHoldEvent;

	[Header("БУДЕТ СКРЫТО")]
	public UnityEvent eventHold;

	[HideInInspector]
	public UnityEvent eventNull;

	private void Start()
	{
		if (!fs)
		{
			fs = true;
			casePlayerInteractive = -1;
			scrIntMain = GameObject.FindWithTag("GameController").GetComponent<Interface_MainPlayer>();
			headPlayer = GameObject.FindWithTag("Head").gameObject.transform;
			scrPlayer = GameObject.FindWithTag("Player").gameObject.GetComponent<Player>();
			Tplayer = GameObject.FindWithTag("Player").transform;
			if (GetComponent<Item_ShowHand>() != null)
			{
				showItemScr = GetComponent<Item_ShowHand>();
			}
			if (GetComponent<Interactive_ActionHand>() != null)
			{
				handsUse = GetComponent<Interactive_ActionHand>();
			}
			positionStart = base.transform.position;
		}
	}

	private void Update()
	{
		if (timeStart > 0f)
		{
			timeStart -= 1f;
			if (timeStart < 0f)
			{
				timeStart = 0f;
			}
		}
		if (timeStartKey > 0f)
		{
			timeStartKey -= Time.deltaTime;
			if (timeStartKey < 0f)
			{
				timeStartKey = 0f;
			}
		}
		if (active && !scrIntMain.inventory && !scrPlayer.busy && timeStart == 0f)
		{
			Vector3 vector = Tplayer.position - base.transform.position;
			Quaternion b = Quaternion.Euler(0f, Quaternion.LookRotation(vector, Vector3.up).eulerAngles.y, 0f);
			float num = Quaternion.Angle(Tplayer.rotation, b);
			if (!onlyRightHandIK && !onlyLeftHandIK)
			{
				if (Vector3.Dot(Tplayer.right, vector) > 0f)
				{
					handRight_handLeft = true;
				}
				else
				{
					handRight_handLeft = false;
				}
			}
			else
			{
				if (onlyRightHandIK)
				{
					handRight_handLeft = false;
				}
				if (onlyLeftHandIK)
				{
					handRight_handLeft = true;
				}
			}
			if (Vector3.Distance(new Vector3(base.transform.position.x, 0f, base.transform.position.z), new Vector3(headPlayer.position.x, 0f, headPlayer.position.z)) < distancePlayer)
			{
				if (num > 80f)
				{
					if (rayCastCollider)
					{
						bool flag = false;
						if (Physics.Linecast(Tplayer.position + new Vector3(0f, 1.5f, 0f), base.transform.position + Vector3.Normalize(-(base.transform.position - (Tplayer.position + new Vector3(0f, 1.5f, 0f)))) / 10f, out var hitInfo) && hitInfo.collider.gameObject.layer != 10)
						{
							flag = true;
						}
						if (!flag)
						{
							if (!enExInter)
							{
								enExInter = true;
								EnterInteractive();
							}
						}
						else if (enExInter)
						{
							enExInter = false;
							ExitInteractive();
						}
					}
					else if (!enExInter)
					{
						enExInter = true;
						EnterInteractive();
					}
				}
				else if (enExInter)
				{
					enExInter = false;
					ExitInteractive();
				}
			}
			else if (enExInter)
			{
				enExInter = false;
				ExitInteractive();
			}
			if (enExInter)
			{
				if (scrPlayer.interactiveObjectNow == base.gameObject)
				{
					changePlayer = true;
					if (Input.GetButtonDown("Action") && timeStartKey == 0f)
					{
						if (handsUse == null)
						{
							ConsoleMain.ConsolePrint("Interactive:" + base.gameObject.name);
							if (!BlackscreenAnimation)
							{
								_eventNoBsanim.Invoke();
								Action();
							}
							else
							{
								_eventNoBsanim.Invoke();
								UnityEvent unityEvent = eventNull;
								unityEvent.AddListener(Action);
								scrPlayer.BSAnim(unityEvent);
							}
						}
						else if (showItemScr != null || (scrPlayer.rightHandUseTool && scrPlayer.leftHandUseTool))
						{
							_eventNoBsanim.Invoke();
							UnityEvent unityEvent2 = eventNull;
							unityEvent2.AddListener(Action);
							scrPlayer.BSAnim(unityEvent2);
						}
						else
						{
							handsUseNow = true;
							handsUse.Pickup();
							if (!handsUse.handRightUse && !handsUse.handLeftUse)
							{
								_eventNoBsanim.Invoke();
								UnityEvent unityEvent3 = eventNull;
								unityEvent3.AddListener(Action);
								scrPlayer.BSAnim(unityEvent3);
							}
						}
						keyDown = true;
					}
					if (handsUse != null && keyDown && !scrPlayer.rightHandUseTool && !scrPlayer.leftHandUseTool && showItemScr == null)
					{
						if (Input.GetButton("Action") && timeStartKey == 0f)
						{
							handsUseNow = true;
						}
						else
						{
							handsUseNow = false;
						}
					}
					if (Input.GetButtonUp("Action"))
					{
						eventDone = false;
						keyDown = false;
					}
				}
				else
				{
					timeStartKey = 0.1f;
				}
			}
		}
		if (enExInter && (!active || scrPlayer.busy || timeStart > 0f))
		{
			enExInter = false;
			ExitInteractive();
		}
		if (!enExInter || scrPlayer.interactiveObjectNow != base.gameObject || !active || scrIntMain.inventory || timeStart > 0f)
		{
			changePlayer = false;
		}
		if (scrPlayer.busy)
		{
			timeStart = 5f;
		}
		if (handsUseNow && keyDown && showItemScr == null && !BlackscreenAnimation)
		{
			if (handsUse.handRightUse && !scrPlayer.rightHandUseTool)
			{
				scrPlayer.handTargetRight.position = handsUse.handWristRight.position;
				scrPlayer.handRightIkTarget = 2;
				scrPlayer.rotationPlayerTo = 2;
				scrPlayer.rotationPlayerToAngle = Quaternion.LookRotation(positionStart - scrPlayer.gameObject.transform.position, Vector3.up).eulerAngles.y;
				if (scrPlayer.rightHandTakeIk)
				{
					handIkEndPlayer = true;
					if (!eventDone)
					{
						eventDone = true;
						handsUseNow = false;
						_eventNoBsanim.Invoke();
						Action();
						ConsoleMain.ConsolePrint("Interactive handUse R:" + base.gameObject.name);
						if (showItemScr != null)
						{
							hold = true;
							showItemScr.Show(x: true);
						}
					}
				}
				else
				{
					handIkEndPlayer = false;
				}
			}
			if (handsUse.handLeftUse && !scrPlayer.leftHandUseTool)
			{
				scrPlayer.handTargetLeft.position = handsUse.handWristLeft.position;
				scrPlayer.handLeftIkTarget = 2;
				scrPlayer.rotationPlayerTo = 2;
				scrPlayer.rotationPlayerToAngle = Quaternion.LookRotation(positionStart - scrPlayer.gameObject.transform.position, Vector3.up).eulerAngles.y;
				if (scrPlayer.leftHandTakeIk)
				{
					handIkEndPlayer = true;
					if (!eventDone)
					{
						eventDone = true;
						handsUseNow = false;
						ConsoleMain.ConsolePrint("Interactive handUse L:" + base.gameObject.name);
						if (GetComponent<Item_ShowHand>() != null)
						{
							hold = true;
							showItemScr.Show(x: true);
						}
						_eventNoBsanim.Invoke();
						Action();
					}
				}
				else
				{
					handIkEndPlayer = false;
				}
			}
			if (handIkEndPlayer && timeHoldEvent > 0f)
			{
				timeHoldEvent -= Time.deltaTime;
				if (timeHoldEvent <= 0f)
				{
					timeHoldEvent = 0f;
					handsUseNow = false;
					hold = false;
					keyDown = false;
					eventHold.Invoke();
				}
			}
		}
		if (hold)
		{
			if (handsUse.handRightUse)
			{
				scrPlayer.handTargetRight.position = handsUse.handWristRight.position;
				scrPlayer.handRightIkTarget = 2;
			}
			if (handsUse.handLeftUse)
			{
				scrPlayer.handTargetLeft.position = handsUse.handWristLeft.position;
				scrPlayer.handLeftIkTarget = 2;
			}
		}
	}

	public void FastHold(bool x)
	{
		hold = x;
		if (!fs)
		{
			Start();
		}
		if (!x)
		{
			return;
		}
		ConsoleMain.ConsolePrint("FastHold | true | ");
		if (handsUse != null)
		{
			ConsoleMain.ConsolePrintAdd("handsUse = true | ");
			if (handsUse.handRightUse)
			{
				ConsoleMain.ConsolePrintAdd("handRight = true | ");
			}
			if (handsUse.handLeftUse)
			{
				ConsoleMain.ConsolePrintAdd("handLeft = true");
			}
			handsUseNow = true;
			handsUse.Pickup();
			if (handsUse.handRightUse)
			{
				scrPlayer.handTargetRight.position = handsUse.handWristRight.position;
				scrPlayer.handRightIkTarget = 2;
				scrPlayer.poserHandRight.weight = 1f;
				scrPlayer.ikBody.solver.rightHandEffector.positionWeight = 1f;
				scrPlayer.ikBody.solver.rightHandEffector.rotationWeight = 1f;
			}
			if (handsUse.handLeftUse)
			{
				scrPlayer.handTargetLeft.position = handsUse.handWristLeft.position;
				scrPlayer.handLeftIkTarget = 2;
				scrPlayer.poserHandLeft.weight = 1f;
				scrPlayer.ikBody.solver.leftHandEffector.positionWeight = 1f;
				scrPlayer.ikBody.solver.leftHandEffector.rotationWeight = 1f;
			}
		}
	}

	private void EnterInteractive()
	{
		for (int i = 0; i < 20; i++)
		{
			if (casePlayerInteractive == -1 && scrPlayer.interactiveObjectsNow[i] == null)
			{
				scrPlayer.interactiveObjectsNow[i] = base.gameObject;
				casePlayerInteractive = i;
				scrPlayer.InteractiveUpdate();
				timeStartKey = 0.1f;
				keyDown = false;
			}
		}
	}

	private void ExitInteractive()
	{
		timeStart = 5f;
		keyDown = false;
		scrPlayer.interactiveObjectsNow[casePlayerInteractive] = null;
		casePlayerInteractive = -1;
		scrPlayer.InteractiveUpdate();
	}

	private void OnDestroy()
	{
		if (scrPlayer != null && scrPlayer.interactiveObjectNow == base.gameObject)
		{
			scrPlayer.interactiveObjectNow = null;
			scrPlayer.interactiveObjectsNow[casePlayerInteractive] = null;
			scrPlayer.InteractiveUpdate();
		}
	}

	public void Activation(bool x)
	{
		eventDone = false;
		keyDown = false;
		active = x;
		timeStart = 5f;
		if (!x)
		{
			hold = false;
			if (enExInter)
			{
				enExInter = false;
				ExitInteractive();
			}
		}
	}

	public void EventHold(float _time, UnityEvent _eventHold)
	{
		timeHoldEvent = _time;
		eventHold = _eventHold;
	}

	[ContextMenu("Fix Events")]
	private void FixEvents()
	{
		_event = eventHold;
		UnityEvent unityEvent = null;
		eventHold = unityEvent;
	}

	private void Action()
	{
		_event.Invoke();
		if (deactiveAfterEvent)
		{
			Activation(x: false);
		}
	}

	private void OnDrawGizmosSelected()
	{
		if (Tplayer == null)
		{
			if (GameObject.FindWithTag("Player") != null)
			{
				Tplayer = GameObject.FindWithTag("Player").transform;
			}
			return;
		}
		if (!Physics.Linecast(Tplayer.position + new Vector3(0f, 1.5f, 0f), base.transform.position + Vector3.Normalize(-(base.transform.position - (Tplayer.position + new Vector3(0f, 1.5f, 0f)))) / 10f))
		{
			Gizmos.color = new Color(1f, 1f, 1f, 1f);
		}
		else
		{
			Gizmos.color = new Color(1f, 0f, 0f, 1f);
		}
		Gizmos.DrawLine(Tplayer.position + new Vector3(0f, 1.5f, 0f), base.transform.position + Vector3.Normalize(-(base.transform.position - (Tplayer.position + new Vector3(0f, 1.5f, 0f)))) / 10f);
	}

	public void SharplyTakeHand(bool _right, bool _left)
	{
		if (_right)
		{
			scrPlayer.handRightIkTarget = 2;
		}
		if (_left)
		{
			scrPlayer.handLeftIkTarget = 2;
		}
	}
}
