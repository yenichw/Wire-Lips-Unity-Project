using UnityEngine;

public class ReflectionProbe_Intesity : MonoBehaviour
{
	private ReflectionProbe refP;

	private GameObject lightObject;

	public Light lightBind;

	public Light lightBindB;

	private void Start()
	{
		refP = GetComponent<ReflectionProbe>();
		lightObject = lightBind.gameObject;
	}

	private void Update()
	{
		float num = 0f;
		if (lightBind != null)
		{
			if (!lightObject.activeInHierarchy)
			{
				num = 0f;
			}
			else
			{
				num = lightBind.intensity;
				if (num > 1f)
				{
					num = 1f;
				}
			}
		}
		float num2 = 0f;
		if (lightBindB != null)
		{
			if (!lightObject.activeInHierarchy)
			{
				num2 = 0f;
			}
			else
			{
				num2 = lightBindB.intensity;
				if (num2 > 1f)
				{
					num2 = 1f;
				}
			}
		}
		if (lightBind != null && lightBindB == null)
		{
			refP.intensity = num;
		}
		if (lightBind != null && lightBindB != null)
		{
			refP.intensity = (num + num2) / 2f;
		}
	}
}
