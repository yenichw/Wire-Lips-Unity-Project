using UnityEngine;

public class Player_HandsPickup : MonoBehaviour
{
	public Transform handWristRight;

	public Transform handWristLeft;

	private Transform handRight;

	private Transform handLeft;

	private bool target;

	private bool rightHandUse;

	private bool leftHandUse;

	private bool rightHandFreePosition;

	private bool leftHandFreePosition;

	private Transform rightWrist;

	private Transform indexRight1;

	private Transform indexRight2;

	private Transform indexRight3;

	private Transform littleRight1;

	private Transform littleRight2;

	private Transform littleRight3;

	private Transform middleRight1;

	private Transform middleRight2;

	private Transform middleRight3;

	private Transform ringRight1;

	private Transform ringRight2;

	private Transform ringRight3;

	private Transform thumbRight1;

	private Transform thumbRight2;

	private Transform thumbRight3;

	private Transform leftWrist;

	private Transform indexLeft1;

	private Transform indexLeft2;

	private Transform indexLeft3;

	private Transform littleLeft1;

	private Transform littleLeft2;

	private Transform littleLeft3;

	private Transform middleLeft1;

	private Transform middleLeft2;

	private Transform middleLeft3;

	private Transform ringLeft1;

	private Transform ringLeft2;

	private Transform ringLeft3;

	private Transform thumbLeft1;

	private Transform thumbLeft2;

	private Transform thumbLeft3;

	private Transform otherHandrightWrist;

	private Transform otherHandindexRight1;

	private Transform otherHandindexRight2;

	private Transform otherHandindexRight3;

	private Transform otherHandlittleRight1;

	private Transform otherHandlittleRight2;

	private Transform otherHandlittleRight3;

	private Transform otherHandmiddleRight1;

	private Transform otherHandmiddleRight2;

	private Transform otherHandmiddleRight3;

	private Transform otherHandringRight1;

	private Transform otherHandringRight2;

	private Transform otherHandringRight3;

	private Transform otherHandthumbRight1;

	private Transform otherHandthumbRight2;

	private Transform otherHandthumbRight3;

	private Transform otherHandleftWrist;

	private Transform otherHandindexLeft1;

	private Transform otherHandindexLeft2;

	private Transform otherHandindexLeft3;

	private Transform otherHandlittleLeft1;

	private Transform otherHandlittleLeft2;

	private Transform otherHandlittleLeft3;

	private Transform otherHandmiddleLeft1;

	private Transform otherHandmiddleLeft2;

	private Transform otherHandmiddleLeft3;

	private Transform otherHandringLeft1;

	private Transform otherHandringLeft2;

	private Transform otherHandringLeft3;

	private Transform otherHandthumbLeft1;

	private Transform otherHandthumbLeft2;

	private Transform otherHandthumbLeft3;

	private void Start()
	{
		indexRight1 = handWristRight.Find("IndexFinger1_R").transform;
		indexRight2 = handWristRight.Find("IndexFinger1_R/IndexFinger2_R").transform;
		indexRight3 = handWristRight.Find("IndexFinger1_R/IndexFinger2_R/IndexFinger3_R").transform;
		littleRight1 = handWristRight.Find("LittleFinger1_R").transform;
		littleRight2 = handWristRight.Find("LittleFinger1_R/LittleFinger2_R").transform;
		littleRight3 = handWristRight.Find("LittleFinger1_R/LittleFinger2_R/LittleFinger3_R").transform;
		middleRight1 = handWristRight.Find("MiddleFinger1_R").transform;
		middleRight2 = handWristRight.Find("MiddleFinger1_R/MiddleFinger2_R").transform;
		middleRight3 = handWristRight.Find("MiddleFinger1_R/MiddleFinger2_R/MiddleFinger3_R").transform;
		ringRight1 = handWristRight.Find("RingFinger1_R").transform;
		ringRight2 = handWristRight.Find("RingFinger1_R/RingFinger2_R").transform;
		ringRight3 = handWristRight.Find("RingFinger1_R/RingFinger2_R/RingFinger3_R").transform;
		thumbRight1 = handWristRight.Find("Thumb0_R").transform;
		thumbRight2 = handWristRight.Find("Thumb0_R/Thumb1_R").transform;
		thumbRight3 = handWristRight.Find("Thumb0_R/Thumb1_R/Thumb2_R").transform;
		indexLeft1 = handWristLeft.Find("IndexFinger1_L").transform;
		indexLeft2 = handWristLeft.Find("IndexFinger1_L/IndexFinger2_L").transform;
		indexLeft3 = handWristLeft.Find("IndexFinger1_L/IndexFinger2_L/IndexFinger3_L").transform;
		littleLeft1 = handWristLeft.Find("LittleFinger1_L").transform;
		littleLeft2 = handWristLeft.Find("LittleFinger1_L/LittleFinger2_L").transform;
		littleLeft3 = handWristLeft.Find("LittleFinger1_L/LittleFinger2_L/LittleFinger3_L").transform;
		middleLeft1 = handWristLeft.Find("MiddleFinger1_L").transform;
		middleLeft2 = handWristLeft.Find("MiddleFinger1_L/MiddleFinger2_L").transform;
		middleLeft3 = handWristLeft.Find("MiddleFinger1_L/MiddleFinger2_L/MiddleFinger3_L").transform;
		ringLeft1 = handWristLeft.Find("RingFinger1_L").transform;
		ringLeft2 = handWristLeft.Find("RingFinger1_L/RingFinger2_L").transform;
		ringLeft3 = handWristLeft.Find("RingFinger1_L/RingFinger2_L/RingFinger3_L").transform;
		thumbLeft1 = handWristLeft.Find("Thumb0_L").transform;
		thumbLeft2 = handWristLeft.Find("Thumb0_L/Thumb1_L").transform;
		thumbLeft3 = handWristLeft.Find("Thumb0_L/Thumb1_L/Thumb2_L").transform;
	}

	private void Update()
	{
		if (!target)
		{
			return;
		}
		if (handRight != null)
		{
			if (!rightHandFreePosition)
			{
				handWristRight.position = otherHandrightWrist.position;
				handWristRight.rotation = otherHandrightWrist.rotation;
			}
			indexRight1.localPosition = otherHandindexRight1.localPosition;
			indexRight1.localRotation = otherHandindexRight1.localRotation;
			indexRight2.localPosition = otherHandindexRight2.localPosition;
			indexRight2.localRotation = otherHandindexRight2.localRotation;
			indexRight3.localPosition = otherHandindexRight3.localPosition;
			indexRight3.localRotation = otherHandindexRight3.localRotation;
			littleRight1.localPosition = otherHandlittleRight1.localPosition;
			littleRight1.localRotation = otherHandlittleRight1.localRotation;
			littleRight2.localPosition = otherHandlittleRight2.localPosition;
			littleRight2.localRotation = otherHandlittleRight2.localRotation;
			littleRight3.localPosition = otherHandlittleRight3.localPosition;
			littleRight3.localRotation = otherHandlittleRight3.localRotation;
			middleRight1.localPosition = otherHandmiddleRight1.localPosition;
			middleRight1.localRotation = otherHandmiddleRight1.localRotation;
			middleRight2.localPosition = otherHandmiddleRight2.localPosition;
			middleRight2.localRotation = otherHandmiddleRight2.localRotation;
			middleRight3.localPosition = otherHandmiddleRight3.localPosition;
			middleRight3.localRotation = otherHandmiddleRight3.localRotation;
			ringRight1.localPosition = otherHandringRight1.localPosition;
			ringRight1.localRotation = otherHandringRight1.localRotation;
			ringRight2.localPosition = otherHandringRight2.localPosition;
			ringRight2.localRotation = otherHandringRight2.localRotation;
			ringRight3.localPosition = otherHandringRight3.localPosition;
			ringRight3.localRotation = otherHandringRight3.localRotation;
			thumbRight1.localPosition = otherHandthumbRight1.localPosition;
			thumbRight1.localRotation = otherHandthumbRight1.localRotation;
			thumbRight2.localPosition = otherHandthumbRight2.localPosition;
			thumbRight2.localRotation = otherHandthumbRight2.localRotation;
			thumbRight3.localPosition = otherHandthumbRight3.localPosition;
			thumbRight3.localRotation = otherHandthumbRight3.localRotation;
		}
		if (handLeft != null)
		{
			if (!leftHandFreePosition)
			{
				handWristLeft.position = otherHandleftWrist.position;
				handWristLeft.rotation = otherHandleftWrist.rotation;
			}
			indexLeft1.localPosition = otherHandindexLeft1.localPosition;
			indexLeft1.localRotation = otherHandindexLeft1.localRotation;
			indexLeft2.localPosition = otherHandindexLeft2.localPosition;
			indexLeft2.localRotation = otherHandindexLeft2.localRotation;
			indexLeft3.localPosition = otherHandindexLeft3.localPosition;
			indexLeft3.localRotation = otherHandindexLeft3.localRotation;
			littleLeft1.localPosition = otherHandlittleLeft1.localPosition;
			littleLeft1.localRotation = otherHandlittleLeft1.localRotation;
			littleLeft2.localPosition = otherHandlittleLeft2.localPosition;
			littleLeft2.localRotation = otherHandlittleLeft2.localRotation;
			littleLeft3.localPosition = otherHandlittleLeft3.localPosition;
			littleLeft3.localRotation = otherHandlittleLeft3.localRotation;
			middleLeft1.localPosition = otherHandmiddleLeft1.localPosition;
			middleLeft1.localRotation = otherHandmiddleLeft1.localRotation;
			middleLeft2.localPosition = otherHandmiddleLeft2.localPosition;
			middleLeft2.localRotation = otherHandmiddleLeft2.localRotation;
			middleLeft3.localPosition = otherHandmiddleLeft3.localPosition;
			middleLeft3.localRotation = otherHandmiddleLeft3.localRotation;
			ringLeft1.localPosition = otherHandringLeft1.localPosition;
			ringLeft1.localRotation = otherHandringLeft1.localRotation;
			ringLeft2.localPosition = otherHandringLeft2.localPosition;
			ringLeft2.localRotation = otherHandringLeft2.localRotation;
			ringLeft3.localPosition = otherHandringLeft3.localPosition;
			ringLeft3.localRotation = otherHandringLeft3.localRotation;
			thumbLeft1.localPosition = otherHandthumbLeft1.localPosition;
			thumbLeft1.localRotation = otherHandthumbLeft1.localRotation;
			thumbLeft2.localPosition = otherHandthumbLeft2.localPosition;
			thumbLeft2.localRotation = otherHandthumbLeft2.localRotation;
			thumbLeft3.localPosition = otherHandthumbLeft3.localPosition;
			thumbLeft3.localRotation = otherHandthumbLeft3.localRotation;
		}
	}

	public void HandsTarget(Transform _handRight, Transform _handLeft, bool _freePosition)
	{
		bool flag = true;
		if (_handRight == null)
		{
			flag = false;
		}
		bool flag2 = true;
		if (_handLeft == null)
		{
			flag2 = false;
		}
		target = true;
		if (flag)
		{
			handRight = _handRight;
			rightHandFreePosition = _freePosition;
			otherHandrightWrist = _handRight;
			otherHandindexRight1 = _handRight.Find("IndexFinger1_R").transform;
			otherHandindexRight2 = _handRight.Find("IndexFinger1_R/IndexFinger2_R").transform;
			otherHandindexRight3 = _handRight.Find("IndexFinger1_R/IndexFinger2_R/IndexFinger3_R").transform;
			otherHandlittleRight1 = _handRight.Find("LittleFinger1_R").transform;
			otherHandlittleRight2 = _handRight.Find("LittleFinger1_R/LittleFinger2_R").transform;
			otherHandlittleRight3 = _handRight.Find("LittleFinger1_R/LittleFinger2_R/LittleFinger3_R").transform;
			otherHandmiddleRight1 = _handRight.Find("MiddleFinger1_R").transform;
			otherHandmiddleRight2 = _handRight.Find("MiddleFinger1_R/MiddleFinger2_R").transform;
			otherHandmiddleRight3 = _handRight.Find("MiddleFinger1_R/MiddleFinger2_R/MiddleFinger3_R").transform;
			otherHandringRight1 = _handRight.Find("RingFinger1_R").transform;
			otherHandringRight2 = _handRight.Find("RingFinger1_R/RingFinger2_R").transform;
			otherHandringRight3 = _handRight.Find("RingFinger1_R/RingFinger2_R/RingFinger3_R").transform;
			otherHandthumbRight1 = _handRight.Find("Thumb0_R").transform;
			otherHandthumbRight2 = _handRight.Find("Thumb0_R/Thumb1_R").transform;
			otherHandthumbRight3 = _handRight.Find("Thumb0_R/Thumb1_R/Thumb2_R").transform;
		}
		if (flag2)
		{
			handLeft = _handLeft;
			leftHandFreePosition = _freePosition;
			otherHandleftWrist = _handLeft;
			otherHandindexLeft1 = _handLeft.Find("IndexFinger1_L").transform;
			otherHandindexLeft2 = _handLeft.Find("IndexFinger1_L/IndexFinger2_L").transform;
			otherHandindexLeft3 = _handLeft.Find("IndexFinger1_L/IndexFinger2_L/IndexFinger3_L").transform;
			otherHandlittleLeft1 = _handLeft.Find("LittleFinger1_L").transform;
			otherHandlittleLeft2 = _handLeft.Find("LittleFinger1_L/LittleFinger2_L").transform;
			otherHandlittleLeft3 = _handLeft.Find("LittleFinger1_L/LittleFinger2_L/LittleFinger3_L").transform;
			otherHandmiddleLeft1 = _handLeft.Find("MiddleFinger1_L").transform;
			otherHandmiddleLeft2 = _handLeft.Find("MiddleFinger1_L/MiddleFinger2_L").transform;
			otherHandmiddleLeft3 = _handLeft.Find("MiddleFinger1_L/MiddleFinger2_L/MiddleFinger3_L").transform;
			otherHandringLeft1 = _handLeft.Find("RingFinger1_L").transform;
			otherHandringLeft2 = _handLeft.Find("RingFinger1_L/RingFinger2_L").transform;
			otherHandringLeft3 = _handLeft.Find("RingFinger1_L/RingFinger2_L/RingFinger3_L").transform;
			otherHandthumbLeft1 = _handLeft.Find("Thumb0_L").transform;
			otherHandthumbLeft2 = _handLeft.Find("Thumb0_L/Thumb1_L").transform;
			otherHandthumbLeft3 = _handLeft.Find("Thumb0_L/Thumb1_L/Thumb2_L").transform;
		}
		ConsoleMain.ConsolePrint(string.Concat("HandsPickup.HandsTarget() |R ", handRight, " |L ", handLeft));
	}

	public void StopTarget()
	{
		ConsoleMain.ConsolePrint("HandsPickup.StopTarget()");
		target = false;
	}

	public void StopTargetHand(bool _right, bool _left)
	{
		ConsoleMain.ConsolePrint("HandsPickup.StopTargetHand()");
		if (_right)
		{
			handRight = null;
		}
		if (_left)
		{
			handLeft = null;
		}
	}
}
