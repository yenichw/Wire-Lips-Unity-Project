using UnityEngine;
using UnityEngine.UI;

public class LaptopTime : MonoBehaviour
{
	public int timeMin;

	public int timeHour;

	private float timeReal;

	private Text text;

	private void Start()
	{
		text = GetComponent<Text>();
		UText();
	}

	private void Update()
	{
		timeReal += Time.deltaTime;
		if (!(timeReal > 60f))
		{
			return;
		}
		timeMin++;
		timeReal = 0f;
		if (timeMin > 60)
		{
			timeMin = 0;
			timeHour++;
			if (timeHour > 11)
			{
				timeHour = 0;
			}
		}
		UText();
	}

	private void UText()
	{
		if (timeMin < 10)
		{
			if (timeHour < 10)
			{
				text.text = "0" + timeHour + " : 0" + timeMin;
			}
			else
			{
				text.text = timeHour + " : 0" + timeMin;
			}
		}
		else if (timeHour < 10)
		{
			if (timeHour == 0)
			{
				text.text = "00 : " + timeMin;
				return;
			}
			text.text = "0" + timeHour + " : " + timeMin;
		}
		else
		{
			text.text = timeHour + " : " + timeMin;
		}
	}
}
