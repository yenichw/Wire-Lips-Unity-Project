using UnityEngine;

public class ConsoleCommandsA : MonoBehaviour
{
	public static void CheckConsoleCommandA(string code)
	{
		if (code == "aihasto")
		{
			ConsoleMain.ConsolePrint("AIHASTO GAMES");
		}
	}
}
