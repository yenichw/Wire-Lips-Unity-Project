using UnityEngine;
using UnityEngine.Events;

[AddComponentMenu("Functions/Trigger/Trigger Object Event")]
[RequireComponent(typeof(BoxCollider))]
public class Triggers_ObjectEvent : MonoBehaviour
{
	public GameObject _object;

	public bool _every;

	public bool destroyAfter;

	public UnityEvent enterEvent;

	public UnityEvent exitEvent;

	public bool useUpdate;

	private bool active;

	private bool enter;

	private bool col;

	private bool colall;

	private Collider[] colsbox;

	private BoxCollider bx;

	private void Start()
	{
		if (_object == null)
		{
			_object = GameObject.FindWithTag("Player").gameObject;
		}
		bx = GetComponent<BoxCollider>();
		if (useUpdate)
		{
			base.gameObject.layer = 2;
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (useUpdate || active)
		{
			return;
		}
		if (_object != null)
		{
			if (other.gameObject == _object)
			{
				enterEvent.Invoke();
				enter = true;
			}
		}
		else
		{
			enterEvent.Invoke();
		}
		if (!_every)
		{
			active = true;
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (useUpdate || !enter)
		{
			return;
		}
		if (_object != null)
		{
			if (other.gameObject == _object)
			{
				ConsoleMain.ConsolePrint("Trigger Exit");
				exitEvent.Invoke();
				if (destroyAfter)
				{
					Object.Destroy(base.gameObject);
				}
			}
		}
		else
		{
			ConsoleMain.ConsolePrint("Trigger Exit");
			exitEvent.Invoke();
			if (destroyAfter)
			{
				Object.Destroy(base.gameObject);
			}
		}
	}

	private void Update()
	{
		if (!useUpdate)
		{
			return;
		}
		colsbox = Physics.OverlapBox(base.transform.position + bx.center, bx.size / 2f, base.transform.rotation);
		colall = false;
		for (int i = 0; i < colsbox.Length; i++)
		{
			if (colsbox[i].gameObject == _object)
			{
				if (!col)
				{
					ConsoleMain.ConsolePrint("Trigger Enter (" + base.gameObject.name + ")");
					col = true;
					enterEvent.Invoke();
				}
				colall = true;
			}
		}
		if (!colall && col)
		{
			ConsoleMain.ConsolePrint("Trigger Exit (" + base.gameObject.name + ")");
			col = false;
			exitEvent.Invoke();
			if (destroyAfter)
			{
				Object.Destroy(base.gameObject);
			}
		}
	}

	private void Reset()
	{
		base.gameObject.layer = 2;
		GetComponent<BoxCollider>().isTrigger = true;
		base.transform.localScale = Vector3.one;
	}

	[ContextMenu("Clear Events")]
	private void ClearEvents()
	{
		enterEvent = new UnityEvent();
		exitEvent = new UnityEvent();
	}
}
