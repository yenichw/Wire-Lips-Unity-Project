using UnityEngine;

public class Material_OffsetMove : MonoBehaviour
{
	public bool active;

	public GameObject objectRenderer;

	public int numberMaterial;

	public Vector2 speed;

	public string nameTextureShader = "_MainTex";

	private Vector2 position;

	private void Update()
	{
		if (active)
		{
			position += speed * Time.deltaTime;
			if (objectRenderer.GetComponent<MeshRenderer>() != null)
			{
				objectRenderer.GetComponent<MeshRenderer>().materials[numberMaterial].SetTextureOffset(nameTextureShader, position);
			}
			if (objectRenderer.GetComponent<SkinnedMeshRenderer>() != null)
			{
				objectRenderer.GetComponent<SkinnedMeshRenderer>().materials[numberMaterial].SetTextureOffset(nameTextureShader, position);
			}
		}
	}

	public void Activation(bool x)
	{
		active = x;
	}
}
