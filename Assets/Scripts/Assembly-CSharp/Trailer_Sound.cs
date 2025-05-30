using UnityEngine;

public class Trailer_Sound : MonoBehaviour
{
	[Header("Trailer")]
	public bool destroyObject = true;

	private void Start()
	{
		if (GlobalGame.trailer)
		{
			if (destroyObject)
			{
				Object.Destroy(base.gameObject);
			}
		}
		else
		{
			Object.Destroy(this);
		}
	}
}
