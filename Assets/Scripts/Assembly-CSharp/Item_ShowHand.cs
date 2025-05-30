using UnityEngine;
using UnityEngine.Events;

public class Item_ShowHand : MonoBehaviour
{
	public bool active = true;

	[Range(0f, 180f)]
	public float intensityRotation = 45f;

	public float intensityPoshand = 25f;

	[Range(0f, 179f)]
	public float cameraFieldOfView = 60f;

	public bool noPosition;

	public bool globalPosition;

	public Vector3 positionShow;

	public Vector3 positionCamera;

	public Vector3[] rotation;

	[Header("Events")]
	public bool dontCancel;

	public bool deactiveActionAfter;

	public UnityEvent _eventEnd;

	[Header("Animation")]
	public bool animationRepeat = true;

	public string animationState;

	public float speedAnimation = 1f;

	[HideInInspector]
	public Vector3 positionStart;

	private int typeShow;

	private bool show;

	private bool showAnimation;

	private bool fs;

	private bool interactiveActionBefore;

	private Quaternion rotationStart;

	private float timeStart;

	private float animationFrame;

	private float animationFrameNeed;

	private Animator anim;

	private Interactive_Action ia;

	private Transform transformPlayer;

	private Vector3 posInput;

	private void Start()
	{
		if (!fs)
		{
			transformPlayer = GameObject.FindWithTag("Player").transform;
			fs = true;
			ia = GetComponent<Interactive_Action>();
			rotationStart = base.transform.rotation;
			positionStart = base.transform.position;
			if (animationState != "")
			{
				anim = GetComponent<Animator>();
				showAnimation = true;
			}
		}
	}

	private void OnEnable()
	{
		animationFrame = 0f;
	}

	private void LateUpdate()
	{
		if (timeStart > 0f)
		{
			timeStart -= 1f;
			if (timeStart < 0f)
			{
				timeStart = 0f;
			}
		}
		if (!active)
		{
			return;
		}
		if (show)
		{
			if (timeStart != 0f)
			{
				return;
			}
			if (!showAnimation)
			{
				posInput = Vector3.Lerp(posInput, transformPlayer.right * (Input.GetAxis("Horizontal") / intensityPoshand) + transformPlayer.up * (Input.GetAxis("Vertical") / intensityPoshand), Time.deltaTime * 8f);
				base.transform.rotation = Quaternion.Lerp(base.transform.rotation, Quaternion.Euler(rotation[typeShow] + new Vector3(Input.GetAxis("Vertical") * intensityRotation, Input.GetAxis("Horizontal") * (0f - intensityRotation), 0f)), Time.deltaTime * 8f);
				if (!noPosition)
				{
					if (!globalPosition)
					{
						base.transform.position = Vector3.Lerp(base.transform.position, positionStart + positionShow + posInput, Time.deltaTime * 8f);
					}
					else
					{
						base.transform.position = Vector3.Lerp(base.transform.position, positionShow + posInput, Time.deltaTime * 8f);
					}
				}
				if (Input.GetButtonDown("Action") && !dontCancel)
				{
					UnityEvent eventEnd = _eventEnd;
					eventEnd.AddListener(ShowCancel);
					transformPlayer.gameObject.GetComponent<Player>().BSAnim(eventEnd);
				}
			}
			if (!showAnimation || !(animationFrame < 1f))
			{
				return;
			}
			if (Input.GetButton("Action"))
			{
				animationFrameNeed += Time.deltaTime * speedAnimation;
			}
			animationFrame = Mathf.Lerp(animationFrame, animationFrameNeed, Time.deltaTime * 8f);
			anim.Play(animationState, -1, animationFrame);
			if (animationFrame >= 0.99f)
			{
				animationFrame = 1f;
				_eventEnd.Invoke();
				Show(x: false);
				if (animationRepeat)
				{
					animationFrame = 0f;
					animationFrameNeed = 0f;
				}
			}
		}
		else
		{
			base.transform.rotation = Quaternion.Lerp(base.transform.rotation, rotationStart, Time.deltaTime * 8f);
			base.transform.position = Vector3.Lerp(base.transform.position, positionStart, Time.deltaTime * 8f);
		}
	}

	public void Show(bool x)
	{
		show = x;
		active = true;
		if (x)
		{
			FastShow(x);
		}
	}

	public void FastShow(bool x)
	{
		Start();
		show = x;
		active = true;
		timeStart = 5f;
		if (!showAnimation)
		{
			if (GetComponent<Animator>() != null)
			{
				GetComponent<Animator>().enabled = false;
			}
		}
		else
		{
			GetComponent<Animator>().enabled = true;
		}
		if (x)
		{
			ConsoleMain.ConsolePrint("Fast show item | true");
			interactiveActionBefore = ia.active;
			if (GameObject.FindWithTag("Player") != null)
			{
				GameObject.FindWithTag("Player").GetComponent<Player>().ShowItemHand(base.gameObject);
			}
			if (!showAnimation)
			{
				base.transform.rotation = Quaternion.Euler(rotation[typeShow]);
			}
			if (!noPosition)
			{
				if (!globalPosition)
				{
					base.transform.position = positionStart + positionShow;
				}
				else
				{
					base.transform.position = positionShow;
				}
			}
			ia.FastHold(x: true);
		}
		else
		{
			ConsoleMain.ConsolePrint("Fast show item | false");
			GameObject.FindWithTag("Player").GetComponent<Player>().ShowItemHandCancel();
			ia.hold = false;
			ia.Activation(interactiveActionBefore);
			if (deactiveActionAfter)
			{
				ia.Activation(x: false);
			}
			base.transform.rotation = rotationStart;
			base.transform.position = positionStart;
		}
	}

	private void OnDrawGizmosSelected()
	{
		if (!noPosition)
		{
			Gizmos.color = Color.green;
			Gizmos.DrawSphere(base.transform.position, 0.01f);
			if (!globalPosition)
			{
				Gizmos.DrawLine(base.transform.position, base.transform.position + positionShow);
				Gizmos.DrawSphere(base.transform.position + positionShow, 0.01f);
			}
			else
			{
				Gizmos.DrawLine(base.transform.position, positionShow);
				Gizmos.DrawSphere(positionShow, 0.01f);
			}
		}
		Gizmos.color = Color.white;
		if (!globalPosition)
		{
			Gizmos.DrawSphere(base.transform.position + positionCamera, 0.02f);
		}
		else
		{
			Gizmos.DrawSphere(positionCamera, 0.02f);
		}
	}

	public void ShowCancel()
	{
		FastShow(x: false);
	}

	public void Activation(bool x)
	{
		active = x;
	}
}
