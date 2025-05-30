using UnityEngine;

public class Animation_Hold : MonoBehaviour
{
	[Range(0f, 1f)]
	public float speed = 0.1f;

	private Animator anim;

	private float holdForce;

	private int tm;

	private void Start()
	{
		anim = GetComponent<Animator>();
		anim.speed = 0f;
	}

	private void Update()
	{
		if (tm == 0)
		{
			holdForce = Mathf.Lerp(holdForce, 0f, 0.1f);
		}
		if (tm > 0)
		{
			tm--;
		}
		anim.speed = holdForce;
	}

	public void Hold()
	{
		holdForce = Mathf.Lerp(holdForce, 1f, 0.1f);
		tm = 2;
	}
}
