using UnityEngine;
using UnityEngine.UI;

public class Interface_ShowItem : MonoBehaviour
{
	public Text textNameItem;

	public string textItem;

	[HideInInspector]
	public GameObject objectShow;

	[HideInInspector]
	public bool backMyInventory;

	private float xRotation;

	private float yRotation;

	private float zRotation;

	private void Start()
	{
		objectShow.SetActive(value: true);
		objectShow.transform.SetParent(base.transform.Find("Camera").transform);
		objectShow.transform.localPosition = new Vector3(0f, 0f, 5f);
		objectShow.transform.rotation = Quaternion.Euler(objectShow.GetComponent<Iten_Show>().rotation);
		xRotation = objectShow.transform.eulerAngles.x;
		yRotation = objectShow.transform.eulerAngles.y;
		zRotation = objectShow.transform.eulerAngles.z;
		textNameItem.text = textItem;
	}

	private void Update()
	{
		if (Input.GetAxis("Vertical") != 0f || Input.GetAxis("Horizontal") != 0f)
		{
			xRotation -= Input.GetAxis("Horizontal") * 200f * Time.deltaTime;
			yRotation += Input.GetAxis("Vertical") * 200f * Time.deltaTime;
			objectShow.transform.rotation = Quaternion.Euler(yRotation, xRotation, zRotation);
		}
		if ((Input.GetButtonDown("Cancel") || Input.GetButtonDown("Inventory") || Input.GetButtonDown("Show")) && backMyInventory)
		{
			objectShow.transform.parent = null;
			objectShow.SetActive(value: false);
			GameObject.FindWithTag("GameController").GetComponent<Interface_MainPlayer>().InventoryPlayer();
			Object.Destroy(base.gameObject);
		}
	}
}
