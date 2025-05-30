using UnityEngine;

public class FurnitureItemsGenerate : MonoBehaviour
{
	public GameObject[] itemsGenerate;

	public SpaceGenerateItem[] generateSpace;

	private void Start()
	{
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.K))
		{
			GenearateStart();
		}
	}

	public void GenearateStart()
	{
		for (int i = 0; i < generateSpace.Length; i++)
		{
			for (int j = 0; j < generateSpace[i].maxItems; j++)
			{
				int num = Random.Range(0, itemsGenerate.Length);
				Vector2 vector = new Vector2(Random.Range((0f - generateSpace[i].sizeSpace.x) / 2f, generateSpace[i].sizeSpace.x / 2f), Random.Range((0f - generateSpace[i].sizeSpace.y) / 2f, generateSpace[i].sizeSpace.y / 2f));
				if (vector.x + itemsGenerate[num].GetComponent<FurnitureItem>().sizeItem.x / 2f > generateSpace[i].sizeSpace.x / 2f)
				{
					vector = new Vector2(generateSpace[i].sizeSpace.x / 2f - itemsGenerate[num].GetComponent<FurnitureItem>().sizeItem.x / 2f, vector.y);
				}
				if (vector.x - itemsGenerate[num].GetComponent<FurnitureItem>().sizeItem.x / 2f < (0f - generateSpace[i].sizeSpace.x) / 2f)
				{
					vector = new Vector2((0f - generateSpace[i].sizeSpace.x) / 2f + itemsGenerate[num].GetComponent<FurnitureItem>().sizeItem.x / 2f, vector.y);
				}
				if (vector.y + itemsGenerate[num].GetComponent<FurnitureItem>().sizeItem.z / 2f > generateSpace[i].sizeSpace.y / 2f)
				{
					vector = new Vector2(vector.x, generateSpace[i].sizeSpace.y / 2f - itemsGenerate[num].GetComponent<FurnitureItem>().sizeItem.z / 2f);
				}
				if (vector.y - itemsGenerate[num].GetComponent<FurnitureItem>().sizeItem.z / 2f < (0f - generateSpace[i].sizeSpace.y) / 2f)
				{
					vector = new Vector2(vector.x, (0f - generateSpace[i].sizeSpace.y) / 2f + itemsGenerate[num].GetComponent<FurnitureItem>().sizeItem.z / 2f);
				}
				if (Physics.OverlapBox(base.transform.position + new Vector3(vector.x, generateSpace[i].positionSpace.y + 0.01f, vector.y), new Vector3(itemsGenerate[num].GetComponent<FurnitureItem>().sizeItem.x, 0.004f, itemsGenerate[num].GetComponent<FurnitureItem>().sizeItem.z)).Length == 0)
				{
					GameObject obj = Object.Instantiate(itemsGenerate[num]);
					obj.transform.SetParent(base.transform);
					obj.transform.position = base.transform.position + new Vector3(vector.x, generateSpace[i].positionSpace.y, vector.y);
				}
			}
		}
	}

	private void OnDrawGizmosSelected()
	{
		if (generateSpace.Length != 0)
		{
			Gizmos.color = new Color(0.2f, 0.4f, 1f, 0.7f);
			for (int i = 0; i < generateSpace.Length; i++)
			{
				Gizmos.DrawCube(base.transform.position + generateSpace[i].positionSpace + new Vector3(0f, 0.005f, 0f), new Vector3(generateSpace[i].sizeSpace.x, 0.01f, generateSpace[i].sizeSpace.y));
			}
		}
	}
}
