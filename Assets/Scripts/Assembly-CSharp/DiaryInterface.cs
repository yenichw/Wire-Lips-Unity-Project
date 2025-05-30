using UnityEngine;
using UnityEngine.UI;

public class DiaryInterface : MonoBehaviour
{
	public GameObject[] pages;

	public Image imgRight;

	public Image imgLeft;

	public int pageNow;

	private bool rightKey;

	private bool leftKey;

	private bool inGame;

	private Player playerScr;

	private void Start()
	{
		if (GameObject.FindWithTag("Player") != null)
		{
			playerScr = GameObject.FindWithTag("Player").GetComponent<Player>();
			playerScr.dontMove = true;
			inGame = true;
		}
		imgLeft.enabled = false;
	}

	private void Update()
	{
		if (Input.GetAxis("Horizontal") > 0f && !rightKey)
		{
			leftKey = false;
			rightKey = true;
			Right();
		}
		if (Input.GetAxis("Horizontal") < 0f && !leftKey)
		{
			rightKey = false;
			leftKey = true;
			Left();
		}
		if (Input.GetAxis("Horizontal") == 0f)
		{
			rightKey = false;
			leftKey = false;
		}
		if (Input.GetButtonDown("Cancel") && inGame)
		{
			CloseDiary();
		}
	}

	private void Right()
	{
		HideAllPages();
		pageNow++;
		pages[pageNow].SetActive(value: true);
		if (pageNow == pages.Length - 1)
		{
			imgRight.enabled = false;
		}
		imgLeft.enabled = true;
	}

	private void Left()
	{
		HideAllPages();
		pageNow--;
		pages[pageNow].SetActive(value: true);
		if (pageNow == 0)
		{
			imgLeft.enabled = false;
		}
		imgRight.enabled = true;
	}

	private void HideAllPages()
	{
		for (int i = 0; i < pages.Length; i++)
		{
			pages[i].SetActive(value: false);
		}
	}

	private void CloseDiary()
	{
		playerScr.dontMove = false;
		Object.Destroy(base.gameObject);
	}
}
