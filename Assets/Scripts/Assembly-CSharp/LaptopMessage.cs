using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class LaptopMessage : MonoBehaviour
{
	public AudioClip[] keyboardSounds;

	public AudioClip soundGiveMessage;

	public AudioClip soundTakeMessage;

	public AudioSource keyboardAudio;

	public Animator writeFriend;

	public RectTransform positionContent;

	public string fileMessage = "Messages 1";

	public GameObject messageGhost;

	public GameObject messageGhostPhoto;

	public GameObject messageMe;

	public MessageDataLaptop[] messages;

	public Text textKeyboard;

	public int startMessages;

	private int isMessage;

	private bool printText;

	private bool printMe;

	private bool soundMes;

	private float timePrintFriend;

	private float yyPositionContent;

	private float timePress;

	private float posMes;

	private float nextPos;

	private string textNeedPrintMe;

	private void Start()
	{
		nextPos = 70f;
		positionContent.anchoredPosition = new Vector2(0f, -250f);
		yyPositionContent = -400f;
		posMes = -10f;
		if (startMessages != 0)
		{
			for (int i = 0; i < startMessages; i++)
			{
				ApplyMessage();
			}
		}
		soundMes = true;
	}

	private void Update()
	{
		if (printText)
		{
			if (timePrintFriend > 0f)
			{
				timePrintFriend -= Time.deltaTime;
				if (timePrintFriend < 0f)
				{
					timePrintFriend = 0f;
					ApplyMessage();
				}
			}
			if (printMe)
			{
				if (textKeyboard.text == textNeedPrintMe)
				{
					textKeyboard.text = "";
					ApplyMessage();
				}
				else
				{
					timePress += Time.deltaTime * 8f;
					if (timePress > 1f)
					{
						timePress = 0f;
						textKeyboard.text += textNeedPrintMe[textKeyboard.text.Length];
						KeyboardSound();
					}
				}
			}
		}
		positionContent.anchoredPosition = new Vector2(0f, Mathf.Lerp(positionContent.anchoredPosition.y, yyPositionContent, Time.deltaTime * 8f));
	}

	public void NextMessage()
	{
		printText = true;
		if (!messages[isMessage].me)
		{
			writeFriend.SetBool("Write", value: true);
			timePrintFriend = 2f;
			printMe = false;
			return;
		}
		textKeyboard.text = "";
		printMe = true;
		textNeedPrintMe = File.ReadAllLines("Data/Languages/" + GlobalGame.Language + "/" + fileMessage + ".txt")[messages[isMessage].stringNumberFile - 1];
	}

	private void ApplyMessage()
	{
		writeFriend.SetBool("Write", value: false);
		posMes -= nextPos;
		if (messages[isMessage].photo != null)
		{
			nextPos = 340f;
		}
		else
		{
			nextPos = 70f;
		}
		printText = false;
		if (messages[isMessage].next)
		{
			printText = true;
		}
		GameObject gameObject = null;
		if (messages[isMessage].me)
		{
			gameObject = Object.Instantiate(messageMe, base.transform.Find("Viewport/Content").transform);
			gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(-10f, posMes);
			yyPositionContent += 70f;
			if (soundMes)
			{
				keyboardAudio.clip = soundGiveMessage;
				keyboardAudio.pitch = 1f;
				keyboardAudio.Play();
			}
		}
		else
		{
			timePrintFriend = 0f;
			if (messages[isMessage].photo != null)
			{
				gameObject = Object.Instantiate(messageGhostPhoto, base.transform.Find("Viewport/Content").transform);
				yyPositionContent += 340f;
			}
			if (messages[isMessage].photo == null)
			{
				gameObject = Object.Instantiate(messageGhost, base.transform.Find("Viewport/Content").transform);
				yyPositionContent += 70f;
			}
			gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(10f, posMes);
			if (soundMes)
			{
				keyboardAudio.clip = soundTakeMessage;
				keyboardAudio.pitch = 1f;
				keyboardAudio.Play();
			}
		}
		if (messages[isMessage].photo == null)
		{
			gameObject.transform.Find("Dis").GetComponent<Text>().text = File.ReadAllLines("Data/Languages/" + GlobalGame.Language + "/" + fileMessage + ".txt")[messages[isMessage].stringNumberFile - 1];
		}
		messages[isMessage]._event.Invoke();
		isMessage++;
		if (messages[isMessage - 1].next)
		{
			NextMessage();
		}
	}

	private void KeyboardSound()
	{
		keyboardAudio.clip = keyboardSounds[Random.Range(0, keyboardSounds.Length)];
		keyboardAudio.pitch = Random.Range(0.95f, 1.05f);
		keyboardAudio.Play();
	}
}
