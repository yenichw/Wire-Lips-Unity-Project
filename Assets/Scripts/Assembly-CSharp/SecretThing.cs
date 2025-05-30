using System.IO;
using UnityEngine;

public class SecretThing : MonoBehaviour
{
	public string filename;

	private void Start()
	{
		if (File.Exists("Data/Save/" + filename))
		{
			Object.Destroy(base.gameObject);
		}
	}

	public void TakeItem()
	{
		File.Create("Data/Save/" + filename);
	}
}
