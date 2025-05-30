using UnityEngine;

public class Interface_DialogueThought : MonoBehaviour
{
	public string fileThought = "Dialogue";

	public void Thought(int x)
	{
		GameObject.FindWithTag("GameController").gameObject.GetComponent<Interface_MainPlayer>().Dialogue(fileThought, x);
	}
}
