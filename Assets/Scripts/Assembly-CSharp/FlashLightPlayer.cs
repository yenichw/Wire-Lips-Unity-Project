using UnityEngine;

public class FlashLightPlayer : MonoBehaviour
{
	private GameObject lightPoint;

	private MeshRenderer rend;

	private void Start()
	{
		lightPoint = new GameObject();
		lightPoint.transform.parent = base.transform;
		lightPoint.AddComponent<Light>();
		lightPoint.GetComponent<Light>().type = LightType.Point;
		lightPoint.GetComponent<Light>().range = 1f;
		lightPoint.GetComponent<Light>().intensity = 1f;
		lightPoint.transform.localPosition = new Vector3(-0.6f, 0.38f, 0f);
		GetComponent<Light>().range = 5.5f;
		GetComponent<Light>().intensity = 2.7f;
		GetComponent<Light>().spotAngle = 75f;
		rend = base.transform.parent.gameObject.GetComponent<MeshRenderer>();
	}

	private void OnEnable()
	{
		rend = base.transform.parent.gameObject.GetComponent<MeshRenderer>();
		rend.material.SetColor("_EmissionColor", new Color(0.7f, 0.7f, 0.7f, 0.7f));
	}

	private void OnDisable()
	{
		rend.material.SetColor("_EmissionColor", new Color(0f, 0f, 0f, 0f));
	}
}
