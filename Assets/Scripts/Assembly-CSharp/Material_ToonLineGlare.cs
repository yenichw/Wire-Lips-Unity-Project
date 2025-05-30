using UnityEngine;

[AddComponentMenu("Functions/Material/Material glare animation alpha")]
public class Material_ToonLineGlare : MonoBehaviour
{
	public AnimationCurve alpha;

	public bool active = true;

	private MeshRenderer rend;

	private Color clr;

	private void Start()
	{
		rend = GetComponent<MeshRenderer>();
		alpha.preWrapMode = WrapMode.PingPong;
		alpha.postWrapMode = WrapMode.PingPong;
		clr = rend.material.GetColor("_OutlineColor");
	}

	private void Update()
	{
		if (active)
		{
			rend.material.SetColor("_OutlineColor", new Vector4(clr.r, clr.g, clr.b, alpha.Evaluate(Time.time)));
		}
	}

	public void Play()
	{
		active = true;
	}

	public void Stop()
	{
		active = false;
	}
}
