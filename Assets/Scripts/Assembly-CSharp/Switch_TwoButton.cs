using UnityEngine;
using UnityEngine.Events;

[AddComponentMenu("Functions/Switch/Switch 2")]
public class Switch_TwoButton : MonoBehaviour
{
	private Animator anim;

	private AudioSource au;

	public bool active;

	public bool dontWork;

	public UnityEvent eventOn;

	public GameObject[] objectsActiveOn;

	public UnityEvent eventOff;

	public GameObject[] objectsDeactiveOff;

	public string boolAnimation;

	private void Awake()
	{
		anim = GetComponent<Animator>();
		au = GetComponent<AudioSource>();
		if (boolAnimation != "")
		{
			anim.SetBool(boolAnimation, active);
		}
	}

	public void ButtonClick()
	{
		active = !active;
		if (au != null)
		{
			au.pitch = Random.Range(0.9f, 1.1f);
			au.Play();
		}
		if (boolAnimation != "")
		{
			anim.SetBool(boolAnimation, active);
		}
		if (!dontWork)
		{
			if (active)
			{
				eventOn.Invoke();
			}
			else
			{
				eventOff.Invoke();
			}
		}
	}

	public void ReactiveAfterDontWork()
	{
		dontWork = false;
		if (active)
		{
			eventOn.Invoke();
			for (int i = 0; i < objectsActiveOn.Length; i++)
			{
				objectsActiveOn[i].SetActive(value: true);
			}
		}
		else
		{
			eventOff.Invoke();
			for (int j = 0; j < objectsDeactiveOff.Length; j++)
			{
				objectsDeactiveOff[j].SetActive(value: false);
			}
		}
	}

	public void DontWork(bool x)
	{
		dontWork = x;
	}

	public void eventVokeOn()
	{
		eventOn.Invoke();
	}

	public void eventVokeOff()
	{
		eventOff.Invoke();
	}

	public void ButtonOff(bool soundUse)
	{
		Awake();
		for (int i = 0; i < objectsDeactiveOff.Length; i++)
		{
			objectsDeactiveOff[i].SetActive(value: false);
		}
		if (au != null && soundUse && active)
		{
			au.pitch = Random.Range(0.9f, 1.1f);
			au.Play();
		}
		active = false;
		if (boolAnimation != "")
		{
			anim.SetBool(boolAnimation, active);
		}
		eventOff.Invoke();
	}

	public void ButtonOn(bool soundUse)
	{
		Awake();
		for (int i = 0; i < objectsActiveOn.Length; i++)
		{
			objectsActiveOn[i].SetActive(value: true);
		}
		if (au != null && soundUse && !active)
		{
			au.pitch = Random.Range(0.9f, 1.1f);
			au.Play();
		}
		active = true;
		if (boolAnimation != "")
		{
			anim.SetBool(boolAnimation, active);
		}
		eventOn.Invoke();
	}
}
