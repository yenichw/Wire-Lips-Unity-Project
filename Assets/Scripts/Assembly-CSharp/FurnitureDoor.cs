using UnityEngine;
using UnityEngine.Events;

public class FurnitureDoor : MonoBehaviour
{
	public AudioClip[] soundOpen;

	public AudioClip[] soundClose;

	public GameObject roomLeft;

	public GameObject roomRight;

	public UnityEvent openDoorEvent;

	public UnityEvent closeDoorEvent;

	public bool dontSwitchRooms;

	[HideInInspector]
	public UnityEvent openDoorEventOne;

	[HideInInspector]
	public UnityEvent closeDoorEventOne;

	[HideInInspector]
	public UnityEvent ifDoorClosedEventOne;

	[HideInInspector]
	public bool boolIfDoorClosedEventOne;

	private GameObject t_door;

	private Rigidbody rb_door;

	private Transform player;

	private Transform door;

	private bool doorOpen;

	private bool fs;

	private bool oneEventOpen;

	private bool oneEventClose;

	private bool animationPlay;

	private bool doorLock;

	private Vector3 posMindDoor;

	private float springForce;

	private Animator anim;

	private AudioSource aud;

	private bool playerRightExit;

	private JointSpring springeDoor;

	private void Start()
	{
		if (!fs)
		{
			fs = true;
			door = base.transform.Find("Shoal/Door").transform;
			aud = base.transform.Find("AudioSource").gameObject.GetComponent<AudioSource>();
			t_door = door.gameObject;
			t_door.layer = 9;
			rb_door = t_door.GetComponent<Rigidbody>();
			springeDoor = t_door.GetComponent<HingeJoint>().spring;
			posMindDoor = new Vector3(t_door.transform.position.x, 0f, t_door.transform.position.z) - t_door.transform.up * 0.4f;
			if (GetComponent<Animator>() != null)
			{
				anim = GetComponent<Animator>();
				anim.enabled = false;
			}
		}
	}

	public void OnEnable()
	{
		if (!fs)
		{
			Start();
		}
		posMindDoor = new Vector3(t_door.transform.position.x, 0f, t_door.transform.position.z) - t_door.transform.up * 0.4f;
	}

	private void Update()
	{
		if (animationPlay)
		{
			return;
		}
		if (t_door.transform.localRotation.eulerAngles.z < 3f && t_door.transform.localRotation.eulerAngles.z > -3f)
		{
			t_door.transform.localRotation = Quaternion.Lerp(t_door.transform.localRotation, Quaternion.Euler(Vector3.zero), 0.2f);
			rb_door.velocity = Vector3.zero;
			rb_door.angularVelocity = Vector3.zero;
			if (t_door.transform.localRotation.eulerAngles.z < 0.25f && t_door.transform.localRotation.eulerAngles.z > -0.25f)
			{
				if (!doorLock)
				{
					t_door.layer = 9;
				}
				if (doorOpen)
				{
					DoorClose();
				}
				if (roomLeft != null && roomRight != null && dontSwitchRooms && roomLeft.activeInHierarchy && roomRight.activeInHierarchy)
				{
					DoorClose();
				}
			}
		}
		if (!doorOpen && boolIfDoorClosedEventOne)
		{
			ifDoorClosedEventOne.Invoke();
			boolIfDoorClosedEventOne = false;
		}
		if (t_door.transform.localRotation.eulerAngles.z > 0.25f || t_door.transform.localRotation.eulerAngles.z < -0.25f)
		{
			if (!doorLock)
			{
				t_door.layer = 11;
			}
			if (!doorOpen)
			{
				DoorOpen();
			}
			if (roomLeft != null && roomRight != null && dontSwitchRooms && (!roomLeft.activeInHierarchy || !roomRight.activeInHierarchy))
			{
				DoorOpen();
			}
		}
		if (doorLock && doorOpen)
		{
			door.localRotation = Quaternion.Lerp(door.localRotation, Quaternion.Euler(Vector3.zero), Time.deltaTime * 10f);
		}
		if (player == null && GameObject.FindWithTag("Player") != null)
		{
			player = GameObject.FindWithTag("Player").transform;
		}
		if (!(player != null))
		{
			return;
		}
		if ((double)Vector3.Distance(posMindDoor, player.position) < 1.5)
		{
			springeDoor.spring = 0f;
			t_door.GetComponent<HingeJoint>().spring = springeDoor;
			springForce = 0f;
			if (Vector3.Dot(base.transform.right, player.position - posMindDoor) > 0f)
			{
				playerRightExit = true;
			}
			else
			{
				playerRightExit = false;
			}
		}
		if ((double)Vector3.Distance(posMindDoor, player.position) > 1.5)
		{
			springeDoor.spring = 2f + springForce;
			if (springForce < 100f)
			{
				springForce += Time.deltaTime * 10f;
			}
			t_door.GetComponent<HingeJoint>().spring = springeDoor;
		}
	}

	private void DoorClose()
	{
		doorOpen = false;
		if (!(player != null))
		{
			return;
		}
		if (!dontSwitchRooms)
		{
			if (playerRightExit)
			{
				if (roomLeft != null)
				{
					roomLeft.SetActive(value: false);
				}
			}
			else if (roomRight != null)
			{
				roomRight.SetActive(value: false);
			}
		}
		if (oneEventClose)
		{
			closeDoorEventOne.Invoke();
			oneEventClose = false;
		}
		closeDoorEvent.Invoke();
		aud.clip = soundClose[Random.Range(0, soundClose.Length)];
		aud.pitch = Random.Range(0.95f, 1.05f);
		aud.volume = 0.8f;
		aud.Play();
		ConsoleMain.ConsolePrint("Door Close (" + base.gameObject.name + ")");
	}

	private void DoorOpen()
	{
		doorOpen = true;
		if (!(player != null))
		{
			return;
		}
		if (!dontSwitchRooms && (double)Vector3.Distance(posMindDoor, player.position) < 1.5)
		{
			if (roomLeft != null)
			{
				roomLeft.SetActive(value: true);
			}
			if (roomRight != null)
			{
				roomRight.SetActive(value: true);
			}
		}
		if (oneEventOpen)
		{
			openDoorEventOne.Invoke();
			oneEventOpen = false;
		}
		openDoorEvent.Invoke();
		aud.volume = 0.4f;
		aud.clip = soundOpen[Random.Range(0, soundOpen.Length)];
		aud.pitch = Random.Range(0.95f, 1.05f);
		aud.Play();
		ConsoleMain.ConsolePrint("Door Open (" + base.gameObject.name + ")");
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.DrawLine(posMindDoor, posMindDoor - base.transform.up * 10f);
	}

	public void AnimationStartDoor(string _nameState)
	{
		ConsoleMain.ConsolePrint("Door Animation Start (" + base.gameObject.name + ")");
		animationPlay = true;
		if (!fs)
		{
			Start();
		}
		anim = GetComponent<Animator>();
		anim.enabled = true;
		rb_door.isKinematic = true;
		if (_nameState != "")
		{
			anim.Play(_nameState, -1, 0f);
		}
	}

	public void StopAnimationDoor()
	{
		ConsoleMain.ConsolePrint("Door Animation Stop (" + base.gameObject.name + ")");
		rb_door.isKinematic = false;
		animationPlay = false;
		if (!fs)
		{
			Start();
		}
		springeDoor.spring = 0f;
		t_door.GetComponent<HingeJoint>().spring = springeDoor;
		anim = GetComponent<Animator>();
		anim.enabled = false;
	}

	public void RoomsActivation(bool x)
	{
		ConsoleMain.ConsolePrint("Door Rooms Activation (" + base.gameObject.name + ")");
		if (roomLeft != null)
		{
			roomLeft.SetActive(x);
		}
		if (roomRight != null)
		{
			roomRight.SetActive(x);
		}
	}

	public void FixRoomsActivePlayer()
	{
		ConsoleMain.ConsolePrint("Door FixRoom (" + base.gameObject.name + ")");
		if (!(player != null))
		{
			return;
		}
		if (Vector3.Dot(base.transform.right, player.position - posMindDoor) > 0f)
		{
			if (roomLeft != null)
			{
				roomLeft.SetActive(value: false);
			}
			if (roomRight != null)
			{
				roomRight.SetActive(value: true);
			}
		}
		else
		{
			if (roomRight != null)
			{
				roomRight.SetActive(value: false);
			}
			if (roomLeft != null)
			{
				roomLeft.SetActive(value: true);
			}
		}
	}

	public void DoorLock(bool x)
	{
		if (!fs)
		{
			Start();
		}
		if (x)
		{
			t_door.layer = 0;
			doorLock = true;
		}
		else
		{
			doorLock = false;
		}
		if (animationPlay)
		{
			rb_door.isKinematic = true;
		}
		else
		{
			rb_door.isKinematic = x;
		}
	}

	public void DontSwitchRooms(bool x)
	{
		dontSwitchRooms = x;
	}

	public void ReEvents(UnityEvent _openDoorEvent, UnityEvent _closeDoorEvent, UnityEvent _openDoorEventOne, UnityEvent _closeDoorEventOne, UnityEvent _ifClosedDoorEventOne)
	{
		boolIfDoorClosedEventOne = true;
		ifDoorClosedEventOne = _ifClosedDoorEventOne;
		oneEventOpen = true;
		oneEventClose = true;
		openDoorEvent = _openDoorEvent;
		closeDoorEvent = _closeDoorEvent;
		openDoorEventOne = _openDoorEventOne;
		closeDoorEventOne = _closeDoorEventOne;
	}

	public void LearnDot()
	{
		if (Vector3.Dot(base.transform.right, player.position - posMindDoor) > 0f)
		{
			playerRightExit = true;
		}
		else
		{
			playerRightExit = false;
		}
	}
}
