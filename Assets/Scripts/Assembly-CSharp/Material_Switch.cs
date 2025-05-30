using UnityEngine;

public class Material_Switch : MonoBehaviour
{
	public ObjectMeshMaterial[] objects;

	public void RematerialObject(int x)
	{
		if (objects[x].objRenderer.GetComponent<MeshRenderer>() != null)
		{
			Material[] materials = objects[x].objRenderer.GetComponent<MeshRenderer>().materials;
			materials[objects[x].numberMaterial] = objects[x].materialChange;
			objects[x].objRenderer.GetComponent<MeshRenderer>().materials = materials;
		}
		if (objects[x].objRenderer.GetComponent<SkinnedMeshRenderer>() != null)
		{
			Material[] materials2 = objects[x].objRenderer.GetComponent<SkinnedMeshRenderer>().materials;
			materials2[objects[x].numberMaterial] = objects[x].materialChange;
			objects[x].objRenderer.GetComponent<SkinnedMeshRenderer>().materials = materials2;
		}
	}

	public void RematerialAllObjects()
	{
		for (int i = 0; i < objects.Length; i++)
		{
			if (objects[i].objRenderer.GetComponent<MeshRenderer>() != null)
			{
				Material[] materials = objects[i].objRenderer.GetComponent<MeshRenderer>().materials;
				materials[objects[i].numberMaterial] = objects[i].materialChange;
				objects[i].objRenderer.GetComponent<MeshRenderer>().materials = materials;
			}
			if (objects[i].objRenderer.GetComponent<SkinnedMeshRenderer>() != null)
			{
				Material[] materials2 = objects[i].objRenderer.GetComponent<SkinnedMeshRenderer>().materials;
				materials2[objects[i].numberMaterial] = objects[i].materialChange;
				objects[i].objRenderer.GetComponent<SkinnedMeshRenderer>().materials = materials2;
			}
		}
	}
}
