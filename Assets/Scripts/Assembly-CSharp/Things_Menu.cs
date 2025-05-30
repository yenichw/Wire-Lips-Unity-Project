using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Things_Menu : MonoBehaviour
{
	public Text textDescription;

	[Header("Things")]
	public Text textCaseThing1;

	public GameObject thing1;

	public Text textCaseThing2;

	public GameObject thing2;

	public Text textCaseThing3;

	public GameObject thing3;

	public Text textCaseThing4;

	public GameObject thing4;

	public Text textCaseThing5;

	public GameObject thing5;

	public Text textCaseThing6;

	public GameObject thing6;

	public Text textCaseThing7;

	public GameObject thing7;

	public Text textCaseThing8;

	public GameObject thing8;

	public Text textCaseThing9;

	public GameObject thing9;

	public Text textCaseThing10;

	public GameObject thing10;

	public MenuCase caseLevelTry;

	private MenuMain mmscr;

	private int caseSelect;

	private void Start()
	{
		mmscr = GameObject.FindWithTag("Interface").gameObject.GetComponent<MenuMain>();
		int num = 0;
		if (File.Exists("Data/Save/" + thing1.name))
		{
			thing1.SetActive(value: true);
			textCaseThing1.gameObject.GetComponent<MenuCase>().ActivationCase(x: true);
			num++;
		}
		if (File.Exists("Data/Save/" + thing2.name))
		{
			thing2.SetActive(value: true);
			textCaseThing2.gameObject.GetComponent<MenuCase>().ActivationCase(x: true);
			num++;
		}
		if (File.Exists("Data/Save/" + thing3.name))
		{
			thing3.SetActive(value: true);
			textCaseThing3.gameObject.GetComponent<MenuCase>().ActivationCase(x: true);
			num++;
		}
		if (File.Exists("Data/Save/" + thing4.name))
		{
			thing4.SetActive(value: true);
			textCaseThing4.gameObject.GetComponent<MenuCase>().ActivationCase(x: true);
			num++;
		}
		if (File.Exists("Data/Save/" + thing5.name))
		{
			thing5.SetActive(value: true);
			textCaseThing5.gameObject.GetComponent<MenuCase>().ActivationCase(x: true);
			num++;
		}
		if (File.Exists("Data/Save/" + thing6.name))
		{
			thing6.SetActive(value: true);
			textCaseThing6.gameObject.GetComponent<MenuCase>().ActivationCase(x: true);
			num++;
		}
		if (File.Exists("Data/Save/" + thing7.name))
		{
			thing7.SetActive(value: true);
			textCaseThing7.gameObject.GetComponent<MenuCase>().ActivationCase(x: true);
			num++;
		}
		if (File.Exists("Data/Save/" + thing8.name))
		{
			thing8.SetActive(value: true);
			textCaseThing8.gameObject.GetComponent<MenuCase>().ActivationCase(x: true);
			num++;
		}
		if (File.Exists("Data/Save/" + thing9.name))
		{
			thing9.SetActive(value: true);
			textCaseThing9.gameObject.GetComponent<MenuCase>().ActivationCase(x: true);
			num++;
		}
		if (File.Exists("Data/Save/" + thing10.name))
		{
			thing10.SetActive(value: true);
			textCaseThing10.gameObject.GetComponent<MenuCase>().ActivationCase(x: true);
			num++;
		}
		if (num == 10)
		{
			caseLevelTry.ActivationCase(x: true);
		}
	}

	private void Update()
	{
		if (mmscr.caseSelected != caseSelect)
		{
			ReChangeCaseMenu();
		}
	}

	private void ReChangeCaseMenu()
	{
		caseSelect = mmscr.caseSelected;
		textDescription.text = "";
		if (caseSelect == 0 && thing1.activeInHierarchy)
		{
			textDescription.text = File.ReadAllLines("Data/Languages/" + GlobalGame.Language + "/Things.txt")[1] ?? "";
		}
		if (caseSelect == 1 && thing2.activeInHierarchy)
		{
			textDescription.text = File.ReadAllLines("Data/Languages/" + GlobalGame.Language + "/Things.txt")[3] ?? "";
		}
		if (caseSelect == 2 && thing3.activeInHierarchy)
		{
			textDescription.text = File.ReadAllLines("Data/Languages/" + GlobalGame.Language + "/Things.txt")[5] ?? "";
		}
		if (caseSelect == 3 && thing4.activeInHierarchy)
		{
			textDescription.text = File.ReadAllLines("Data/Languages/" + GlobalGame.Language + "/Things.txt")[7] ?? "";
		}
		if (caseSelect == 4 && thing5.activeInHierarchy)
		{
			textDescription.text = File.ReadAllLines("Data/Languages/" + GlobalGame.Language + "/Things.txt")[9] ?? "";
		}
		if (caseSelect == 5 && thing6.activeInHierarchy)
		{
			textDescription.text = File.ReadAllLines("Data/Languages/" + GlobalGame.Language + "/Things.txt")[11] ?? "";
		}
		if (caseSelect == 6 && thing7.activeInHierarchy)
		{
			textDescription.text = File.ReadAllLines("Data/Languages/" + GlobalGame.Language + "/Things.txt")[13] ?? "";
		}
		if (caseSelect == 7 && thing8.activeInHierarchy)
		{
			textDescription.text = File.ReadAllLines("Data/Languages/" + GlobalGame.Language + "/Things.txt")[15] ?? "";
		}
		if (caseSelect == 8 && thing9.activeInHierarchy)
		{
			textDescription.text = File.ReadAllLines("Data/Languages/" + GlobalGame.Language + "/Things.txt")[17] ?? "";
		}
		if (caseSelect == 9 && thing10.activeInHierarchy)
		{
			textDescription.text = File.ReadAllLines("Data/Languages/" + GlobalGame.Language + "/Things.txt")[19] ?? "";
		}
	}
}
