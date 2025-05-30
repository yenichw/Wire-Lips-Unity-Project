using UnityEngine;

public class Player_Teleport : MonoBehaviour
{
	public Vector3 positionPlayer;

	public float rotationPlayer;

	public bool onStart;

	private void Start()
	{
		if (onStart)
		{
			GameObject.FindWithTag("Player").GetComponent<Player>().TeleportFast(positionPlayer, rotationPlayer);
		}
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = new Color(0.3f, 0.5f, 1f, 0.9f);
		Gizmos.DrawCube(positionPlayer + new Vector3(0f, 0.01f, 0f), new Vector3(0.3f, 0.025f, 0.3f));
		Gizmos.DrawLine(positionPlayer, positionPlayer + Vector3.up * 1.5f);
		Gizmos.DrawLine(positionPlayer + Vector3.up * 1.5f, Vector3.up * 1.5f + positionPlayer + new Vector3(Mathf.Cos((0f - rotationPlayer + 90f) * 0.017444445f), 0f, Mathf.Sin((0f - rotationPlayer + 90f) * 0.017444445f)) / 2f);
	}

	public void TeleportPlayer()
	{
		GameObject.FindWithTag("Player").GetComponent<Player>().TeleportFast(positionPlayer, rotationPlayer);
	}

	public void TeleportPlayerPosition()
	{
		GameObject.FindWithTag("Player").transform.position = positionPlayer;
		GameObject.FindWithTag("Player").transform.rotation = Quaternion.Euler(0f, rotationPlayer, 0f);
	}
}
