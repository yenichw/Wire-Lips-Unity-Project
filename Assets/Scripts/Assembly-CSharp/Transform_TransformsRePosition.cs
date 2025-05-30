using UnityEngine;

[AddComponentMenu("Functions/Transform/Transform Reposition")]
public class Transform_TransformsRePosition : MonoBehaviour
{
	public TransformPosition[] targets;

	public void RePosition()
	{
		for (int i = 0; i < targets.Length; i++)
		{
			if (!targets[i].local)
			{
				targets[i].transform.position = targets[i].position;
				targets[i].transform.rotation = Quaternion.Euler(targets[i].rotation);
			}
			else
			{
				targets[i].transform.localPosition = targets[i].position;
				targets[i].transform.localRotation = Quaternion.Euler(targets[i].rotation);
			}
		}
	}
}
