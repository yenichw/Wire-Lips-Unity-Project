using UnityEngine;

public class Interface_Task : MonoBehaviour
{
	public string fileTask = "Thoughts 1";

	[Header("будет удалено в следующей игре")]
	public int fileString = 1;

	public void TaskStart()
	{
		ConsoleMain.ConsolePrint("Task: " + fileString);
		GameObject.FindWithTag("GameController").gameObject.GetComponent<Interface_MainPlayer>().Task(fileTask, fileString);
	}

	public void TaskStartInt(int numberString)
	{
		ConsoleMain.ConsolePrint("Task: " + numberString);
		GameObject.FindWithTag("GameController").gameObject.GetComponent<Interface_MainPlayer>().Task(fileTask, numberString);
	}

	public void TaskEnd()
	{
		ConsoleMain.ConsolePrint("TaskEnd");
		GameObject.FindWithTag("GameController").gameObject.GetComponent<Interface_MainPlayer>().TaskEnd();
	}
}
