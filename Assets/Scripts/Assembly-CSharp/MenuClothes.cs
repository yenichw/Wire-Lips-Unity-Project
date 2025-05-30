using System;
using UnityEngine;
using UnityEngine.UI;

public class MenuClothes : MonoBehaviour
{
	[Serializable]
	public class ClothesChange
	{
		public RectTransform changeRect;

		public GameObject[] cases;
	}

	public GameObject buyObject;

	public GameObject wearObject;

	public Text textBuy;

	public Text textTeeth;

	public MenuMain menuMain;

	public ClothesChange[] clothes;

	public int changeCloth;

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void Right()
	{
	}

	public void Left()
	{
	}

	public void Wear()
	{
	}
}
