using UnityEngine;

public class MenuChangeStartCharacter : MonoBehaviour
{
	public SkinnedMeshRenderer meshEyes;

	public GameObject head;

	public MenuMain menuMain;

	public Texture2D[] eyesTexture;

	public int hairsCount;

	public RectTransform changeRectHairs;

	public RectTransform changeRectEyes;

	public GameObject[] eyesCases = new GameObject[15];

	private GameObject[] hairsCases = new GameObject[15];

	public int iChangeEyesNow;

	public int iChangeHairsNow;

	private void Start()
	{
		GameObject original = base.transform.Find("Hairs/Change").gameObject;
		int num = -15;
		for (int i = 0; i < hairsCount; i++)
		{
			hairsCases[i] = Object.Instantiate(original, base.transform.Find("Hairs").transform);
			hairsCases[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(num, 0f);
			num -= 20;
		}
		num = -15;
		for (int j = 0; j < eyesTexture.Length; j++)
		{
			eyesCases[j] = Object.Instantiate(original, base.transform.Find("Eyes").transform);
			eyesCases[j].GetComponent<RectTransform>().anchoredPosition = new Vector2(num, 0f);
			num -= 20;
		}
	}

	private void Update()
	{
	}

	public void Left()
	{
		if (menuMain.caseSelected == 2)
		{
			iChangeEyesNow++;
			if (iChangeEyesNow > eyesTexture.Length - 1)
			{
				iChangeEyesNow = 0;
			}
			meshEyes.materials[2].mainTexture = eyesTexture[iChangeEyesNow];
			changeRectEyes.anchoredPosition = new Vector2(eyesCases[iChangeEyesNow].GetComponent<RectTransform>().anchoredPosition.x + 5f, 0f);
		}
		if (menuMain.caseSelected == 1)
		{
			iChangeHairsNow++;
			if (iChangeHairsNow > eyesTexture.Length - 1)
			{
				iChangeHairsNow = 0;
			}
			changeRectHairs.anchoredPosition = new Vector2(hairsCases[iChangeHairsNow].GetComponent<RectTransform>().anchoredPosition.x + 5f, 0f);
		}
	}

	public void Right()
	{
		if (menuMain.caseSelected == 2)
		{
			iChangeEyesNow--;
			if (iChangeEyesNow < 0)
			{
				iChangeEyesNow = eyesTexture.Length - 1;
			}
			meshEyes.materials[2].mainTexture = eyesTexture[iChangeEyesNow];
			changeRectEyes.anchoredPosition = new Vector2(eyesCases[iChangeEyesNow].GetComponent<RectTransform>().anchoredPosition.x + 5f, 0f);
		}
		if (menuMain.caseSelected == 1)
		{
			iChangeHairsNow--;
			if (iChangeHairsNow < 0)
			{
				iChangeHairsNow = eyesTexture.Length - 1;
			}
			changeRectHairs.anchoredPosition = new Vector2(hairsCases[iChangeHairsNow].GetComponent<RectTransform>().anchoredPosition.x + 5f, 0f);
		}
	}
}
