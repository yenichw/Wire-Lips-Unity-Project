using UnityEngine;
using UnityEngine.Events;

[AddComponentMenu("Functions/Event/Start Event")]
public class Events_StartObject : MonoBehaviour
{
	public UnityEvent eventAwake;

	public UnityEvent eventOnEnable;

	public UnityEvent eventStart;

	private void Awake()
	{
		eventAwake.Invoke();
	}

	private void OnEnable()
	{
		eventOnEnable.Invoke();
	}

	private void Start()
	{
		eventStart.Invoke();
	}
}
