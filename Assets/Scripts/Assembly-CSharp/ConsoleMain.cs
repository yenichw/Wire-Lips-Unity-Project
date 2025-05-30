using UnityEngine;

public class ConsoleMain : MonoBehaviour
{
	public static string[] console_lastCodes = new string[100];

	public static int console_iLastCode;

	public static string consoleText = "";

	public static void ConsolePrint(string text)
	{
		consoleText = consoleText + "\n" + text;
	}

	public static void ConsolePrintAdd(string text)
	{
		consoleText += text;
	}
}
