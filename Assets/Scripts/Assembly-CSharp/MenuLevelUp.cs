using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuLevelUp : MonoBehaviour
{
	public GameObject skillBoard;

	public Text textLevel;

	public Text textScore;

	public Text textPlus;

	public Text textDescription;

	private bool down;

	private bool up;

	private int ichangeCase;

	private RectTransform change;

	private GameObject board;

	private GameObject lastChangeUI;

	private MenuSkillBoard scrSkillBoard;

	private void Start()
	{
		board = Object.Instantiate(skillBoard, base.transform.Find("Frame Cases/FrameMask").gameObject.transform);
		change = board.transform.Find("CaseChange").gameObject.GetComponent<RectTransform>();
		scrSkillBoard = board.GetComponent<MenuSkillBoard>();
		EventSystem.current.SetSelectedGameObject(scrSkillBoard.firstCaseUI);
		lastChangeUI = scrSkillBoard.firstCaseUI;
	}

	private void Update()
	{
		if (Input.GetButtonDown("Space") || Input.GetButtonDown("Action"))
		{
			Enter();
		}
		if (Input.GetButtonDown("Cancel"))
		{
			Close();
		}
		if (EventSystem.current.currentSelectedGameObject == null)
		{
			EventSystem.current.SetSelectedGameObject(lastChangeUI);
		}
		lastChangeUI = EventSystem.current.currentSelectedGameObject;
		change.anchoredPosition = EventSystem.current.currentSelectedGameObject.GetComponent<RectTransform>().anchoredPosition;
		board.GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(board.GetComponent<RectTransform>().anchoredPosition, -change.anchoredPosition, 0.1f);
	}

	private void Enter()
	{
	}

	private void Close()
	{
		Object.Destroy(base.gameObject);
	}
}
