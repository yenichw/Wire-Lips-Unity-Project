using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

[AddComponentMenu("Functions/AI/AI move")]
[RequireComponent(typeof(NavMeshAgent))]
public class AI_Move : MonoBehaviour
{
	public bool active;

	public Transform target;

	public string boolWalk;

	[Header("Events")]
	public float distance;

	public UnityEvent nearEnter;

	public UnityEvent near;

	private Animator anim;

	private NavMeshAgent agent;

	private bool _nearEnter;

	private bool pointsGO;

	private bool angleGO;

	private Vector3[] pointsMove;

	private UnityEvent eventsEndPoints;

	private int iPoint;

	private float angleTurn;

	private float distanceendevent;

	private void Start()
	{
		anim = GetComponent<Animator>();
		agent = GetComponent<NavMeshAgent>();
	}

	private void Update()
	{
		if (active)
		{
			if (Vector3.Distance(base.transform.position, target.position) < agent.radius + distance)
			{
				if (boolWalk != "")
				{
					anim.SetBool(boolWalk, value: false);
				}
				if (!_nearEnter)
				{
					_nearEnter = true;
					agent.SetDestination(base.transform.position);
					nearEnter.Invoke();
				}
				near.Invoke();
			}
			else
			{
				agent.SetDestination(target.position);
				if (boolWalk != "")
				{
					anim.SetBool(boolWalk, value: true);
				}
				_nearEnter = false;
			}
		}
		if (pointsGO)
		{
			if (Vector3.Distance(base.transform.position, pointsMove[iPoint]) < agent.radius + distance)
			{
				iPoint++;
				if (iPoint == pointsMove.Length - 1)
				{
					distance = distanceendevent;
				}
				if (iPoint == pointsMove.Length)
				{
					pointsGO = false;
					eventsEndPoints.Invoke();
				}
			}
			if (pointsGO)
			{
				agent.SetDestination(pointsMove[iPoint]);
			}
		}
		if (angleGO)
		{
			base.transform.rotation = Quaternion.Lerp(base.transform.rotation, Quaternion.Euler(0f, angleTurn, 0f), 0.1f);
			if (Quaternion.Angle(base.transform.rotation, Quaternion.Euler(0f, angleTurn, 0f)) < 0.5f)
			{
				angleGO = false;
			}
		}
	}

	public void Activation(bool x)
	{
		active = x;
		if (!x)
		{
			if (boolWalk != "")
			{
				anim.SetBool(boolWalk, value: false);
			}
			agent.SetDestination(base.transform.position);
		}
	}

	public void Turn(float angle)
	{
		angleTurn = angle;
		angleGO = true;
	}

	public void GoPoints(Vector3[] points, float _distance, UnityEvent _evntEnd, float _distanceend)
	{
		pointsMove = points;
		eventsEndPoints = _evntEnd;
		distance = _distance;
		iPoint = 0;
		pointsGO = true;
		angleGO = false;
		distanceendevent = _distanceend;
	}
}
