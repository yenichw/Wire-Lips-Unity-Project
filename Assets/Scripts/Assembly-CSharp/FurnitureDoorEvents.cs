using UnityEngine;
using UnityEngine.Events;

public class FurnitureDoorEvents : MonoBehaviour
{
	public UnityEvent openDoorEvent;

	public UnityEvent closeDoorEvent;

	[Header("One events")]
	public UnityEvent openDoorEventOne;

	public UnityEvent closeDoorEventOne;

	public UnityEvent ifDoorClosedEventOne;

	[Header("Rooms")]
	public GameObject roomLeft;

	public GameObject roomRight;

	public void GiveRooms(FurnitureDoor fd)
	{
		fd.roomLeft = roomLeft;
		fd.roomRight = roomRight;
	}

	public void DoorObject(GameObject _obj)
	{
		_obj.GetComponent<FurnitureDoor>().ReEvents(openDoorEvent, closeDoorEvent, openDoorEventOne, closeDoorEventOne, ifDoorClosedEventOne);
		ConsoleMain.ConsolePrint("Door ReEvents (" + _obj.name + ")");
	}
}
