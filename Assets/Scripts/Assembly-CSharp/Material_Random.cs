using UnityEngine;

[AddComponentMenu("Functions/Material/Material random")]
public class Material_Random : MonoBehaviour
{
	public Texture2D[] textures;

	public Color[] colors;

	[Header("Smoothness")]
	public bool use;

	[Range(0f, 1f)]
	public float min;

	[Range(0f, 1f)]
	public float max;

	private Renderer rend;

	private void Start()
	{
		rend = GetComponent<Renderer>();
		if (textures.Length != 0)
		{
			rend.material.mainTexture = textures[Random.Range(0, textures.Length)];
		}
		if (colors.Length != 0)
		{
			rend.material.color = colors[Random.Range(0, colors.Length)];
		}
		if (use)
		{
			rend.material.SetFloat("_Glossiness", Random.Range(min, max));
		}
		Object.Destroy(this);
	}
}
