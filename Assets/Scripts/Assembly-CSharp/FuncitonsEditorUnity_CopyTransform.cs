using UnityEngine;

public class FuncitonsEditorUnity_CopyTransform : MonoBehaviour
{
	public bool copyTransform;

	private Component[] obj;

	private Quaternion[] copyRotation = new Quaternion[500];

	private Vector3[] copyPosition = new Vector3[500];

	private Quaternion[] startRotation = new Quaternion[500];

	private Vector3[] startPosition = new Vector3[500];

	private void Reset()
	{
		Component[] componentsInChildren = base.gameObject.GetComponentsInChildren<Transform>();
		obj = componentsInChildren;
		for (int i = 0; i < obj.Length; i++)
		{
			if (obj[i] != null)
			{
				startRotation[i] = obj[i].transform.localRotation;
				startPosition[i] = obj[i].transform.localPosition;
			}
		}
	}

	[ContextMenu("Reset Transforms")]
	private void ResetTransforms()
	{
		for (int i = 0; i < obj.Length; i++)
		{
			if (obj[i] != null)
			{
				obj[i].transform.localRotation = startRotation[i];
				obj[i].transform.localPosition = startPosition[i];
			}
		}
	}

	[ContextMenu("Copy Transforms")]
	private void CopyTransforms()
	{
		for (int i = 0; i < obj.Length; i++)
		{
			if (obj[i] != null)
			{
				copyRotation[i] = obj[i].transform.localRotation;
				copyPosition[i] = obj[i].transform.localPosition;
			}
		}
		copyTransform = true;
	}

	[ContextMenu("Paste Transforms")]
	private void PasteTransforms()
	{
		if (!copyTransform)
		{
			return;
		}
		for (int i = 0; i < obj.Length; i++)
		{
			if (obj[i] != null)
			{
				obj[i].transform.localRotation = copyRotation[i];
				obj[i].transform.localPosition = copyPosition[i];
			}
		}
	}
}
