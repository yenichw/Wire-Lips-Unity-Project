using UnityEngine;

public class Animation_InputKeyTime : MonoBehaviour
{
	public bool active = true;

	public string stateName = "Input";

	public float speed = 1f;

	public bool vertical = true;

	public bool horizontal;

	private Animator anim;

	private float value;

	private void Start()
	{
		anim = GetComponent<Animator>();
		anim.speed = 0f;
	}

	private void Update()
	{
		if (!active)
		{
			return;
		}
		if (vertical)
		{
			if (Input.GetAxis("Vertical") < 0f)
			{
				value += Time.deltaTime * speed;
			}
			if (Input.GetAxis("Vertical") > 0f)
			{
				value -= Time.deltaTime * speed;
			}
		}
		if (horizontal)
		{
			if (Input.GetAxis("Horizontal") < 0f)
			{
				value += Time.deltaTime * speed;
			}
			if (Input.GetAxis("Horizontal") > 0f)
			{
				value -= Time.deltaTime * speed;
			}
		}
		if (value > 1f)
		{
			value = 1f;
		}
		if (value < 0f)
		{
			value = 0f;
		}
		anim.Play(stateName, -1, value);
	}

	public void Activation(bool x)
	{
		active = x;
	}
}
