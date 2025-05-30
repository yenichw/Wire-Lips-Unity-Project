using UnityEngine;

public class Mesh_MeshBonesArmatureCorrect : MonoBehaviour
{
	public SkinnedMeshRenderer rend;

	public GameObject meshObject;

	public Transform parent;

	public Transform armatureParent;

	public Transform[] qwe;

	public Transform[] trns;

	private void Reset()
	{
		parent = base.transform.parent;
		armatureParent = base.transform.parent.Find("Armature").transform;
		meshObject = base.transform.Find("Body").gameObject;
		meshObject.transform.parent = parent;
		rend = meshObject.GetComponent<SkinnedMeshRenderer>();
		trns = rend.bones;
		Object[] componentsInChildren = armatureParent.GetComponentsInChildren(typeof(Transform));
		Object[] array = componentsInChildren;
		qwe = new Transform[400];
		for (int i = 0; i < array.Length; i++)
		{
			qwe[i] = array[i] as Transform;
		}
		for (int j = 0; j < trns.Length; j++)
		{
			for (int k = 0; k < qwe.Length; k++)
			{
				if (qwe[k] != null && trns[j].gameObject.name == qwe[k].gameObject.name)
				{
					trns[j] = qwe[k];
				}
			}
		}
		rend.bones = trns;
		Debug.Log("Object can be destroy.");
	}
}
