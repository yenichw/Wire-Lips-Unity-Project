using UnityEngine;

public class Material_AlphaDestroy : MonoBehaviour
{
	private MeshRenderer rend;

	public float speed = 0.005f;

	public bool coming;

	public float speedComing = 0.1f;

	public int timeStop;

	public bool autoDestroy;

	private bool dest;

	private void Start()
	{
		rend = GetComponent<MeshRenderer>();
		if (coming)
		{
			rend.material.color = new Vector4(rend.material.color.r, rend.material.color.g, rend.material.color.b, 0f);
		}
		if (autoDestroy)
		{
			dest = true;
		}
	}

	private void FixedUpdate()
	{
		if (!coming)
		{
			if (timeStop > 0)
			{
				timeStop--;
			}
			if (timeStop == 0 && dest)
			{
				rend.material.color = new Vector4(rend.material.color.r, rend.material.color.g, rend.material.color.b, rend.material.color.a - speed);
				if (rend.material.color.a <= 0f)
				{
					Object.Destroy(base.gameObject);
				}
			}
		}
		else
		{
			rend.material.color = new Vector4(rend.material.color.r, rend.material.color.g, rend.material.color.b, rend.material.color.a + speedComing);
			if (rend.material.color.a >= 1f)
			{
				coming = false;
			}
		}
	}

	public void DestroyGo()
	{
		dest = true;
	}
}
