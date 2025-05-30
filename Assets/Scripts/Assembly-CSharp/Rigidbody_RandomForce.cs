using UnityEngine;

[AddComponentMenu("Functions/Rigidbody/Random Force")]
public class Rigidbody_RandomForce : MonoBehaviour
{
	public Vector3 randomForce;

	private Rigidbody rg;

	private void Start()
	{
		rg = GetComponent<Rigidbody>();
	}

	private void Update()
	{
		rg.AddForce(new Vector3(Random.Range(0f - randomForce.x, randomForce.x), Random.Range(0f - randomForce.y, randomForce.y), Random.Range(0f - randomForce.z, randomForce.z)));
	}
}
