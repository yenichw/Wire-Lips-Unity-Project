using UnityEngine;

[AddComponentMenu("Functions/Start/Objects Active")]
public class Start_ActiveAndDeactive : MonoBehaviour
{
	private int i;

	public GameObject[] startActive;

	public GameObject[] startDeactive;

	public GameObject[] destroyObjects;

	private void Start()
	{
		for (i = 0; i < startActive.Length; i++)
		{
			startActive[i].SetActive(value: true);
		}
		for (i = 0; i < startDeactive.Length; i++)
		{
			startDeactive[i].SetActive(value: false);
		}
		for (i = 0; i < destroyObjects.Length; i++)
		{
			Object.Destroy(destroyObjects[i]);
		}
	}
}
