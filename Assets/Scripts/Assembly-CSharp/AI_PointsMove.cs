using UnityEngine;
using UnityEngine.Events;

public class AI_PointsMove : MonoBehaviour
{
	public Vector3[] points;

	public AI_Move AI_object;

	public float distance;

	public float distanceEnd;

	public UnityEvent eventsEndPoints;

	public void Go()
	{
		if (distanceEnd == 0f)
		{
			distanceEnd = distance;
		}
		AI_object.GoPoints(points, distance, eventsEndPoints, distanceEnd);
	}

	public void GoOtherAI(AI_Move target)
	{
		if (distanceEnd == 0f)
		{
			distanceEnd = distance;
		}
		target.GoPoints(points, distance, eventsEndPoints, distanceEnd);
	}
}
