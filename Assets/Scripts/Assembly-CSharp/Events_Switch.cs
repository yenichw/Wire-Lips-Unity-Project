using UnityEngine;
using UnityEngine.Events;

public class Events_Switch : MonoBehaviour
{
	public AudioClip[] sounds;

	public string animBool = "On";

	public bool On = true;

	public bool dontWork;

	public bool startEvents;

	public UnityEvent _eventOn;

	public UnityEvent _eventOff;

	private Animator anim;

	private AudioSource aud;

	private void Start()
	{
		aud = GetComponent<AudioSource>();
		anim = GetComponent<Animator>();
		anim.SetBool(animBool, On);
		if (On)
		{
			anim.Play("On", -1, 1f);
			if (startEvents)
			{
				_eventOn.Invoke();
			}
		}
		else
		{
			anim.Play("Off", -1, 1f);
			if (startEvents)
			{
				_eventOff.Invoke();
			}
		}
	}

	public void Switch()
	{
		On = !On;
		anim.SetBool(animBool, On);
		if (On)
		{
			_eventOn.Invoke();
		}
		else
		{
			_eventOff.Invoke();
		}
		aud.clip = sounds[Random.Range(0, sounds.Length)];
		aud.pitch = Random.Range(0.95f, 1.05f);
		aud.Play();
		ConsoleMain.ConsolePrint("Switch (" + On + "):" + base.gameObject.name);
	}

	public void DontWork(bool x)
	{
		dontWork = x;
	}

	public void SwitchBool(bool x)
	{
		anim = GetComponent<Animator>();
		On = x;
		anim.SetBool(animBool, x);
		if (On)
		{
			_eventOn.Invoke();
		}
		else
		{
			_eventOff.Invoke();
		}
	}
}
