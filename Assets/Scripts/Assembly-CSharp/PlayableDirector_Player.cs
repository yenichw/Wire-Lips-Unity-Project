using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;

public class PlayableDirector_Player : MonoBehaviour
{
	public UnityEvent _eventStart;

	public UnityEvent _eventStop;

	[Header("Player")]
	public bool dontTeleportPlayer;

	public Vector3 positionPlayer;

	[Range(0f, 360f)]
	public float rotation;

	[Header("Animaiton")]
	public bool noBlackScreenStart = true;

	public bool noBlackScreenEnd = true;

	[Header("Events")]
	public int[] timeEvent;

	public UnityEvent[] _events;

	private bool playTimeAnimation;

	private float timeAnimation;

	public double timeAnimationEnd;

	[HideInInspector]
	public UnityEvent _evNull;

	private void OnEnable()
	{
		GetComponent<PlayableDirector>().stopped += OnPlayableDirectorStopped;
		if (GetComponent<PlayableDirector>().extrapolationMode != DirectorWrapMode.None)
		{
			noBlackScreenEnd = false;
		}
		if (GetComponent<PlayableDirector>().playOnAwake && !noBlackScreenStart)
		{
			GameObject.FindWithTag("Player").GetComponent<Player>().AnimationPlayablePlay();
			_eventStart.Invoke();
			for (int i = 0; i < _events.Length; i++)
			{
				StartCoroutine(TimeStartEvent(i));
			}
			if (noBlackScreenEnd)
			{
				playTimeAnimation = true;
				timeAnimation = 0f;
			}
		}
		if (noBlackScreenStart)
		{
			UnityEvent evNull = _evNull;
			evNull.AddListener(PlayAbleStart);
			GameObject.FindWithTag("Player").GetComponent<Player>().BSAnim(evNull);
		}
		if (!GetComponent<PlayableDirector>().playOnAwake || noBlackScreenStart)
		{
			GetComponent<PlayableDirector>().played += OnPlayableDirectorPlayed;
		}
		timeAnimationEnd = GetComponent<PlayableDirector>().duration;
	}

	private void OnPlayableDirectorStopped(PlayableDirector aDirector)
	{
		if (!dontTeleportPlayer && GameObject.FindWithTag("Player") != null)
		{
			GameObject.FindWithTag("Player").GetComponent<Player>().AnimationPlayableStop(positionPlayer, rotation);
			GameObject.FindWithTag("Player").GetComponent<Player>().BlendShapeSetStress();
		}
		_eventStop.Invoke();
		playTimeAnimation = false;
	}

	private void OnPlayableDirectorPlayed(PlayableDirector aDirector)
	{
		GameObject.FindWithTag("Player").GetComponent<Player>().AnimationPlayablePlay();
		_eventStart.Invoke();
		for (int i = 0; i < _events.Length; i++)
		{
			StartCoroutine(TimeStartEvent(i));
		}
		if (noBlackScreenEnd)
		{
			playTimeAnimation = true;
			timeAnimation = 0f;
		}
	}

	private void Update()
	{
		if (playTimeAnimation)
		{
			timeAnimation += Time.deltaTime;
			if ((double)timeAnimation > timeAnimationEnd - 0.6000000238418579)
			{
				playTimeAnimation = false;
				UnityEvent evNull = _evNull;
				evNull.AddListener(PlayAbleEnd);
				GameObject.FindWithTag("Player").GetComponent<Player>().BSAnim(evNull);
			}
		}
	}

	public void PlayAbleStart()
	{
		GetComponent<PlayableDirector>().Play();
	}

	public void PlayAbleStartBlackScreen()
	{
		UnityEvent evNull = _evNull;
		evNull.AddListener(PlayAbleStart);
		GameObject.FindWithTag("Player").GetComponent<Player>().BSAnim(evNull);
	}

	public void PlayAbleEnd()
	{
		GetComponent<PlayableDirector>().Stop();
	}

	public void PlayAbleEndBlackScreen()
	{
		UnityEvent evNull = _evNull;
		evNull.AddListener(PlayAbleEnd);
		GameObject.FindWithTag("Player").GetComponent<Player>().BSAnim(evNull);
	}

	private IEnumerator TimeStartEvent(int num)
	{
		yield return new WaitForSeconds((float)timeEvent[num] / 60f);
		_events[num].Invoke();
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = new Color(0.3f, 0.5f, 1f, 0.9f);
		Gizmos.DrawCube(positionPlayer + new Vector3(0f, 0.01f, 0f), new Vector3(0.3f, 0.025f, 0.3f));
		Gizmos.DrawLine(positionPlayer, positionPlayer + Vector3.up * 1.5f);
		Gizmos.DrawLine(positionPlayer + Vector3.up * 1.5f, Vector3.up * 1.5f + positionPlayer + new Vector3(Mathf.Cos((0f - rotation + 90f) * 0.017444445f), 0f, Mathf.Sin((0f - rotation + 90f) * 0.017444445f)) / 2f);
	}
}
