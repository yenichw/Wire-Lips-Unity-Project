using UnityEngine;
using UnityEngine.Events;

[AddComponentMenu("Functions/Animation/Animation Function")]
public class Animation_Function : MonoBehaviour
{
	[Header("[EventKey]")]
	public bool activeEvents = true;

	public Animator[] otherAnimators;

	public UnityEvent[] _events;

	private Animator anim;

	private void OnEnable()
	{
		if (otherAnimators.Length == 0)
		{
			anim = GetComponent<Animator>();
		}
	}

	public void BoolOn(string x)
	{
		if (otherAnimators.Length == 0)
		{
			anim.SetBool(x, value: true);
			return;
		}
		for (int i = 0; i < otherAnimators.Length; i++)
		{
			otherAnimators[i].SetBool(x, value: true);
		}
	}

	public void BoolOff(string x)
	{
		if (otherAnimators.Length == 0)
		{
			anim.SetBool(x, value: false);
			return;
		}
		for (int i = 0; i < otherAnimators.Length; i++)
		{
			otherAnimators[i].SetBool(x, value: false);
		}
	}

	public void TriggerClick(string x)
	{
		if (otherAnimators.Length == 0)
		{
			anim.SetTrigger(x);
			return;
		}
		for (int i = 0; i < otherAnimators.Length; i++)
		{
			otherAnimators[i].SetTrigger(x);
		}
	}

	public void BoolSwitch(string x)
	{
		anim.SetBool(x, !anim.GetBool(x));
	}

	public void EventKey(int x)
	{
		if (activeEvents)
		{
			_events[x].Invoke();
		}
	}

	public void reRunAnimator(RuntimeAnimatorController x)
	{
		anim.runtimeAnimatorController = x;
	}

	public void ActivationEvents(bool x)
	{
		activeEvents = x;
	}
}
