using UnityEngine;
using UnityEngine.Events;

public class LockItemsCount : MonoBehaviour
{
	public UnityEvent eventOpenLock;

	public GameObject[] dolls;

	public GameObject[] pictures;

	public GameObject[] chains;

	public Vector3 positionCamera;

	public Vector3 rotationCamera;

	[Header("Lock")]
	public int countNowDolls;

	public int countNowPicture;

	public int countNowChains;

	public Transform wheelDolls;

	public Transform wheelPicture;

	public Transform wheelChains;

	private int typeWheel;

	private Animator anim;

	[Header("Interface")]
	public Interactive_Action interactive;

	public Animator interfaceLock;

	public RectTransform rectTargetWheel;

	public RectTransform rectPointerRight;

	public RectTransform rectPointerLeft;

	public RectTransform rectPointerUp;

	public RectTransform rectPointerDown;

	public Animator animButtonApply;

	public Animator animButtonCancel;

	[Header("Sounds")]
	private AudioSource audioLock;

	public AudioClip soundLock;

	public AudioClip soundWheel;

	public AudioClip soundUpDown;

	public AudioClip soundOpen;

	[Header("Information")]
	public int countDolls;

	public int countPictures;

	public int countChains;

	[HideInInspector]
	public UnityEvent eventNull;

	private bool look;

	private bool downKey;

	private bool upKey;

	private bool leftKey;

	private bool rightKey;

	private void Start()
	{
		for (int i = 0; i < Random.Range(1, 20); i++)
		{
			dolls[Random.Range(0, 8)].SetActive(value: true);
		}
		for (int j = 0; j < 8; j++)
		{
			if (dolls[j].activeInHierarchy)
			{
				countDolls++;
			}
		}
		for (int k = 0; k < Random.Range(1, 20); k++)
		{
			pictures[Random.Range(0, 8)].SetActive(value: true);
		}
		for (int l = 0; l < 8; l++)
		{
			if (pictures[l].activeInHierarchy)
			{
				countPictures++;
			}
		}
		for (int m = 0; m < Random.Range(1, 20); m++)
		{
			chains[Random.Range(0, 8)].SetActive(value: true);
		}
		for (int n = 0; n < 8; n++)
		{
			if (chains[n].activeInHierarchy)
			{
				countChains++;
			}
		}
		rectTargetWheel = base.transform.Find("Lock/Interface Lock/Target").gameObject.GetComponent<RectTransform>();
		anim = base.transform.Find("Lock").gameObject.GetComponent<Animator>();
		countNowDolls = 1;
		countNowPicture = 1;
		countNowChains = 1;
		audioLock = base.transform.Find("Lock").gameObject.GetComponent<AudioSource>();
	}

	private void Update()
	{
		if (!look)
		{
			return;
		}
		wheelDolls.transform.localRotation = Quaternion.Lerp(wheelDolls.transform.localRotation, Quaternion.Euler(0f, 0f, -50f + 46f * (float)(countNowDolls - 1)), Time.deltaTime * 10f);
		wheelPicture.transform.localRotation = Quaternion.Lerp(wheelPicture.transform.localRotation, Quaternion.Euler(0f, 0f, -50f + 46f * (float)(countNowPicture - 1)), Time.deltaTime * 10f);
		wheelChains.transform.localRotation = Quaternion.Lerp(wheelChains.transform.localRotation, Quaternion.Euler(0f, 0f, -50f + 46f * (float)(countNowChains - 1)), Time.deltaTime * 10f);
		rectTargetWheel.anchoredPosition = Vector2.Lerp(rectTargetWheel.anchoredPosition, new Vector2(-145f, 125 * typeWheel), Time.deltaTime * 10f);
		rectPointerRight.sizeDelta = Vector2.Lerp(rectPointerRight.sizeDelta, new Vector2(50f, 50f), Time.deltaTime * 10f);
		rectPointerLeft.sizeDelta = Vector2.Lerp(rectPointerLeft.sizeDelta, new Vector2(50f, 50f), Time.deltaTime * 10f);
		rectPointerUp.sizeDelta = Vector2.Lerp(rectPointerUp.sizeDelta, new Vector2(50f, 50f), Time.deltaTime * 10f);
		rectPointerDown.sizeDelta = Vector2.Lerp(rectPointerDown.sizeDelta, new Vector2(50f, 50f), Time.deltaTime * 10f);
		if (Input.GetButtonDown("Action"))
		{
			if (countNowDolls == countDolls && countNowPicture == countPictures && countNowChains == countChains)
			{
				OpenLock();
			}
			else
			{
				anim.SetTrigger("Close");
				SoundPlay(soundLock, 0.5f, 0.9f, 1.1f);
			}
		}
		if (Input.GetButtonDown("Space"))
		{
			look = false;
			UnityEvent unityEvent = eventNull;
			unityEvent.AddListener(CancelLook);
			GameObject.FindWithTag("Player").GetComponent<Player>().BSAnim(unityEvent);
		}
		if (!downKey && (double)Input.GetAxis("Vertical") < -0.4)
		{
			downKey = true;
			upKey = false;
			Down();
		}
		if (!upKey && (double)Input.GetAxis("Vertical") > 0.4)
		{
			upKey = true;
			downKey = false;
			Up();
		}
		if ((double)Input.GetAxis("Vertical") < 0.4 && (double)Input.GetAxis("Vertical") > -0.4)
		{
			upKey = false;
			downKey = false;
		}
		if (!leftKey && (double)Input.GetAxis("Horizontal") < -0.4)
		{
			leftKey = true;
			rightKey = false;
			Left();
		}
		if (!rightKey && (double)Input.GetAxis("Horizontal") > 0.4)
		{
			rightKey = true;
			downKey = false;
			Right();
		}
		if ((double)Input.GetAxis("Horizontal") < 0.4 && (double)Input.GetAxis("Horizontal") > -0.4)
		{
			rightKey = false;
			leftKey = false;
		}
	}

	private void Down()
	{
		rectPointerDown.sizeDelta = new Vector2(80f, 80f);
		typeWheel--;
		if (typeWheel < 0)
		{
			typeWheel = 2;
		}
		SoundPlay(soundUpDown, 0.5f, 0.9f, 1.1f);
	}

	private void Up()
	{
		SoundPlay(soundUpDown, 0.5f, 0.9f, 1.1f);
		rectPointerUp.sizeDelta = new Vector2(80f, 80f);
		typeWheel++;
		if (typeWheel > 2)
		{
			typeWheel = 0;
		}
	}

	private void Right()
	{
		SoundPlay(soundWheel, 0.5f, 0.9f, 1.1f);
		rectPointerRight.sizeDelta = new Vector2(80f, 80f);
		if (typeWheel == 0)
		{
			countNowDolls++;
			if (countNowDolls > 8)
			{
				countNowDolls = 1;
			}
		}
		if (typeWheel == 1)
		{
			countNowPicture++;
			if (countNowPicture > 8)
			{
				countNowPicture = 1;
			}
		}
		if (typeWheel == 2)
		{
			countNowChains++;
			if (countNowChains > 8)
			{
				countNowChains = 1;
			}
		}
	}

	private void Left()
	{
		SoundPlay(soundWheel, 0.5f, 0.9f, 1.1f);
		rectPointerLeft.sizeDelta = new Vector2(80f, 80f);
		if (typeWheel == 0)
		{
			countNowDolls--;
			if (countNowDolls < 1)
			{
				countNowDolls = 8;
			}
		}
		if (typeWheel == 1)
		{
			countNowPicture--;
			if (countNowPicture < 1)
			{
				countNowPicture = 8;
			}
		}
		if (typeWheel == 2)
		{
			countNowChains--;
			if (countNowChains < 1)
			{
				countNowChains = 8;
			}
		}
	}

	public void LookLock(bool x)
	{
		look = x;
		if (x)
		{
			interfaceLock.SetBool("Hide", value: false);
			GameObject.FindWithTag("Player").GetComponent<Player>().CameraViewStart(positionCamera, rotationCamera);
			animButtonApply.gameObject.SetActive(value: true);
			animButtonCancel.gameObject.SetActive(value: true);
			animButtonApply.SetTrigger("Start");
			animButtonCancel.SetTrigger("Start");
			interactive.Activation(x: false);
		}
		else
		{
			interfaceLock.SetBool("Hide", value: true);
			GameObject.FindWithTag("Player").GetComponent<Player>().CameraViewStop();
			animButtonApply.SetTrigger("Hide");
			animButtonCancel.SetTrigger("Hide");
			interactive.Activation(x: true);
		}
	}

	public void OpenLock()
	{
		look = false;
		GameObject.FindWithTag("Player").GetComponent<Player>().CameraViewStop();
		anim.SetBool("Open", value: true);
		eventOpenLock.Invoke();
		animButtonApply.SetTrigger("End");
		animButtonCancel.SetTrigger("End");
		SoundPlay(soundOpen, 1f);
		interfaceLock.SetBool("Hide", value: true);
	}

	private void SoundPlay(AudioClip _clip, float _volume)
	{
		audioLock.clip = _clip;
		audioLock.volume = _volume;
		audioLock.pitch = 1f;
		audioLock.Play();
	}

	private void SoundPlay(AudioClip _clip, float _volume, float _randomMin, float _randomMax)
	{
		audioLock.clip = _clip;
		audioLock.volume = _volume;
		audioLock.pitch = Random.Range(_randomMin, _randomMax);
		audioLock.Play();
	}

	public void CancelLook()
	{
		LookLock(x: false);
	}
}
