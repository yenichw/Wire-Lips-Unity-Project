using UnityEngine;

public class Object_RandomMesh : MonoBehaviour
{
	public GameObject objectRandom;

	public randomMeshMaterial[] Meshes;

	private void Start()
	{
		if (objectRandom == null)
		{
			objectRandom = base.gameObject;
		}
		int num = Random.Range(0, Meshes.Length);
		objectRandom.GetComponent<MeshFilter>().mesh = Meshes[num].mesh;
		objectRandom.GetComponent<MeshRenderer>().material.mainTexture = Meshes[num].textures[Random.Range(0, Meshes[num].textures.Length)];
	}
}
