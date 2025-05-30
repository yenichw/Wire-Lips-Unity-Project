using UnityEngine;

[AddComponentMenu("Functions/Object/Create")]
public class Object_Create : MonoBehaviour
{
	public GameObject _object;

	public Transform whereTransform;

	public void Create()
	{
		Object.Instantiate(_object, whereTransform);
	}

	public void CreateMyPosition()
	{
		Object.Instantiate(_object, whereTransform).transform.position = base.transform.position;
	}
}
