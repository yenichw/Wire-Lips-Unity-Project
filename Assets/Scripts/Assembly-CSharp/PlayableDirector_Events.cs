using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;

public class PlayableDirector_Events : MonoBehaviour
{
	public UnityEvent _eventStart;

	public UnityEvent _eventStop;

	[Header("Events")]
	public int[] timeEvent;

	public UnityEvent[] _events;

	private void OnEnable()
	{
		GetComponent<PlayableDirector>().stopped += OnPlayableDirectorStopped;
		if (GetComponent<PlayableDirector>().playOnAwake)
		{
			_eventStart.Invoke();
			for (int i = 0; i < _events.Length; i++)
			{
				StartCoroutine(TimeStartEvent(i));
			}
		}
		else
		{
			GetComponent<PlayableDirector>().played += OnPlayableDirectorPlayed;
		}
	}

	private void OnPlayableDirectorStopped(PlayableDirector aDirector)
	{
		_eventStop.Invoke();
	}

	private void OnPlayableDirectorPlayed(PlayableDirector aDirector)
	{
		_eventStart.Invoke();
		for (int i = 0; i < _events.Length; i++)
		{
			StartCoroutine(TimeStartEvent(i));
		}
	}

	private IEnumerator TimeStartEvent(int num)
	{
		yield return new WaitForSeconds((float)timeEvent[num] / 60f);
		_events[num].Invoke();
	}
}
