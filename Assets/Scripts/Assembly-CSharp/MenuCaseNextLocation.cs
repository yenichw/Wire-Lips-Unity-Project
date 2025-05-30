using UnityEngine;

public class MenuCaseNextLocation : MonoBehaviour
{
	private MenuMain menu;

	public GameObject[] ChangeSelect;

	public int selectFirst;

	public MenuMain.UIAll[] ObjectUI;

	public void NextLocation(int selectCase)
	{
		menu = GameObject.FindWithTag("Interface").gameObject.GetComponent<MenuMain>();
		if (selectCase != -1)
		{
			selectFirst = selectCase;
		}
		menu.NextMenu(ObjectUI, ChangeSelect, selectFirst);
	}
}
