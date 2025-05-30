using UnityEngine;

[AddComponentMenu("Functions/Transform/Transform move points")]
public class Transform_MovePoints : MonoBehaviour
{
	public Vector3[] points = new Vector3[2];

	public bool local = true;

	public bool lerp = true;

	public float speed = 1f;

	public int iP;

	private Vector3 positionLerp;

	private void Update()
	{
		if (lerp)
		{
			if (local)
			{
				positionLerp = Vector3.MoveTowards(positionLerp, points[iP], Time.deltaTime * speed);
				if (Vector3.Distance(positionLerp, points[iP]) == 0f)
				{
					Next();
				}
			}
			else
			{
				positionLerp = Vector3.MoveTowards(positionLerp, points[iP], Time.deltaTime * speed);
				if (Vector3.Distance(positionLerp, points[iP]) == 0f)
				{
					Next();
				}
			}
			base.transform.localPosition = Vector3.Lerp(base.transform.localPosition, positionLerp, Time.deltaTime * (speed * 4f));
		}
		else if (local)
		{
			base.transform.localPosition = Vector3.MoveTowards(base.transform.localPosition, points[iP], Time.deltaTime * speed);
			if (Vector3.Distance(base.transform.localPosition, points[iP]) == 0f)
			{
				Next();
			}
		}
		else
		{
			base.transform.position = Vector3.MoveTowards(base.transform.position, points[iP], Time.deltaTime * speed);
			if (Vector3.Distance(base.transform.position, points[iP]) == 0f)
			{
				Next();
			}
		}
	}

	private void Next()
	{
		iP++;
		if (iP > points.Length - 1)
		{
			iP = 0;
		}
	}

	private void OnDrawGizmosSelected()
	{
		if (points.Length == 0)
		{
			return;
		}
		if (!local)
		{
			for (int i = 0; i < points.Length; i++)
			{
				if (i > 0)
				{
					Gizmos.DrawLine(points[i - 1], points[i]);
				}
				Gizmos.DrawSphere(points[i], 0.025f);
			}
			return;
		}
		for (int j = 0; j < points.Length; j++)
		{
			if (j > 0)
			{
				Gizmos.DrawLine(base.transform.parent.position + points[j - 1], base.transform.parent.position + points[j]);
			}
			Gizmos.DrawSphere(points[0] + points[j], 0.025f);
		}
	}
}
