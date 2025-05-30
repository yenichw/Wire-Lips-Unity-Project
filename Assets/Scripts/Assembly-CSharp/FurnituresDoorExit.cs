using UnityEngine;

public class FurnituresDoorExit : MonoBehaviour
{
	public DoorLockPrefab[] lockers;

	private int countLockers;

	private GameObject obj;

	public void InstallLocker(int _maxLock)
	{
		float num = 0.35f;
		int num2 = Random.Range(1, _maxLock);
		for (int i = 0; i < num2; i++)
		{
			int num3 = Random.Range(0, lockers.Length);
			if (!lockers[num3].lockOnlyOneUsed)
			{
				if (lockers[num3].onlyOne)
				{
					lockers[num3].lockOnlyOneUsed = true;
				}
				if (lockers[num3].prefabOnShoal != null)
				{
					obj = Object.Instantiate(lockers[num3].prefabOnShoal);
					obj.transform.SetParent(base.transform.Find("Shoal").transform);
					obj.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
					obj.transform.localPosition = new Vector3(0f, 0f, num);
				}
				if (lockers[num3].prefabOnDoor != null)
				{
					obj = Object.Instantiate(lockers[num3].prefabOnDoor);
					obj.transform.SetParent(base.transform.Find("Shoal/Door").transform);
					obj.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
					obj.transform.localPosition = new Vector3(0f, 0f, num);
				}
				countLockers++;
				num -= 0.17f;
			}
		}
	}
}
