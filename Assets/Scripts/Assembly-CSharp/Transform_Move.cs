using UnityEngine;

public class Transform_Move : MonoBehaviour
{
	public enum TypeMove
	{
		Forward = 0,
		Right = 1,
		Up = 2,
		None = 3
	}

	public TypeMove typeMove;

	public Vector3 myMove;

	public float speed;

	public bool active;

	public bool SmoothUse;

	public float SmoothSpeed = 5f;

	private Vector3 speedMove;

	private float _spd;

	private void FixedUpdate()
	{
		if (!active)
		{
			return;
		}
		if (!SmoothUse)
		{
			if (typeMove == TypeMove.Forward)
			{
				base.transform.position += base.transform.forward * speed * Time.deltaTime;
			}
			if (typeMove == TypeMove.Right)
			{
				base.transform.position += base.transform.right * speed * Time.deltaTime;
			}
			if (typeMove == TypeMove.Up)
			{
				base.transform.position += base.transform.up * speed * Time.deltaTime;
			}
			base.transform.position += myMove;
			return;
		}
		if (typeMove == TypeMove.Forward)
		{
			base.transform.position += base.transform.forward * _spd * Time.deltaTime;
		}
		if (typeMove == TypeMove.Right)
		{
			base.transform.position += base.transform.right * _spd * Time.deltaTime;
		}
		if (typeMove == TypeMove.Up)
		{
			base.transform.position += base.transform.up * _spd * Time.deltaTime;
		}
		base.transform.position += speedMove;
		speedMove = Vector3.Lerp(speedMove, myMove, SmoothSpeed * Time.deltaTime);
		_spd = Mathf.Lerp(_spd, speed, SmoothSpeed * Time.deltaTime);
	}

	public void Activation(bool x)
	{
		active = x;
	}

	public void ActivationOn()
	{
		active = true;
	}
}
