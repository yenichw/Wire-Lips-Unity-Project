using UnityEngine;
using UnityEngine.Events;

public class Interactive_Animation : MonoBehaviour
{
	public Animator anim;

	public string nameState = "Animation";

	public float speed = 1f;

	public UnityEvent _eventEnd;

	[Header("Information")]
	public float value;

	private float valueNeed;

	private Interactive_Action ia;

	private bool eventDone;

	private void Start()
	{
		ia = GetComponent<Interactive_Action>();
	}

	private void Update()
	{
		if (ia.handIkEndPlayer && !eventDone)
		{
			valueNeed += Time.deltaTime * speed;
			if (value >= 1f)
			{
				eventDone = true;
				ConsoleMain.ConsolePrint("Interactive animation event end:" + base.gameObject.name);
				value = 1f;
				_eventEnd.Invoke();
			}
			anim.Play(nameState, -1, value);
		}
		if (!eventDone)
		{
			value = Mathf.Lerp(value, valueNeed, Time.deltaTime * 8f);
		}
	}

	private void Reset()
	{
		if (GetComponent<Animator>() != null)
		{
			anim = GetComponent<Animator>();
		}
	}
}
