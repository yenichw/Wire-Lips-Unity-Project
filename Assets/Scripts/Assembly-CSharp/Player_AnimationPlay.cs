using UnityEngine;

public class Player_AnimationPlay : MonoBehaviour
{
	public Transform target;

	public AnimationClip animationClip;

	public bool IKPlayer = true;

	[Header("Position")]
	[Space(5f)]
	public bool usePosition = true;

	public Vector3 positionPlayer;

	public float backDistance;

	public float distanceNeed;

	[Header("Rotation")]
	[Space(5f)]
	public bool useRotation = true;

	public bool rotationTarget = true;

	[Range(0f, 360f)]
	public float rotationPlayer;

	[Space(5f)]
	public TimePointBool[] events;

	private void Start()
	{
		if (target == null)
		{
			target = base.transform;
		}
	}

	public void AnimationPlay()
	{
		Vector3 pos = positionPlayer;
		Vector3 vector = new Vector3(target.position.x, 0f, target.position.z);
		if (usePosition)
		{
			if (backDistance != 0f)
			{
				pos = ((!(Vector3.Distance(vector, GameObject.FindWithTag("Player").transform.position) < backDistance)) ? GameObject.FindWithTag("Player").transform.position : (vector + Vector3.Normalize(GameObject.FindWithTag("Player").transform.position - vector) * backDistance));
			}
			if (distanceNeed != 0f)
			{
				pos = vector + Vector3.Normalize(GameObject.FindWithTag("Player").transform.position - vector) * distanceNeed;
			}
		}
		else
		{
			pos = GameObject.FindWithTag("Player").transform.position;
		}
		float y = rotationPlayer;
		if (useRotation)
		{
			if (rotationTarget)
			{
				y = Quaternion.LookRotation(new Vector3(target.position.x, 0f, target.position.z) - GameObject.FindWithTag("Player").transform.position, Vector3.up).eulerAngles.y;
			}
		}
		else
		{
			y = GameObject.FindWithTag("Player").transform.rotation.eulerAngles.y;
		}
		GameObject.FindWithTag("Player").gameObject.GetComponent<Player>().AnimationPlayPerson(animationClip, pos, y, events, IKPlayer);
	}

	private void OnDrawGizmosSelected()
	{
		if (target == null)
		{
			target = base.transform;
		}
		Vector3 vector = positionPlayer;
		Vector3 vector2 = new Vector3(target.position.x, 0f, target.position.z);
		Gizmos.color = new Color(0.3f, 0.5f, 1f, 0.9f);
		if (backDistance != 0f)
		{
			Gizmos.DrawLine(vector2, vector2 + Vector3.up * 1.5f);
			if (Vector3.Distance(vector2, vector) < backDistance)
			{
				vector = vector2 + Vector3.Normalize(vector - vector2) * backDistance;
			}
		}
		if (distanceNeed != 0f)
		{
			Gizmos.DrawLine(vector2, vector2 + Vector3.up * 1.5f);
			vector = vector2 + Vector3.Normalize(vector - vector2) * distanceNeed;
		}
		Gizmos.DrawCube(vector + new Vector3(0f, 0.01f, 0f), new Vector3(0.3f, 0.025f, 0.3f));
		Gizmos.DrawLine(vector, vector + Vector3.up * 1.5f);
		Gizmos.color = new Color(1f, 0f, 0f, 1f);
		if (!rotationTarget)
		{
			Gizmos.DrawLine(vector + Vector3.up * 1.5f, Vector3.up * 1.5f + vector + new Vector3(Mathf.Cos((0f - rotationPlayer + 90f) * 0.017444445f), 0f, Mathf.Sin((0f - rotationPlayer + 90f) * 0.017444445f)) / 2f);
			return;
		}
		float y = Quaternion.LookRotation(new Vector3(target.position.x, 0f, target.position.z) - vector, Vector3.up).eulerAngles.y;
		Gizmos.DrawLine(vector + Vector3.up * 1.5f, Vector3.up * 1.5f + vector + new Vector3(Mathf.Cos((0f - y + 90f) * 0.017444445f), 0f, Mathf.Sin((0f - y + 90f) * 0.017444445f)) / 2f);
	}

	private void Reset()
	{
		target = base.transform;
		positionPlayer = new Vector3(base.transform.position.x, 0f, base.transform.position.z);
	}
}
