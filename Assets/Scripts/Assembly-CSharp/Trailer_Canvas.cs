using UnityEngine;

public class Trailer_Canvas : MonoBehaviour
{
	[Header("Trailer")]
	public bool disableCanvas = true;

	private void Start()
	{
		if (GlobalGame.trailer)
		{
			if (disableCanvas)
			{
				GetComponent<Canvas>().enabled = false;
			}
		}
		else
		{
			Object.Destroy(this);
		}
	}
}
