using UnityEngine;

[AddComponentMenu("Functions/Transform/Magnet")]
public class Transform_Magnet : MonoBehaviour
{
	public Transform targetObject;

	public Transform transformObject;

	public bool active = true;

	public bool local = true;

	public float speed = 8f;

	public Vector3 position;

	public Vector3 rotation;

	[Header("Noise")]
	public bool _noise;

	public int noiseRandomTime;

	public float noiseMin;

	public float noiseMax;

	private float noise;

	private float noisetmNow;

	private float realSpeed;

	private void Start()
	{
		if (transformObject == null)
		{
			transformObject = base.transform;
		}
		realSpeed = speed;
	}

	private void FixedUpdate()
	{
		if (!active)
		{
			return;
		}
		Quaternion quaternion = Quaternion.Euler(rotation);
		if (targetObject == null)
		{
			if (!local)
			{
				transformObject.position = Vector3.Lerp(transformObject.position, Time.deltaTime * position, Time.deltaTime * realSpeed);
				transformObject.rotation = Quaternion.Lerp(transformObject.rotation, quaternion, Time.deltaTime * realSpeed);
			}
			else
			{
				transformObject.localPosition = Vector3.Lerp(transformObject.localPosition, position, Time.deltaTime * realSpeed);
				transformObject.localRotation = Quaternion.Lerp(transformObject.localRotation, quaternion, Time.deltaTime * realSpeed);
			}
		}
		else if (!local)
		{
			transformObject.position = Vector3.Lerp(transformObject.position, targetObject.position + position, Time.deltaTime * realSpeed);
			transformObject.rotation = Quaternion.Lerp(transformObject.rotation, quaternion, Time.deltaTime * realSpeed);
		}
		else
		{
			transformObject.position = Vector3.Lerp(transformObject.position, targetObject.position + position, Time.deltaTime * realSpeed);
			transformObject.rotation = Quaternion.Lerp(transformObject.rotation, targetObject.rotation * quaternion, Time.deltaTime * realSpeed);
		}
		if (realSpeed != speed)
		{
			realSpeed = Mathf.Lerp(realSpeed, speed, Time.deltaTime * 5f);
		}
	}

	private void Update()
	{
		if (_noise)
		{
			if (Random.Range(0, noiseRandomTime) == 0)
			{
				noisetmNow = Random.Range(1, 40);
			}
			if (noisetmNow > 0f)
			{
				transformObject.position += new Vector3(Random.Range(0f - noise, noise), Random.Range(0f - noise, noise), Random.Range(0f - noise, noise));
				noisetmNow -= 1f;
				noise = Random.Range(noiseMin, noiseMax);
			}
		}
	}

	public void PassComponent(Transform_Magnet obj)
	{
		obj.local = local;
		obj.speed = speed;
		obj.position = position;
		obj.rotation = rotation;
	}

	public void ReTarget(Transform x)
	{
		targetObject = x;
	}

	public void ReTargetSmooth(Transform x)
	{
		targetObject = x;
		realSpeed = 0f;
	}

	public void Activation(bool x)
	{
		active = x;
	}

	public void ActivationParent(bool x)
	{
		active = x;
		base.transform.parent = targetObject;
	}

	private void Reset()
	{
		transformObject = base.transform;
	}
}
