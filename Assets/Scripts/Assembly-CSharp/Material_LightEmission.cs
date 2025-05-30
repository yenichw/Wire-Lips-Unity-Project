using UnityEngine;

public class Material_LightEmission : MonoBehaviour
{
	public Light lightObject;

	private MeshRenderer rend;

	private void Start()
	{
		rend = GetComponent<MeshRenderer>();
	}

	private void Update()
	{
		float num = lightObject.color.r;
		float num2 = lightObject.color.g;
		float num3 = lightObject.color.b;
		if (lightObject.intensity < 1f)
		{
			num -= 1f - lightObject.intensity;
			num2 -= 1f - lightObject.intensity;
			num3 -= 1f - lightObject.intensity;
		}
		rend.material.SetColor("_EmissionColor", new Color(num, num2, num3, 0f));
		if (!lightObject.gameObject.activeInHierarchy)
		{
			rend.material.SetColor("_EmissionColor", new Color(0f, 0f, 0f, 0f));
		}
	}
}
