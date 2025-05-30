using UnityEngine;

public class FurnitureItem : MonoBehaviour
{
	public Vector3 rotationStart;

	public float yFloor;

	public bool randomXrotate;

	public bool randomYrotate;

	public bool randomZrotate;

	public Vector3[] rotates;

	public Vector3 sizeItem = new Vector3(0.1f, 0.1f, 0.1f);

	private void Start()
	{
		if (rotates.Length == 0)
		{
			if (randomXrotate)
			{
				rotationStart = new Vector3(Random.Range(0f, 360f), rotationStart.y, rotationStart.z);
			}
			if (randomYrotate)
			{
				rotationStart = new Vector3(rotationStart.x, Random.Range(0f, 360f), rotationStart.z);
			}
			if (randomZrotate)
			{
				rotationStart = new Vector3(rotationStart.x, rotationStart.y, Random.Range(0f, 360f));
			}
			base.transform.rotation = Quaternion.Euler(rotationStart);
		}
		else
		{
			int num = Random.Range(0, rotates.Length);
			base.transform.rotation = Quaternion.Euler(rotates[num]);
		}
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = new Color(0.4f, 0.5f, 1f, 0.5f);
		Gizmos.DrawCube(base.transform.position, sizeItem);
		Gizmos.color = new Color(1f, 0f, 0f, 0.8f);
		Gizmos.DrawCube(base.transform.position - new Vector3(0f, 0.0025f + yFloor, 0f), new Vector3(sizeItem.x, 0.005f, sizeItem.z));
	}

	private void Reset()
	{
		rotationStart = base.transform.eulerAngles;
	}
}
