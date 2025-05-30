using UnityEngine;

[AddComponentMenu("Functions/Object/Objects Active If Active")]
public class Object_ActiveIfActive : MonoBehaviour
{
	public GameObject ifActiveObject;

	public GameObject[] otherObjects;

	public bool reverse;

	private bool x;

	private void Update()
	{
		if (ifActiveObject.activeInHierarchy && !x)
		{
			x = true;
			for (int i = 0; i < otherObjects.Length; i++)
			{
				if (!reverse)
				{
					otherObjects[i].SetActive(value: true);
				}
				if (reverse)
				{
					otherObjects[i].SetActive(value: false);
				}
			}
		}
		if (ifActiveObject.activeInHierarchy || !x)
		{
			return;
		}
		x = false;
		for (int j = 0; j < otherObjects.Length; j++)
		{
			if (!reverse)
			{
				otherObjects[j].SetActive(value: false);
			}
			if (reverse)
			{
				otherObjects[j].SetActive(value: true);
			}
		}
	}

	public void reReverse(bool Bool)
	{
		reverse = Bool;
	}
}
