using System.IO;
using UnityEngine;

public class Save_LevelContinueLoad : MonoBehaviour
{
	public string nameFileForLoad;

	public string nameLevelLoad;

	public void Save()
	{
		File.Create("Data/Save/" + nameFileForLoad);
		StreamWriter streamWriter = File.CreateText("Data/Save/Continue");
		streamWriter.WriteLine(nameLevelLoad ?? "");
		streamWriter.Close();
	}
}
