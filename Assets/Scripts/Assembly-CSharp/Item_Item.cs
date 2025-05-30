using UnityEngine;

public class Item_Item : MonoBehaviour
{
	public string nameItemResource;

	private Interactive_Action myAction;

	private Player scrPlayer;

	private GameObject objInteract;

	private Interface_MainPlayer scrIntMain;

	private bool takeHand;

	private int tmDestroyScale;

	private void Start()
	{
		scrIntMain = GameObject.FindWithTag("GameController").GetComponent<Interface_MainPlayer>();
		objInteract = Object.Instantiate(Resources.Load<GameObject>("Interface/Interactive"), base.transform.position, Quaternion.identity, base.transform);
		myAction = objInteract.GetComponent<Interactive_Action>();
		scrPlayer = GameObject.FindWithTag("Player").gameObject.GetComponent<Player>();
	}

	private void Update()
	{
		if (myAction.changePlayer && !scrPlayer.ikBringEndHand && !takeHand && Input.GetButton("Action"))
		{
			if (!myAction.handRight_handLeft)
			{
				scrPlayer.handTargetRight.position = Vector3.Lerp(scrPlayer.handTargetRight.position, base.transform.position, Time.deltaTime * 10f);
				scrPlayer.handRightIkTarget = 2;
				if (scrPlayer.rightHandTakeIk)
				{
					TakeHand();
				}
			}
			if (myAction.handRight_handLeft)
			{
				scrPlayer.handTargetLeft.position = Vector3.Lerp(scrPlayer.handTargetLeft.position, base.transform.position, Time.deltaTime * 10f);
				scrPlayer.handLeftIkTarget = 2;
				if (scrPlayer.leftHandTakeIk)
				{
					TakeHand();
				}
			}
		}
		if (takeHand)
		{
			tmDestroyScale++;
			if (tmDestroyScale > 20)
			{
				base.transform.localScale -= new Vector3(base.transform.localScale.x, base.transform.localScale.x, base.transform.localScale.x) / 10f;
			}
			base.transform.localPosition = Vector3.zero;
			if ((double)base.transform.localScale.x < 0.001)
			{
				TakeItem();
			}
		}
	}

	private void TakeHand()
	{
		Object.Destroy(objInteract);
		if (!myAction.handRight_handLeft)
		{
			base.transform.SetParent(GameObject.FindWithTag("Player").gameObject.transform.Find("Armature/Hips/Spine/Chest/Right shoulder/Right arm/Right elbow/Right wrist").gameObject.transform);
		}
		if (myAction.handRight_handLeft)
		{
			base.transform.SetParent(GameObject.FindWithTag("Player").gameObject.transform.Find("Armature/Hips/Spine/Chest/Left shoulder/Left arm/Left elbow/Left wrist").gameObject.transform);
		}
		scrPlayer.IkBringEndHand();
		takeHand = true;
		if (GetComponent<BoxCollider>() != null)
		{
			Object.Destroy(GetComponent<BoxCollider>());
		}
	}

	public void TakeItem()
	{
		GameObject gameObject = Resources.Load<GameObject>("Items/" + nameItemResource);
		scrIntMain.itemsDataNow[scrIntMain.itemCount].itemObject = gameObject;
		if (gameObject.GetComponent<Item_ItemInventory>().specialItem && !gameObject.GetComponent<Item_ItemInventory>().canCombine)
		{
			GameObject gameObject2 = Object.Instantiate(Resources.Load<GameObject>("ItemsShow/" + nameItemResource));
			scrIntMain.itemsDataNow[scrIntMain.itemCount].dataShow = gameObject2;
			gameObject2.SetActive(value: false);
		}
		scrIntMain.itemCount++;
		Object.Destroy(base.gameObject);
	}
}
