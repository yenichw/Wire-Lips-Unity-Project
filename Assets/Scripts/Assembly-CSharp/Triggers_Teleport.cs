using UnityEngine;

[AddComponentMenu("Functions/Trigger/Trigger Teleport")]
public class Triggers_Teleport : MonoBehaviour
{
	public Vector3 positionTeleport;

	[Space(5f)]
	public bool useRotation;

	public Vector3 rotation;

	[Space(15f)]
	public GameObject _object;

	private void Start()
	{
		if (_object == null)
		{
			_object = GameObject.FindWithTag("Player").gameObject;
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject == _object)
		{
			_object.transform.position = positionTeleport;
			if (useRotation)
			{
				_object.transform.rotation = Quaternion.Euler(rotation);
			}
		}
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = new Color(0.3f, 0.5f, 1f, 0.9f);
		Gizmos.DrawCube(positionTeleport + new Vector3(0f, 0.01f, 0f), new Vector3(0.3f, 0.025f, 0.3f));
		Gizmos.DrawLine(positionTeleport, positionTeleport + Vector3.up * 1.5f);
	}
}
