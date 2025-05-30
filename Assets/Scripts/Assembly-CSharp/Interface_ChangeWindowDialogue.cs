using System.IO;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Interface_ChangeWindowDialogue : MonoBehaviour
{
	public RectTransform changeLine;

	[HideInInspector]
	public int iTableChange;

	private int iTables;

	private bool down;

	private bool up;

	private bool stFirst;

	private GameObject exampleTable;

	private RectTransform frameWindow;

	private GameObject[] tables = new GameObject[20];

	public UnityEvent[] _eventTable = new UnityEvent[20];

	public void Start()
	{
		if (!stFirst)
		{
			stFirst = true;
			frameWindow = GetComponent<RectTransform>();
			exampleTable = base.transform.Find("TextChange").gameObject;
			exampleTable.SetActive(value: false);
		}
	}

	private void Update()
	{
		if (Input.GetButtonDown("Space") || Input.GetButtonDown("Action"))
		{
			Enter();
		}
		if (Input.GetAxis("Vertical") > 0f && !up)
		{
			up = true;
			down = false;
			Up();
		}
		if (Input.GetAxis("Vertical") < 0f && !down)
		{
			down = true;
			up = false;
			Down();
		}
		if (Input.GetAxis("Vertical") == 0f)
		{
			up = false;
			down = false;
		}
	}

	private void ChangeRect()
	{
		changeLine.GetComponent<RectTransform>().anchoredPosition = new Vector2(24f, -30 - iTableChange * 30);
	}

	public void Down()
	{
		iTableChange++;
		if (iTableChange > iTables - 1)
		{
			iTableChange = 0;
		}
		ChangeRect();
	}

	public void Up()
	{
		iTableChange--;
		if (iTableChange < 0)
		{
			iTableChange = iTables - 1;
		}
		ChangeRect();
	}

	private void Enter()
	{
		_eventTable[iTableChange].Invoke();
	}

	public void AddTable(string _file, int _string, UnityEvent _event)
	{
		tables[iTables] = Object.Instantiate(exampleTable, base.transform);
		tables[iTables].SetActive(value: true);
		tables[iTables].gameObject.transform.Find("Text").GetComponent<Text>().text = File.ReadAllLines("Data/Languages/" + GlobalGame.Language + "/" + _file + ".txt")[_string - 1];
		tables[iTables].GetComponent<RectTransform>().anchoredPosition = new Vector2(17f, -30 - iTables * 30);
		_eventTable[iTables] = _event;
		frameWindow.sizeDelta = new Vector2(222f, 60 + iTables * 30);
		iTables++;
		ChangeRect();
	}
}
