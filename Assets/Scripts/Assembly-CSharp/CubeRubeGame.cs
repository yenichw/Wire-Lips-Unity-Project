using UnityEngine;
using UnityEngine.Events;

public class CubeRubeGame : MonoBehaviour
{
	public UnityEvent eventEndGame;

	public UnityEvent eventStartGame;

	public Animator animatorCubeRotation;

	public Transform transformRotation;

	public AudioSource audioCube;

	public AudioClip[] soundsAction;

	public AudioClip[] soundTip;

	public AudioClip soundNot;

	public ParticleSystem particleBlood;

	[Header("Events")]
	public UnityEvent eventWin1;

	public UnityEvent eventWin2;

	public UnityEvent eventWin3;

	public UnityEvent eventWin4;

	public UnityEvent eventWin5;

	public UnityEvent eventWin6;

	private int[] rotationAction = new int[10];

	private int rotationActionNow;

	private int rotationActionMax = 2;

	private int momentGame;

	private float timeAnim;

	private float timeAnimYes;

	private bool gameMove;

	private float notTime;

	private bool firstGameStart;

	private bool canKeyDown;

	private bool showTip;

	private float timeTip;

	private void Start()
	{
		rotationAction[0] = Random.Range(0, 4);
		rotationAction[1] = Random.Range(0, 4);
		rotationAction[2] = Random.Range(0, 4);
		rotationAction[3] = Random.Range(0, 4);
		rotationAction[4] = Random.Range(0, 4);
		rotationAction[5] = Random.Range(0, 4);
	}

	private void Update()
	{
		if (notTime > 0f)
		{
			notTime -= Time.deltaTime;
			if (notTime <= 0f)
			{
				animatorCubeRotation.SetTrigger("Reset");
				ShowTip();
			}
		}
		if (showTip)
		{
			timeTip += Time.deltaTime;
			if (timeTip >= 1f)
			{
				if (rotationAction[rotationActionNow] == 0)
				{
					transformRotation.localPosition = new Vector3(0.8f, 0f, 0f);
				}
				if (rotationAction[rotationActionNow] == 1)
				{
					transformRotation.localPosition = new Vector3(-0.8f, 0f, 0f);
				}
				if (rotationAction[rotationActionNow] == 2)
				{
					transformRotation.localPosition = new Vector3(0f, 0.8f, 0f);
				}
				if (rotationAction[rotationActionNow] == 3)
				{
					transformRotation.localPosition = new Vector3(0f, -0.8f, 0f);
				}
				SoundPlay(soundTip[Random.Range(0, soundTip.Length)]);
				timeTip = 0f;
				rotationActionNow++;
				if (rotationActionNow >= rotationActionMax)
				{
					showTip = false;
					gameMove = true;
					rotationActionNow = 0;
				}
			}
		}
		transformRotation.localPosition = Vector3.Lerp(transformRotation.localPosition, Vector3.zero, Time.deltaTime * 5f);
		if (timeAnim > 0f)
		{
			timeAnim -= Time.deltaTime;
			if (timeAnim < 0f)
			{
				if (rotationActionNow == rotationActionMax)
				{
					NextPicture();
				}
				timeAnim = 0f;
			}
		}
		if (timeAnimYes > 0f)
		{
			timeAnimYes -= Time.deltaTime;
			if (timeAnimYes < 0f)
			{
				animatorCubeRotation.SetTrigger("Yes");
			}
		}
		if (timeAnim == 0f && Input.GetAxis("Horizontal") == 0f && Input.GetAxis("Vertical") == 0f)
		{
			canKeyDown = true;
		}
		if (!gameMove)
		{
			return;
		}
		if (Input.GetAxis("Horizontal") > 0f && timeAnim == 0f && canKeyDown)
		{
			canKeyDown = false;
			timeAnim = 0.2f;
			if (rotationAction[rotationActionNow] == 0)
			{
				NextAction();
			}
			else
			{
				ResetMoment();
			}
		}
		if (Input.GetAxis("Horizontal") < 0f && timeAnim == 0f && canKeyDown)
		{
			canKeyDown = false;
			timeAnim = 0.2f;
			if (rotationAction[rotationActionNow] == 1)
			{
				NextAction();
			}
			else
			{
				ResetMoment();
			}
		}
		if (Input.GetAxis("Vertical") > 0f && timeAnim == 0f && canKeyDown)
		{
			canKeyDown = false;
			timeAnim = 0.2f;
			if (rotationAction[rotationActionNow] == 2)
			{
				NextAction();
			}
			else
			{
				ResetMoment();
			}
		}
		if (Input.GetAxis("Vertical") < 0f && timeAnim == 0f && canKeyDown)
		{
			canKeyDown = false;
			timeAnim = 0.2f;
			if (rotationAction[rotationActionNow] == 3)
			{
				NextAction();
			}
			else
			{
				ResetMoment();
			}
		}
	}

	public void ShowTip()
	{
		rotationActionNow = 0;
		gameMove = false;
		showTip = true;
		timeTip = 0f;
		if (!firstGameStart)
		{
			firstGameStart = true;
			eventStartGame.Invoke();
		}
	}

	private void NextPicture()
	{
		momentGame++;
		if (momentGame < 6)
		{
			if (momentGame == 1)
			{
				rotationActionMax = 2;
				eventWin1.Invoke();
			}
			if (momentGame == 2)
			{
				rotationActionMax = 3;
				eventWin2.Invoke();
			}
			if (momentGame == 3)
			{
				rotationActionMax = 3;
				eventWin3.Invoke();
			}
			if (momentGame == 4)
			{
				rotationActionMax = 4;
				eventWin4.Invoke();
			}
			if (momentGame == 5)
			{
				rotationActionMax = 4;
				eventWin5.Invoke();
			}
			animatorCubeRotation.SetInteger("Moment", momentGame);
			animatorCubeRotation.SetTrigger("Reset");
			rotationActionNow = 0;
			rotationAction[0] = Random.Range(0, 4);
			rotationAction[1] = Random.Range(0, 4);
			rotationAction[2] = Random.Range(0, 4);
			rotationAction[3] = Random.Range(0, 4);
			rotationAction[4] = Random.Range(0, 4);
			rotationAction[5] = Random.Range(0, 4);
			ShowTip();
		}
		if (momentGame == 6)
		{
			eventWin6.Invoke();
			eventEndGame.Invoke();
		}
	}

	private void ResetMoment()
	{
		gameMove = false;
		notTime = 1f;
		animatorCubeRotation.SetTrigger("Not");
		particleBlood.Play();
		rotationActionNow = 0;
		SoundPlay(soundNot);
	}

	private void NextAction()
	{
		SoundPlay(soundsAction[Random.Range(0, soundsAction.Length)]);
		rotationActionNow++;
		animatorCubeRotation.SetTrigger("NextAction");
		if (rotationActionNow == rotationActionMax)
		{
			gameMove = false;
			timeAnim = 4.5f;
			timeAnimYes = 1f;
		}
	}

	private void SoundPlay(AudioClip ac)
	{
		audioCube.clip = ac;
		audioCube.pitch = Random.Range(0.95f, 1.05f);
		audioCube.Play();
	}
}
