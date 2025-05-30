using UnityEngine;
using UnityEngine.Events;

public class Item_Tool : MonoBehaviour
{
	public enum EnumToolHand
	{
		leftHand = 0,
		rightHand = 1
	}

	public bool interactiveActiveStart = true;

	public bool AbAction;

	public EnumToolHand inHands;

	public float distance = 1f;

	[Header("Position")]
	public bool freePosition;

	public Vector3 rotation;

	public Vector3 position;

	public bool rotationView;

	public float rotationForward;

	public UnityEvent eventTake;

	[Header("Hands wrist")]
	public Transform objectTakeRightHand;

	public Transform objectTakeLeftHand;

	private Interactive_Action myAction;

	private Player scrPlayer;

	private GameObject objInteract;

	private bool takeHand;

	private bool keyDown;

	private bool eventUse;

	private Transform headPlayer;

	private Transform bodyHeight;

	[HideInInspector]
	public UnityEvent eventNull;

	private void Start()
	{
		objInteract = Object.Instantiate(Resources.Load<GameObject>("Interface/Interactive"), base.transform.position, Quaternion.identity, base.transform);
		scrPlayer = GameObject.FindWithTag("Player").gameObject.GetComponent<Player>();
		if (rotationView)
		{
			headPlayer = GameObject.FindWithTag("Player").gameObject.transform.Find("Armature/Hips/Spine/Chest/Neck/Head");
		}
		myAction = objInteract.GetComponent<Interactive_Action>();
		myAction.active = interactiveActiveStart;
		myAction.BlackscreenAnimation = AbAction;
		if (inHands == EnumToolHand.rightHand)
		{
			myAction.onlyRightHandIK = true;
		}
		if (inHands == EnumToolHand.leftHand)
		{
			myAction.onlyLeftHandIK = true;
		}
		myAction.distancePlayer = distance;
		if (AbAction)
		{
			UnityEvent unityEvent = eventNull;
			unityEvent.AddListener(TakeTool);
			myAction._event = unityEvent;
		}
		if (freePosition)
		{
			Transform parent = base.transform.parent;
			if (inHands == EnumToolHand.rightHand)
			{
				objectTakeRightHand.SetParent(null);
				base.transform.SetParent(objectTakeRightHand);
			}
			if (inHands == EnumToolHand.leftHand)
			{
				objectTakeLeftHand.SetParent(null);
				base.transform.SetParent(objectTakeLeftHand);
			}
			rotation = base.transform.localRotation.eulerAngles;
			position = base.transform.localPosition;
			base.transform.SetParent(parent);
			if (inHands == EnumToolHand.rightHand)
			{
				objectTakeRightHand.SetParent(base.transform);
			}
			if (inHands == EnumToolHand.leftHand)
			{
				objectTakeLeftHand.SetParent(base.transform);
			}
		}
	}

	private void Update()
	{
		if (myAction.changePlayer && !scrPlayer.ikBringEndHand && !takeHand)
		{
			if (keyDown && !AbAction)
			{
				if (!myAction.handRight_handLeft)
				{
					scrPlayer.handTargetRight.position = Vector3.Lerp(scrPlayer.handTargetRight.position, base.transform.position, Time.deltaTime * 10f);
					scrPlayer.handRightIkTarget = 2;
					scrPlayer.rotationPlayerTo = 2;
					scrPlayer.rotationPlayerToAngle = Quaternion.LookRotation(base.transform.position - scrPlayer.gameObject.transform.position, Vector3.up).eulerAngles.y;
					if (scrPlayer.rightHandTakeIk)
					{
						TakeTool();
					}
				}
				if (myAction.handRight_handLeft)
				{
					scrPlayer.handTargetLeft.position = Vector3.Lerp(scrPlayer.handTargetLeft.position, base.transform.position, Time.deltaTime * 10f);
					scrPlayer.handLeftIkTarget = 2;
					scrPlayer.rotationPlayerTo = 2;
					scrPlayer.rotationPlayerToAngle = Quaternion.LookRotation(base.transform.position - scrPlayer.gameObject.transform.position, Vector3.up).eulerAngles.y;
					if (scrPlayer.leftHandTakeIk)
					{
						TakeTool();
					}
				}
			}
			if (Input.GetButtonDown("Action"))
			{
				keyDown = true;
			}
			if (!Input.GetButton("Action"))
			{
				keyDown = false;
			}
		}
		else
		{
			keyDown = false;
		}
		if (takeHand)
		{
			if (rotationView && !freePosition)
			{
				Quaternion quaternion = Quaternion.Euler(new Vector3(0f, headPlayer.localRotation.eulerAngles.y, 0f - headPlayer.localRotation.eulerAngles.x));
				base.transform.localRotation = Quaternion.Lerp(base.transform.localRotation, Quaternion.Euler(rotation) * quaternion, Time.deltaTime * 20f);
				Vector3 b = bodyHeight.position + (bodyHeight.right * position.x + bodyHeight.up * position.y + bodyHeight.forward * position.z) + headPlayer.forward * rotationForward;
				base.transform.position = Vector3.Lerp(base.transform.position, b, Time.deltaTime * 8f);
			}
			else
			{
				base.transform.localRotation = Quaternion.Lerp(base.transform.localRotation, Quaternion.Euler(rotation), Time.deltaTime * 8f);
				base.transform.localPosition = Vector3.Lerp(base.transform.localPosition, position, Time.deltaTime * 8f);
			}
		}
	}

	public void TakeTool()
	{
		takeHand = true;
		myAction.active = false;
		if (!myAction.handRight_handLeft)
		{
			scrPlayer.handTargetRight.position = base.transform.position;
			scrPlayer.handRightIkTarget = 2;
		}
		if (myAction.handRight_handLeft)
		{
			scrPlayer.handTargetLeft.position = base.transform.position;
			scrPlayer.handLeftIkTarget = 2;
		}
		bool right = false;
		bool left = false;
		if (objectTakeLeftHand != null)
		{
			left = true;
		}
		if (objectTakeRightHand != null)
		{
			right = true;
		}
		myAction.SharplyTakeHand(right, left);
		scrPlayer.ToolTakeObject(base.gameObject, freePosition);
		scrPlayer.StartInteractiveHands(objectTakeRightHand, objectTakeLeftHand, freePosition);
		bodyHeight = base.transform.parent;
		if (freePosition)
		{
			_ = Quaternion.identity;
			_ = Vector3.zero;
			if (inHands == EnumToolHand.rightHand)
			{
				base.transform.SetParent(GameObject.FindWithTag("Player").transform.Find("Armature/Hips/Spine/Chest/Right shoulder/Right arm/Right elbow/Right wrist").transform);
			}
			if (inHands == EnumToolHand.leftHand)
			{
				base.transform.SetParent(GameObject.FindWithTag("Player").transform.Find("Armature/Hips/Spine/Chest/Left shoulder/Left arm/Left elbow/Left wrist").transform);
			}
		}
		if (!eventUse)
		{
			eventUse = true;
			eventTake.Invoke();
		}
	}

	public void Activation(bool x)
	{
		myAction.active = x;
	}

	public void DestroyTool()
	{
		Object.Destroy(base.gameObject);
	}
}
