using UnityEngine;
using UnityEngine.UI;

public class UI_TextFontGlobal : MonoBehaviour
{
	private void Start()
	{
		GetComponent<Text>().font = Resources.Load("Fonts/Font " + GlobalGame.font) as Font;
	}
}
