using UnityEngine;

[AddComponentMenu("Functions/Transform/Hierarchy Parent")]
public class Transform_SetParent : MonoBehaviour
{
	[Header("Start")]
	public bool onStart;

	public GameObject[] objects;

	public Transform parent;

	private void Start()
	{
		if (onStart)
		{
			for (int i = 0; i < objects.Length; i++)
			{
				objects[i].transform.SetParent(parent);
			}
		}
	}

	public void ParentHierarchy(Transform x)
	{
		base.transform.SetParent(x);
	}
}
