using UnityEngine;
using UnityEngine.Events;

[AddComponentMenu("Functions/Trigger/Trigger Hold Event")]
[RequireComponent(typeof(BoxCollider))]
public class Triggers_HoldEvent : MonoBehaviour
{
	public GameObject colliderObject;

	public UnityEvent Enter;

	public UnityEvent Exit;

	[Space(10f)]
	public UnityEvent Inside;

	public UnityEvent Out;

	public Collider[] colsbox;

	private BoxCollider bx;

	private bool col;

	private bool colall;

	private void Start()
	{
		bx = GetComponent<BoxCollider>();
		if (colliderObject == null)
		{
			colliderObject = GameObject.FindWithTag("Player").gameObject;
		}
	}

	private void Update()
	{
		colsbox = Physics.OverlapBox(base.transform.position + bx.center, bx.size / 2f, base.transform.rotation);
		colall = false;
		for (int i = 0; i < colsbox.Length; i++)
		{
			if (colsbox[i].gameObject == colliderObject)
			{
				if (!col)
				{
					col = true;
					Enter.Invoke();
				}
				colall = true;
				Inside.Invoke();
			}
		}
		if (!colall)
		{
			if (col)
			{
				col = false;
				Exit.Invoke();
			}
			Out.Invoke();
		}
	}
}
