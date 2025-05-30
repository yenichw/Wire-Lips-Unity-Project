using RootMotion.FinalIK;
using UnityEngine;

public class IK_HeadRandomLook : MonoBehaviour
{
	public bool fixTransform;

	public bool active = true;

	public SkinnedMeshRenderer bodyBlink;

	public string nameBlink = "Blink";

	[Header("Time random")]
	public float speed = 0.0025f;

	public float speedMax = 0.15f;

	[Range(0f, 8f)]
	public float timeHeadMax = 1f;

	public float timeHeadMin = -3f;

	public float headRotationHorizontal;

	public float headRotationVertical = 0.05f;

	[Header("IK")]
	public LookAtIK IK_eyes;

	public Transform transformEyes;

	public LookAtIK IK_head;

	public Transform transformHead;

	private float tmblink;

	private float blink;

	private float tmEyes;

	private float tmHead;

	private float spdHead;

	private Vector3 posEyes;

	private Vector3 posHead;

	private Transform lookOnly;

	private void Start()
	{
		posEyes = new Vector3(Random.Range(-0.25f, 0.25f), Random.Range(-0.25f, 0.25f), Random.Range(-0.25f, 0.25f));
		if (transformHead != null)
		{
			transformHead.position = base.transform.position + base.transform.forward * 3f + new Vector3(Random.Range(0f - headRotationHorizontal, headRotationHorizontal), Random.Range(1.3f - headRotationVertical, 2.1f + headRotationVertical), Random.Range(0f, 2f));
			if (!fixTransform)
			{
				posHead = transformHead.position;
			}
			else
			{
				posHead = transformHead.localPosition;
			}
		}
	}

	private void Update()
	{
		if (nameBlink != "" && bodyBlink != null)
		{
			bodyBlink.SetBlendShapeWeight(bodyBlink.sharedMesh.GetBlendShapeIndex(nameBlink), blink);
			tmblink += Time.deltaTime;
			if (tmblink > 5f)
			{
				blink += Time.deltaTime * 1000f;
				int num = Random.Range(0, 100);
				if (blink > 100f)
				{
					tmblink = Random.Range(-2.5f, 3.5f);
					if (num < 70)
					{
						EyesPos();
					}
				}
			}
			if (tmblink < 5f && blink > 0f)
			{
				blink = Mathf.Lerp(blink, 0f, 0.25f);
			}
		}
		if (transformHead != null)
		{
			if (lookOnly != null)
			{
				transformHead.position = Vector3.Lerp(transformHead.position, lookOnly.position, spdHead);
			}
			else
			{
				if (!fixTransform)
				{
					transformHead.position = Vector3.Lerp(transformHead.position, posHead, spdHead);
				}
				else
				{
					transformHead.localPosition = Vector3.Lerp(transformHead.localPosition, posHead, spdHead);
				}
				tmHead += Time.deltaTime;
				if (tmHead > 8f)
				{
					tmHead = Random.Range(timeHeadMin, timeHeadMax);
					if (!fixTransform)
					{
						posHead = base.transform.position + base.transform.forward * 3f + base.transform.forward * Random.Range(0f, 2f) + base.transform.right * Random.Range(0f - headRotationHorizontal, headRotationHorizontal) + base.transform.up * Random.Range(1.3f - headRotationVertical, 2.1f + headRotationVertical);
					}
					else
					{
						posHead = new Vector3(Random.Range(0f - headRotationHorizontal, headRotationHorizontal), Random.Range(1.3f - headRotationVertical, 2.1f + headRotationVertical), 2f + Random.Range(0f, 3f));
					}
					spdHead = 0f;
					if (transformEyes != null)
					{
						EyesPos();
					}
				}
			}
			spdHead = Mathf.Lerp(spdHead, speedMax, speed);
		}
		if (!(transformEyes != null))
		{
			return;
		}
		if (lookOnly == null)
		{
			transformEyes.localPosition = Vector3.Lerp(transformEyes.localPosition, posEyes, 0.2f);
			tmEyes += Time.deltaTime;
			if (tmEyes > 5f)
			{
				EyesPos();
			}
		}
		else
		{
			transformEyes.localPosition = Vector3.Lerp(transformEyes.localPosition, Vector3.zero, 0.2f);
		}
	}

	private void EyesPos()
	{
		tmEyes = Random.Range(1.5f, 4.5f);
		posEyes = new Vector3(Random.Range(-0.2f, 0.2f), Random.Range(-0.3f, 0.3f), Random.Range(-0.25f, 0.25f));
	}

	public void Activation(bool _active)
	{
		active = _active;
	}

	public void LookTarget(Transform _lookTarget)
	{
	}

	private void Reset()
	{
		Component[] componentsInChildren = base.transform.GetComponentsInChildren(typeof(Transform));
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			if (componentsInChildren[i].gameObject.name == "Body")
			{
				bodyBlink = componentsInChildren[i].gameObject.GetComponent<SkinnedMeshRenderer>();
			}
		}
	}

	public void LookOnly(Transform _transform)
	{
		lookOnly = _transform;
	}

	public void LookDrop()
	{
		if (lookOnly != null)
		{
			posHead = lookOnly.position;
			posEyes = Vector3.zero;
			lookOnly = null;
		}
	}
}
