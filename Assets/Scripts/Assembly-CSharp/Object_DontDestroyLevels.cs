using UnityEngine;

public class Object_DontDestroyLevels : MonoBehaviour
{
	public bool onStart;

	private void Start()
	{
		if (onStart)
		{
			DontDestroy();
		}
	}

	public void DontDestroy()
	{
		base.transform.SetParent(null);
		Object.DontDestroyOnLoad(base.gameObject);
	}
}
