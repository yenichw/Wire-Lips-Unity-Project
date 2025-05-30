using RootMotion.FinalIK;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
	public float speedMove = 1f;

	public LayerMask layerNoWall;

	public LayerMask layerCameraWall;

	[Space(8f)]
	public Sprite spriteInteractiveHand;

	public Sprite spriteInteractiveEye;

	[Space(8f)]
	public RuntimeAnimatorController animatorController;

	public Transform cameraPlayer;

	public Transform pointViewPlayer;

	public Transform pointTargetHeadPlayer;

	public Transform pointTargetEyesPlayer;

	public Transform handTargetLeft;

	public Transform handTargetRight;

	public Transform handRightGoal;

	public Transform handLeftGoal;

	public float distanceCamera;

	public Player_HandsPickup handsPickup;

	public InteractionTarget rightHandEffector;

	public InteractionTarget leftHandEffector;

	public GameObject rightHandWrist;

	public GameObject leftHandWrist;

	public Transform bodyHeight;

	public float fear;

	public Transform interactiveTarget;

	public HandPoser poserHandRight;

	public HandPoser poserHandLeft;

	public GameObject creac;

	[SerializeField]
	private InteractionObject interactionObject;

	[SerializeField]
	private FullBodyBipedEffector[] effectors;

	[Header("Breath")]
	public AudioSource audioBreath;

	[Header("Information")]
	public GameObject interactiveObjectNow;

	[HideInInspector]
	public GameObject toolItem;

	[HideInInspector]
	public GameObject[] interactiveObjectsNow = new GameObject[20];

	[HideInInspector]
	public int handRightIkTarget;

	[HideInInspector]
	public int handLeftIkTarget;

	[HideInInspector]
	public int rotationPlayerTo;

	[HideInInspector]
	public bool rightHandTakeIk;

	[HideInInspector]
	public bool leftHandTakeIk;

	[HideInInspector]
	public bool ikBringEndHand;

	[HideInInspector]
	public bool dontMove;

	[HideInInspector]
	public bool busy;

	[HideInInspector]
	public float progressInteractionHand;

	[HideInInspector]
	public float rotationPlayerToAngle;

	[HideInInspector]
	public FullBodyBipedIK ikBody;

	[HideInInspector]
	public UnityEvent _eventBsAnim;

	private int indexPupilsBS;

	private int indexBlinkBS;

	private Vector3 fearLerp;

	private Vector3 fearLerpDot;

	private Transform head;

	private Transform headTarget;

	private Transform itemShow;

	private Vector3 viewPoint;

	private Vector3 viewPointMouse;

	private Vector3 positionPlayerNeed;

	private Vector2 move;

	private Vector2 moveAnim;

	private float blink;

	private float tmEyeMove;

	private float tmblink;

	private float spdHandsTime;

	private float rotationPlayerNeed;

	private float timeAnimationPlayOther;

	private float spdHead;

	private float tmsetblsh;

	private int avatarOtherPlayer;

	private int iItemChangeNow;

	private Vector3 eyesRotation;

	private LookAtIK ikLook;

	private SkinnedMeshRenderer bodyMesh;

	private Rigidbody playerRigidbody;

	private Animator animPerson;

	private bool animationRun;

	private bool showItemHand;

	private bool fs;

	private bool rotationPlayerNeedActive;

	private bool positionPlayerNeedActive;

	private bool animationPerson;

	private bool ikanimationPerson;

	private Audio_StepFoot stepFoot;

	private Camera mainCam;

	private Camera interfaceCam;

	private Image imgTarget;

	private AnimationClip animationClipNeed;

	private AnimatorOverrideController animatorOverrideController;

	private TimePointBool[] eventsTimeAnimation;

	private float timeEventsAnimation;

	private bool creacSkip;

	private float sensitivityMouse = 0.5f;

	private float noiseCamera;

	private float intensityFOV;

	private float timeTargetView;

	private float spdLerpFOV;

	private Transform targetView;

	private bool cameraView;

	private bool rightHandToolFreePosition;

	private bool leftHandToolFreePosition;

	[HideInInspector]
	public bool rightHandUseTool;

	[HideInInspector]
	public bool leftHandUseTool;

	private Image bsAnimation;

	private bool bsAnimActive;

	[HideInInspector]
	public float bsAnimAlpha;

	private void Awake()
	{
		head = base.transform.Find("Armature/Hips/Spine/Chest/Neck/Head").transform;
		head.tag = "Head";
		creacSkip = true;
	}

	private void Start()
	{
		if (!fs)
		{
			fs = true;
			animPerson = GetComponent<Animator>();
			animatorOverrideController = new AnimatorOverrideController(animPerson.runtimeAnimatorController);
			animPerson.runtimeAnimatorController = animatorOverrideController;
			animatorController = animPerson.runtimeAnimatorController;
			ikLook = GetComponent<LookAtIK>();
			playerRigidbody = GetComponent<Rigidbody>();
			handTargetLeft.parent = base.transform;
			handTargetRight.parent = base.transform;
			handRightGoal.parent = base.transform;
			handLeftGoal.parent = base.transform;
			bodyHeight.parent = base.transform;
			bodyMesh = base.transform.Find("Body").GetComponent<SkinnedMeshRenderer>();
			imgTarget = interactiveTarget.Find("Image").GetComponent<Image>();
			bodyHeight.localPosition = new Vector3(0f, 1f, 0f);
			mainCam = cameraPlayer.gameObject.GetComponent<Camera>();
			interfaceCam = cameraPlayer.transform.Find("CameraUI").gameObject.GetComponent<Camera>();
			indexPupilsBS = bodyMesh.sharedMesh.GetBlendShapeIndex("Pupils Little");
			indexBlinkBS = bodyMesh.sharedMesh.GetBlendShapeIndex("Eyes Blink");
			stepFoot = GetComponent<Audio_StepFoot>();
			base.transform.position = new Vector3(base.transform.position.x, 0f, base.transform.position.z);
			ikBody = GetComponent<FullBodyBipedIK>();
			ikBody.solver.leftHandEffector.positionWeight = 0.2f;
			ikBody.solver.rightHandEffector.positionWeight = 0.2f;
			interactiveObjectsNow = new GameObject[20];
			tmEyeMove = 5000f;
			eyesRotation = new Vector3(0f, 2.5f, 5f);
			bodyMesh.SetBlendShapeWeight(bodyMesh.sharedMesh.GetBlendShapeIndex("Brown Stressed"), 80f);
			bodyMesh.SetBlendShapeWeight(bodyMesh.sharedMesh.GetBlendShapeIndex("Brown Down"), 20f);
			bodyMesh.SetBlendShapeWeight(bodyMesh.sharedMesh.GetBlendShapeIndex("Eyes Stare"), 10f);
			bodyMesh.SetBlendShapeWeight(bodyMesh.sharedMesh.GetBlendShapeIndex("Mouth Hmm"), 70f);
			bsAnimation = GameObject.FindWithTag("GameController").transform.Find("Interface/BlackScreen Animation").gameObject.GetComponent<Image>();
			UpdateSettings();
		}
	}

	private void Update()
	{
		if (tmsetblsh > 0f)
		{
			tmsetblsh -= Time.deltaTime;
			if (tmsetblsh < 0f)
			{
				tmsetblsh = 0f;
				BlendShapeSetStress();
			}
		}
		if (base.transform.position.y < 0f && !animationRun)
		{
			base.transform.position = new Vector3(base.transform.position.x, 0f, base.transform.position.z);
		}
		if (fear > 0f)
		{
			bodyMesh.SetBlendShapeWeight(indexPupilsBS, fear * 100f);
			fear -= Time.deltaTime * 0.005f;
			if (fear < 0f)
			{
				fear = 0f;
			}
			fearLerpDot = new Vector3(Random.Range(0f - fear, fear), Random.Range(0f - fear, fear), Random.Range(0f - fear, fear)) / 8f;
			fearLerp = Vector3.Lerp(fearLerp, fearLerpDot, Time.deltaTime);
		}
		else
		{
			fearLerp = Vector3.Lerp(fearLerp, Vector3.zero, Time.deltaTime * 0.1f);
		}
		if (interactiveObjectNow != null)
		{
			interactiveTarget.position = interactiveObjectNow.transform.position;
			interactiveTarget.rotation = Quaternion.LookRotation(-(cameraPlayer.position - interactiveTarget.transform.position), Vector3.up);
			if (interactiveObjectNow.GetComponent<Interactive_Action>().spriteInteractive == Interactive_Action.EnumTypeSpriteInteractive.hand)
			{
				imgTarget.sprite = spriteInteractiveHand;
			}
			else
			{
				imgTarget.sprite = spriteInteractiveEye;
			}
		}
		else
		{
			interactiveTarget.gameObject.SetActive(value: false);
		}
		bool flag = false;
		if (Physics.CapsuleCast(base.transform.position + new Vector3(0f, 0.15f, 0f), base.transform.position + new Vector3(0f, 1.65f, 0f), 0.1f, base.transform.right * Input.GetAxis("Horizontal") + base.transform.forward * Input.GetAxis("Vertical"), 0.2f, layerNoWall))
		{
			flag = true;
		}
		if (targetView != null || dontMove || animationRun || handRightIkTarget > 0 || handLeftIkTarget > 0)
		{
			flag = true;
		}
		if (!flag)
		{
			move = Vector2.Lerp(move, new Vector2(Input.GetAxis("Horizontal") / (1f + Mathf.Abs(Input.GetAxis("Vertical"))), Input.GetAxis("Vertical") / (1f + Mathf.Abs(Input.GetAxis("Horizontal")))), Time.deltaTime * 10f);
			moveAnim = Vector2.Lerp(moveAnim, new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")), Time.deltaTime * 10f);
			playerRigidbody.velocity = Vector3.Lerp(playerRigidbody.velocity, (base.transform.right * (move.x * 2f) + base.transform.forward * (move.y * 2f)) * speedMove, Time.deltaTime * 10f);
			if (Input.GetAxis("Horizontal") != 0f || Input.GetAxis("Vertical") != 0f)
			{
				stepFoot.active = true;
			}
			else
			{
				stepFoot.active = false;
			}
		}
		if (flag)
		{
			playerRigidbody.velocity = Vector3.zero;
			stepFoot.active = false;
			move = Vector2.Lerp(move, Vector2.zero, Time.deltaTime * 5f);
			moveAnim = Vector2.Lerp(move, Vector2.zero, Time.deltaTime * 5f);
		}
		animPerson.SetFloat("Forward", moveAnim.y);
		animPerson.SetFloat("Right", moveAnim.x);
		tmEyeMove += Time.deltaTime * (1f + fear);
		if (tmEyeMove >= 1f)
		{
			pointTargetEyesPlayer.gameObject.GetComponent<LookAtIK>().solver.eyes[0].axis = new Vector3(0f, 1f, 0f);
			pointTargetEyesPlayer.gameObject.GetComponent<LookAtIK>().solver.eyes[1].axis = new Vector3(0f, 1f, 0f);
			int num = Random.Range(0, 101);
			if (num <= 15)
			{
				eyesRotation = new Vector3(Random.Range(-1.25f, 1.25f), Random.Range(-1.2f, 1f), Random.Range(1.5f, 7f));
			}
			if (num >= 70)
			{
				eyesRotation = new Vector3(0f, 0f, 5f);
			}
			tmEyeMove = 0f;
		}
		pointTargetEyesPlayer.position = Vector3.Lerp(pointTargetEyesPlayer.position, head.transform.position + head.transform.right * eyesRotation.x + head.transform.up * eyesRotation.y + head.transform.forward * eyesRotation.z, Time.deltaTime * 25f);
		bodyMesh.SetBlendShapeWeight(indexBlinkBS, blink);
		tmblink += Time.deltaTime * (1f + Random.Range(0f, fear) * 7f);
		if (tmblink > 1f)
		{
			blink += Time.deltaTime * 1250f;
			if (blink > 100f)
			{
				tmblink = Random.Range(-6, 0);
				eyesRotation = new Vector3(Random.Range(-1f, 1f), Random.Range(-1.2f, 1f), Random.Range(1.5f, 4f));
			}
		}
		if (tmblink < 1f && blink > 0f)
		{
			blink = Mathf.Lerp(blink, 0f, Time.deltaTime * 15f);
		}
		handRightGoal.position = base.transform.position + base.transform.forward * -0.5f + base.transform.right * 0.1f + base.transform.up * 1f;
		handLeftGoal.position = base.transform.position + base.transform.forward * -0.5f + base.transform.right * -0.1f + base.transform.up * 1f;
		if (spdHandsTime < 2f)
		{
			spdHandsTime += Time.deltaTime * 10f;
			if (spdHandsTime > 2f)
			{
				spdHandsTime = 2f;
			}
		}
		if (ikBringEndHand)
		{
			if ((double)ikBody.solver.rightHandEffector.positionWeight > 0.2)
			{
				ikBody.solver.rightHandEffector.positionWeight -= Time.deltaTime * spdHandsTime;
			}
			if ((double)ikBody.solver.leftHandEffector.positionWeight > 0.2)
			{
				ikBody.solver.leftHandEffector.positionWeight -= Time.deltaTime * spdHandsTime;
			}
			if ((double)ikBody.solver.rightHandEffector.positionWeight <= 0.2 && (double)ikBody.solver.leftHandEffector.positionWeight <= 0.2)
			{
				ikBringEndHand = false;
			}
		}
		if (!ikBringEndHand && !rightHandUseTool)
		{
			if (handRightIkTarget > 0)
			{
				spdHead = 1f;
				poserHandRight.weight = Mathf.Lerp(poserHandRight.weight, 1f, Time.deltaTime * 10f);
				pointTargetHeadPlayer.position = Vector3.Lerp(pointTargetHeadPlayer.position, interactiveTarget.position, Time.deltaTime * 8f);
				handRightIkTarget--;
				if (handRightIkTarget == 0)
				{
					spdHandsTime = 0f;
					StopInteractiveHand(_right: true, _left: false);
				}
				if (ikBody.solver.rightHandEffector.positionWeight < 1f)
				{
					ikBody.solver.rightHandEffector.positionWeight += Time.deltaTime * 2f;
				}
				if (ikBody.solver.rightHandEffector.rotationWeight < 1f)
				{
					ikBody.solver.rightHandEffector.rotationWeight += Time.deltaTime * 4f;
				}
				if (ikBody.solver.rightHandEffector.positionWeight >= 1f)
				{
					rightHandTakeIk = true;
				}
			}
			if (handRightIkTarget == 0)
			{
				poserHandRight.weight = Mathf.Lerp(poserHandRight.weight, 0f, Time.deltaTime * 10f);
				rightHandTakeIk = false;
				if (ikBody.solver.rightHandEffector.positionWeight > 0.2f)
				{
					ikBody.solver.rightHandEffector.positionWeight -= Time.deltaTime * spdHandsTime;
					if (ikBody.solver.rightHandEffector.positionWeight < 0.2f)
					{
						ikBody.solver.rightHandEffector.positionWeight = 0.2f;
					}
				}
				if (ikBody.solver.rightHandEffector.rotationWeight > 0f)
				{
					ikBody.solver.rightHandEffector.rotationWeight -= Time.deltaTime * spdHandsTime;
					if (ikBody.solver.rightHandEffector.rotationWeight < 0f)
					{
						ikBody.solver.rightHandEffector.rotationWeight = 0f;
					}
				}
				handTargetRight.position = Vector3.Lerp(handTargetRight.position, base.transform.position + base.transform.right * (0.25f + fear / 3f) + Vector3.up * (1f + fear / 3f) + base.transform.forward * (fear / 3f) + new Vector3(Random.Range(0f - fear, fear), Random.Range(0f - fear, fear), Random.Range(0f - fear, fear)) / 4f, Time.deltaTime * spdHandsTime);
			}
		}
		if (rightHandToolFreePosition)
		{
			if (ikBody.solver.rightHandEffector.positionWeight > 0.2f)
			{
				ikBody.solver.rightHandEffector.positionWeight -= Time.deltaTime * spdHandsTime;
				if (ikBody.solver.rightHandEffector.positionWeight < 0.2f)
				{
					ikBody.solver.rightHandEffector.positionWeight = 0.2f;
				}
			}
			if (ikBody.solver.rightHandEffector.rotationWeight > 0f)
			{
				ikBody.solver.rightHandEffector.rotationWeight -= Time.deltaTime * spdHandsTime;
				if (ikBody.solver.rightHandEffector.rotationWeight < 0f)
				{
					ikBody.solver.rightHandEffector.rotationWeight = 0f;
				}
			}
			handTargetRight.position = Vector3.Lerp(handTargetRight.position, base.transform.position + base.transform.right * (0.25f + fear / 3f) + Vector3.up * (1f + fear / 3f) + base.transform.forward * (fear / 3f) + new Vector3(Random.Range(0f - fear, fear), Random.Range(0f - fear, fear), Random.Range(0f - fear, fear)) / 4f, Time.deltaTime * spdHandsTime);
		}
		if (!ikBringEndHand && !leftHandUseTool)
		{
			if (handLeftIkTarget > 0)
			{
				spdHead = 1f;
				poserHandLeft.weight = Mathf.Lerp(poserHandLeft.weight, 1f, Time.deltaTime * 10f);
				pointTargetHeadPlayer.position = Vector3.Lerp(pointTargetHeadPlayer.position, interactiveTarget.position, Time.deltaTime * 8f);
				handLeftIkTarget--;
				if (handLeftIkTarget == 0)
				{
					spdHandsTime = 0f;
					StopInteractiveHand(_right: false, _left: true);
				}
				if (ikBody.solver.leftHandEffector.positionWeight < 1f)
				{
					ikBody.solver.leftHandEffector.positionWeight += Time.deltaTime * 2f;
				}
				if (ikBody.solver.leftHandEffector.rotationWeight < 1f)
				{
					ikBody.solver.leftHandEffector.rotationWeight += Time.deltaTime * 4f;
				}
				if (ikBody.solver.leftHandEffector.positionWeight >= 1f)
				{
					leftHandTakeIk = true;
				}
			}
			if (handLeftIkTarget == 0)
			{
				poserHandLeft.weight = Mathf.Lerp(poserHandLeft.weight, 0f, Time.deltaTime * 10f);
				leftHandTakeIk = false;
				if (ikBody.solver.leftHandEffector.positionWeight > 0.2f)
				{
					ikBody.solver.leftHandEffector.positionWeight -= Time.deltaTime * spdHandsTime;
					if (ikBody.solver.leftHandEffector.positionWeight < 0.2f)
					{
						ikBody.solver.leftHandEffector.positionWeight = 0.2f;
					}
				}
				if (ikBody.solver.leftHandEffector.rotationWeight > 0f)
				{
					ikBody.solver.leftHandEffector.rotationWeight -= Time.deltaTime * spdHandsTime;
					if (ikBody.solver.leftHandEffector.rotationWeight < 0f)
					{
						ikBody.solver.leftHandEffector.rotationWeight = 0f;
					}
				}
				handTargetLeft.position = Vector3.Lerp(handTargetLeft.position, base.transform.position + base.transform.right * (-0.25f - fear / 3f) + Vector3.up * (1f + fear / 3f) + base.transform.forward * (fear / 3f) + new Vector3(Random.Range(0f - fear, fear), Random.Range(0f - fear, fear), Random.Range(0f - fear, fear)) / 4f, Time.deltaTime * spdHandsTime);
			}
		}
		if (leftHandToolFreePosition)
		{
			if (ikBody.solver.leftHandEffector.positionWeight > 0.2f)
			{
				ikBody.solver.leftHandEffector.positionWeight -= Time.deltaTime * spdHandsTime;
				if (ikBody.solver.leftHandEffector.positionWeight < 0.2f)
				{
					ikBody.solver.leftHandEffector.positionWeight = 0.2f;
				}
			}
			if (ikBody.solver.leftHandEffector.rotationWeight > 0f)
			{
				ikBody.solver.leftHandEffector.rotationWeight -= Time.deltaTime * spdHandsTime;
				if (ikBody.solver.leftHandEffector.rotationWeight < 0f)
				{
					ikBody.solver.leftHandEffector.rotationWeight = 0f;
				}
			}
			handTargetLeft.position = Vector3.Lerp(handTargetLeft.position, base.transform.position + base.transform.right * (-0.25f + fear / 3f) + Vector3.up * (1f + fear / 3f) + base.transform.forward * (fear / 3f) + new Vector3(Random.Range(0f - fear, fear), Random.Range(0f - fear, fear), Random.Range(0f - fear, fear)) / 4f, Time.deltaTime * spdHandsTime);
		}
		if (handLeftIkTarget == 0 && handRightIkTarget == 0 && headTarget == null)
		{
			if (spdHead < 5f)
			{
				spdHead += Time.deltaTime;
			}
			pointTargetHeadPlayer.position = Vector3.Lerp(pointTargetHeadPlayer.position, cameraPlayer.position + cameraPlayer.forward * 5f, Time.deltaTime * spdHead);
		}
		if (headTarget != null)
		{
			if (spdHead < 5f)
			{
				spdHead += Time.deltaTime;
			}
			pointTargetHeadPlayer.position = Vector3.Lerp(pointTargetHeadPlayer.position, headTarget.position, Time.deltaTime * spdHead);
		}
		if (Input.GetButtonDown("ChangeInteractive"))
		{
			iItemChangeNow++;
			interactiveObjectNow = null;
			InteractiveUpdate();
		}
		if ((!showItemHand && !cameraView && !animationRun) || ikanimationPerson)
		{
			if (Input.GetButton("Mouse Right") || timeTargetView > 0f)
			{
				if (intensityFOV < 3f)
				{
					intensityFOV += Time.deltaTime;
					if (intensityFOV > 3f)
					{
						intensityFOV = 3f;
					}
				}
				mainCam.fieldOfView = Mathf.Lerp(mainCam.fieldOfView, 30f - intensityFOV * 3.33f, Time.deltaTime * 10f);
				spdLerpFOV = 0.01f;
			}
			else
			{
				intensityFOV = 0f;
				if (spdLerpFOV < 10f)
				{
					spdLerpFOV += Time.deltaTime * 10f;
				}
				mainCam.fieldOfView = Mathf.Lerp(mainCam.fieldOfView, 55f, Time.deltaTime * spdLerpFOV);
			}
			interfaceCam.fieldOfView = mainCam.fieldOfView;
			if (timeTargetView > 0f)
			{
				if (targetView == null)
				{
					timeTargetView = 0f;
				}
				else
				{
					Quaternion quaternion = Quaternion.LookRotation(targetView.position - cameraPlayer.position, Vector3.up);
					if (quaternion.eulerAngles.x > 180f)
					{
						quaternion = Quaternion.LookRotation(cameraPlayer.position - targetView.position, Vector3.up);
						viewPointMouse = new Vector3(quaternion.eulerAngles.y + 180f, 0f - quaternion.eulerAngles.x, 0f);
					}
					else
					{
						viewPointMouse = new Vector3(quaternion.eulerAngles.y, quaternion.eulerAngles.x, 0f);
					}
					timeTargetView -= Time.deltaTime;
					if (timeTargetView < 0f)
					{
						timeTargetView = 0f;
						targetView = null;
					}
					base.transform.rotation = Quaternion.Lerp(base.transform.rotation, Quaternion.Euler(0f, viewPoint.x, 0f), Time.deltaTime * 10f);
				}
			}
			if (timeTargetView == 0f)
			{
				viewPointMouse += new Vector3(Input.GetAxis("Mouse X") * 3.5f, (0f - Input.GetAxis("Mouse Y")) * 3.5f, 0f) * (sensitivityMouse + 0.6f);
			}
			if (viewPointMouse.x > 360f)
			{
				viewPointMouse -= new Vector3(360f, 0f, 0f);
			}
			if (viewPointMouse.x < -360f)
			{
				viewPointMouse += new Vector3(360f, 0f, 0f);
			}
			viewPointMouse = new Vector3(viewPointMouse.x, Mathf.Clamp(viewPointMouse.y, -50f, 70f), 0f);
			viewPoint = new Vector3(viewPointMouse.x, Mathf.Lerp(viewPoint.y, viewPointMouse.y, Time.deltaTime * 10f), 0f);
			pointViewPlayer.position = base.transform.position;
			Quaternion quaternion2 = default(Quaternion);
			pointViewPlayer.rotation = Quaternion.Lerp(pointViewPlayer.rotation, Quaternion.Euler(0f, viewPoint.x, 0f), Time.deltaTime * 10f);
			if (!dontMove && !animationRun)
			{
				if (rotationPlayerTo == 0)
				{
					if (Input.GetAxis("Horizontal") != 0f || Input.GetAxis("Vertical") != 0f)
					{
						base.transform.rotation = Quaternion.Lerp(base.transform.rotation, Quaternion.Euler(0f, viewPoint.x, 0f), Time.deltaTime * 10f);
					}
				}
				else
				{
					rotationPlayerTo--;
					if (rotationPlayerTo < 0)
					{
						rotationPlayerTo = 0;
					}
					base.transform.rotation = Quaternion.Lerp(base.transform.rotation, Quaternion.Euler(0f, rotationPlayerToAngle, 0f), Time.deltaTime * 10f);
				}
			}
			quaternion2 = Quaternion.Euler(new Vector3(viewPoint.y, 0f, 0f)) * pointViewPlayer.rotation;
			cameraPlayer.rotation = Quaternion.Euler(new Vector3(viewPoint.y, quaternion2.eulerAngles.y, 0f) + fearLerp * 150f);
			if (noiseCamera > 0f)
			{
				noiseCamera -= Time.deltaTime * 0.025f;
				if (noiseCamera < 0f)
				{
					noiseCamera = 0f;
				}
			}
			Vector3 vector = Vector3.zero;
			if (noiseCamera > 0f)
			{
				vector = new Vector3(Random.Range(0f - noiseCamera, noiseCamera), Random.Range(0f - noiseCamera, noiseCamera), Random.Range(0f - noiseCamera, noiseCamera));
			}
			Vector3 vector2 = pointViewPlayer.position + Vector3.up * 1.6f + pointViewPlayer.right * 0.28f;
			if (Physics.Raycast(pointViewPlayer.position + Vector3.up * 1.6f, pointViewPlayer.right, out var hitInfo, 0.29f, layerCameraWall))
			{
				vector2 = pointViewPlayer.position + Vector3.up * 1.6f + pointViewPlayer.right * (Vector3.Distance(pointViewPlayer.position + Vector3.up * 1.6f, hitInfo.point) / 1.1f);
			}
			Vector3 vector3 = vector2 + pointViewPlayer.forward * -0.55f;
			if (Physics.Raycast(vector2, -cameraPlayer.forward, out hitInfo, Vector3.Distance(vector2, vector3) + 0.1f, layerCameraWall))
			{
				vector3 = hitInfo.point + Vector3.Normalize(vector2 - hitInfo.point) * 0.1f;
			}
			cameraPlayer.position = vector3 + vector;
		}
		if (showItemHand)
		{
			cameraPlayer.LookAt(itemShow);
		}
		if (animationRun && animationPerson)
		{
			if (rotationPlayerNeedActive)
			{
				base.transform.rotation = Quaternion.Lerp(base.transform.rotation, Quaternion.Euler(0f, rotationPlayerNeed, 0f), Time.deltaTime * 8f);
			}
			if (positionPlayerNeedActive)
			{
				base.transform.position = Vector3.Lerp(base.transform.position, positionPlayerNeed, Time.deltaTime * 8f);
			}
			if ((double)Vector3.Distance(base.transform.position, positionPlayerNeed) < 0.05 && Quaternion.Angle(base.transform.rotation, Quaternion.Euler(0f, rotationPlayerNeed, 0f)) < 1f && timeAnimationPlayOther == 0f)
			{
				animatorOverrideController["Other Animation A"] = animationClipNeed;
				timeAnimationPlayOther = animationClipNeed.length;
				animPerson.SetTrigger("OtherAnimation");
			}
			if (timeAnimationPlayOther > 0f)
			{
				timeEventsAnimation += Time.deltaTime;
				for (int i = 0; i < eventsTimeAnimation.Length; i++)
				{
					if (!eventsTimeAnimation[i].eventActive && timeEventsAnimation >= eventsTimeAnimation[i].time)
					{
						eventsTimeAnimation[i]._event.Invoke();
						eventsTimeAnimation[i].eventActive = true;
					}
				}
				timeAnimationPlayOther -= Time.deltaTime;
				if (timeAnimationPlayOther < 0f)
				{
					AnimationPlayerStop();
				}
			}
		}
		if (bsAnimActive)
		{
			if (bsAnimAlpha < 1f)
			{
				bsAnimAlpha += Time.deltaTime * 2f;
				if (bsAnimAlpha > 1f)
				{
					bsAnimAlpha = 1f;
					_eventBsAnim.Invoke();
					bsAnimActive = false;
				}
			}
		}
		else if (bsAnimAlpha > 0f)
		{
			bsAnimAlpha -= Time.deltaTime * 2f;
			if (bsAnimAlpha < 0f)
			{
				bsAnimAlpha = 0f;
			}
		}
		bsAnimation.color = new Color(0f, 0f, 0f, bsAnimAlpha);
	}

	public void IkBringEndHand()
	{
		ikBringEndHand = true;
		handRightIkTarget = 0;
		handLeftIkTarget = 0;
		spdHandsTime = 0f;
		rightHandTakeIk = false;
		leftHandTakeIk = false;
	}

	public void InteractiveUpdate()
	{
		bool flag = false;
		if (interactiveObjectNow != null)
		{
			for (int i = 0; i < 20; i++)
			{
				if (!flag && interactiveObjectsNow[i] == interactiveObjectNow)
				{
					flag = true;
				}
			}
		}
		if (!flag)
		{
			interactiveObjectNow = null;
		}
		if (!(interactiveObjectNow == null))
		{
			return;
		}
		for (int j = 0; j < 20; j++)
		{
			if (!(interactiveObjectNow == null))
			{
				continue;
			}
			if (interactiveObjectsNow[iItemChangeNow] != null)
			{
				interactiveObjectNow = interactiveObjectsNow[iItemChangeNow];
				interactiveTarget.gameObject.SetActive(value: true);
				continue;
			}
			iItemChangeNow++;
			if (iItemChangeNow == 20)
			{
				iItemChangeNow = 0;
			}
		}
	}

	public void HeadTargetPlayer(Transform _obj)
	{
		if (_obj != null)
		{
			headTarget = _obj;
		}
		else
		{
			headTarget = null;
		}
	}

	public void TeleportFast(Vector3 _pos, float _rot)
	{
		base.transform.position = _pos;
		base.transform.rotation = Quaternion.Euler(0f, _rot, 0f);
		viewPoint = new Vector2(_rot, 0f);
		viewPointMouse = viewPoint;
		cameraPlayer.rotation = Quaternion.Euler(new Vector3(viewPoint.y, viewPoint.x, 0f));
		pointViewPlayer.position = base.transform.position;
		pointViewPlayer.rotation = Quaternion.Euler(0f, viewPoint.x, 0f);
		if (Physics.Raycast(pointViewPlayer.position + (pointViewPlayer.right * 0.2f + pointViewPlayer.up * 1.7f), -cameraPlayer.forward, out var hitInfo, 0.5f))
		{
			cameraPlayer.position = hitInfo.point + cameraPlayer.forward * 0.2f;
		}
		else
		{
			cameraPlayer.position = pointViewPlayer.position + (pointViewPlayer.right * 0.2f + pointViewPlayer.up * 1.7f + pointViewPlayer.forward * -0.5f);
		}
		pointTargetHeadPlayer.position = base.transform.position + base.transform.forward * 5f + base.transform.up * 1.5f;
		pointTargetEyesPlayer.position = pointTargetHeadPlayer.position;
		if (!ikBringEndHand && !leftHandUseTool)
		{
			handTargetLeft.position = base.transform.position + base.transform.right * (-0.25f - fear / 3f) + Vector3.up * (1f + fear / 3f) + base.transform.forward * (fear / 3f);
		}
		if (!ikBringEndHand && !rightHandUseTool)
		{
			handTargetRight.position = base.transform.position + base.transform.right * (0.25f + fear / 3f) + Vector3.up * (1f + fear / 3f) + base.transform.forward * (fear / 3f);
		}
	}

	public void AnimationPlayablePlay()
	{
		ConsoleMain.ConsolePrint("Player Playable Play");
		if (!fs)
		{
			Start();
		}
		pointTargetEyesPlayer.gameObject.GetComponent<LookAtIK>().solver.IKPositionWeight = 0f;
		ikBody.solver.IKPositionWeight = 0f;
		playerRigidbody = GetComponent<Rigidbody>();
		ikLook = GetComponent<LookAtIK>();
		animPerson.runtimeAnimatorController = null;
		playerRigidbody.isKinematic = true;
		ikLook.solver.SetLookAtWeight(0f);
		animationRun = true;
		ShowItemHandCancel();
		creac.SetActive(value: false);
	}

	public void AnimationPlayableStop(Vector3 posTP, float rotTP)
	{
		ConsoleMain.ConsolePrint("Player Playable Stop");
		pointTargetEyesPlayer.gameObject.GetComponent<LookAtIK>().solver.IKPositionWeight = 1f;
		ikBody.solver.IKPositionWeight = 1f;
		animationRun = false;
		playerRigidbody.isKinematic = false;
		animPerson.runtimeAnimatorController = animatorController;
		ikLook.solver.SetLookAtWeight(1f);
		TeleportFast(posTP, rotTP);
		ShowItemHandCancel();
		if (creacSkip)
		{
			creac.SetActive(value: true);
		}
	}

	public void StartInteractiveHands(Transform _handRight, Transform _handLeft, bool _free)
	{
		handsPickup.HandsTarget(_handRight, _handLeft, _free);
	}

	public void StopInteractiveHands()
	{
		handTargetRight.parent = base.transform;
		handTargetLeft.parent = base.transform;
	}

	public void StopInteractiveHand(bool _right, bool _left)
	{
		handsPickup.StopTargetHand(_right, _left);
		if (_right)
		{
			handTargetRight.parent = base.transform;
		}
		if (_left)
		{
			handTargetLeft.parent = base.transform;
		}
	}

	public void ToolTakeObject(GameObject _obj, bool _freePosition)
	{
		toolItem = _obj;
		handLeftIkTarget = 0;
		handRightIkTarget = 0;
		toolItem.transform.SetParent(bodyHeight);
		if (toolItem.GetComponent<Item_Tool>().inHands == Item_Tool.EnumToolHand.leftHand)
		{
			leftHandUseTool = true;
			leftHandToolFreePosition = _freePosition;
			spdHead = 1f;
			poserHandLeft.weight = 1f;
			pointTargetHeadPlayer.position = interactiveTarget.position;
			ikBody.solver.leftHandEffector.positionWeight = 1f;
			ikBody.solver.leftHandEffector.rotationWeight = 1f;
		}
		if (toolItem.GetComponent<Item_Tool>().inHands == Item_Tool.EnumToolHand.rightHand)
		{
			rightHandUseTool = true;
			rightHandToolFreePosition = _freePosition;
			spdHead = 1f;
			poserHandRight.weight = 1f;
			pointTargetHeadPlayer.position = interactiveTarget.position;
			ikBody.solver.rightHandEffector.positionWeight = 1f;
			ikBody.solver.rightHandEffector.rotationWeight = 1f;
		}
	}

	public void ShowItemHand(GameObject _obj)
	{
		itemShow = _obj.transform;
		showItemHand = true;
		if (!_obj.GetComponent<Item_ShowHand>().globalPosition)
		{
			cameraPlayer.position = _obj.GetComponent<Item_ShowHand>().positionStart + _obj.GetComponent<Item_ShowHand>().positionCamera;
		}
		else
		{
			cameraPlayer.position = _obj.GetComponent<Item_ShowHand>().positionCamera;
		}
		mainCam.fieldOfView = _obj.GetComponent<Item_ShowHand>().cameraFieldOfView;
		dontMove = true;
		busy = true;
	}

	public void ShowItemHandCancel()
	{
		showItemHand = false;
		dontMove = false;
		StopInteractiveHand(_right: true, _left: true);
		busy = false;
		if (toolItem != null && (rightHandUseTool || leftHandUseTool))
		{
			toolItem.GetComponent<Item_Tool>().TakeTool();
		}
		if (!ikBringEndHand && !leftHandUseTool)
		{
			handTargetLeft.position = base.transform.position + base.transform.right * (-0.25f - fear / 3f) + Vector3.up * (1f + fear / 3f) + base.transform.forward * (fear / 3f);
		}
		if (!ikBringEndHand && !rightHandUseTool)
		{
			handTargetRight.position = base.transform.position + base.transform.right * (0.25f + fear / 3f) + Vector3.up * (1f + fear / 3f) + base.transform.forward * (fear / 3f);
		}
	}

	private void OnDrawGizmosSelected()
	{
		if (head != null)
		{
			Gizmos.color = Color.red;
			Gizmos.DrawLine(head.transform.position, head.transform.position + head.transform.right * eyesRotation.x + head.transform.up * eyesRotation.y + head.transform.forward * eyesRotation.z);
		}
	}

	public void AnimationPlayPerson(AnimationClip _animClip, Vector3 _pos, float _rot, TimePointBool[] _eventsAnimation, bool _IKPlayer)
	{
		if (animationRun)
		{
			AnimationPlayerStop();
		}
		ConsoleMain.ConsolePrint("Player Animation Play");
		ikanimationPerson = _IKPlayer;
		if (!fs)
		{
			Start();
		}
		playerRigidbody = GetComponent<Rigidbody>();
		playerRigidbody.isKinematic = true;
		animationRun = true;
		animationPerson = true;
		ShowItemHandCancel();
		animationClipNeed = _animClip;
		rotationPlayerNeed = _rot;
		rotationPlayerNeedActive = true;
		positionPlayerNeed = _pos;
		positionPlayerNeedActive = true;
		timeEventsAnimation = 0f;
		eventsTimeAnimation = _eventsAnimation;
		ikLook = GetComponent<LookAtIK>();
		if (!ikanimationPerson)
		{
			ikLook.solver.SetLookAtWeight(0f);
			pointTargetEyesPlayer.gameObject.GetComponent<LookAtIK>().solver.IKPositionWeight = 0f;
			ikBody.solver.IKPositionWeight = 0f;
			audioBreath.Stop();
			intensityFOV = 0f;
			mainCam.fieldOfView = Mathf.Lerp(mainCam.fieldOfView, 60f, Time.deltaTime * 10f);
			interfaceCam.fieldOfView = mainCam.fieldOfView;
		}
		else
		{
			ikLook.solver.SetLookAtWeight(1f);
		}
	}

	private void AnimationPlayerStop()
	{
		ConsoleMain.ConsolePrint("Player Animation Stop");
		animationPerson = false;
		timeAnimationPlayOther = 0f;
		pointTargetEyesPlayer.gameObject.GetComponent<LookAtIK>().solver.IKPositionWeight = 1f;
		ikBody.solver.IKPositionWeight = 1f;
		animationRun = false;
		playerRigidbody.isKinematic = false;
		animPerson.runtimeAnimatorController = animatorController;
		ikLook.solver.SetLookAtWeight(1f);
		ShowItemHandCancel();
		bodyMesh.SetBlendShapeWeight(bodyMesh.sharedMesh.GetBlendShapeIndex("Brown Stressed"), 80f);
		bodyMesh.SetBlendShapeWeight(bodyMesh.sharedMesh.GetBlendShapeIndex("Brown Down"), 20f);
		bodyMesh.SetBlendShapeWeight(bodyMesh.sharedMesh.GetBlendShapeIndex("Eyes Stare"), 10f);
		bodyMesh.SetBlendShapeWeight(bodyMesh.sharedMesh.GetBlendShapeIndex("Mouth Hmm"), 70f);
		tmsetblsh = 0.5f;
		if (ikanimationPerson)
		{
			ikanimationPerson = false;
			audioBreath.Play();
			intensityFOV = 0f;
			mainCam.fieldOfView = Mathf.Lerp(mainCam.fieldOfView, 60f, Time.deltaTime * 10f);
			interfaceCam.fieldOfView = mainCam.fieldOfView;
		}
	}

	public void Fear(float _float)
	{
		fear = _float;
	}

	public void DontMove(bool x)
	{
		dontMove = x;
	}

	public void TargetView(Transform _transform)
	{
		targetView = _transform;
		timeTargetView = 1.25f;
	}

	public void Noise(float _float)
	{
		noiseCamera = _float / 50f;
	}

	public void BSAnim(UnityEvent _event)
	{
		_eventBsAnim = _event;
		bsAnimActive = true;
	}

	public void HandsFree()
	{
		handsPickup.StopTargetHand(_right: true, _left: true);
		handTargetRight.parent = base.transform;
		handTargetLeft.parent = base.transform;
		rightHandToolFreePosition = false;
		leftHandToolFreePosition = false;
		rightHandUseTool = false;
		leftHandUseTool = false;
	}

	public void CameraViewStart(Vector3 _pos, Vector3 _rot)
	{
		dontMove = true;
		cameraView = true;
		cameraPlayer.position = _pos;
		cameraPlayer.rotation = Quaternion.Euler(_rot);
	}

	public void CameraViewStop()
	{
		dontMove = false;
		cameraView = false;
	}

	public void AnimationSpeed(float x)
	{
		animPerson.speed = x;
	}

	public void UpdateSettings()
	{
		sensitivityMouse = PlayerPrefs.GetFloat("SensitivityMouse", 0.5f);
	}

	public void BlendShapeSetStress()
	{
		bodyMesh.SetBlendShapeWeight(bodyMesh.sharedMesh.GetBlendShapeIndex("Brown Stressed"), 80f);
		bodyMesh.SetBlendShapeWeight(bodyMesh.sharedMesh.GetBlendShapeIndex("Brown Down"), 20f);
		bodyMesh.SetBlendShapeWeight(bodyMesh.sharedMesh.GetBlendShapeIndex("Eyes Stare"), 10f);
		bodyMesh.SetBlendShapeWeight(bodyMesh.sharedMesh.GetBlendShapeIndex("Mouth Hmm"), 70f);
	}

	public void CreacSkip(bool x)
	{
		if (x)
		{
			creac.SetActive(value: false);
		}
		else
		{
			creac.SetActive(value: true);
		}
		creacSkip = !x;
	}
}
