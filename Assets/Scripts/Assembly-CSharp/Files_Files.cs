using System.IO;
using UnityEngine;

public class Files_Files : MonoBehaviour
{
	public void CreateFile(string x)
	{
		File.Create(x ?? "");
	}

	public void CreateFileSave(string x)
	{
		File.Create("Data/Save/" + x);
	}
}
