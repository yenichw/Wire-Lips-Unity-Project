using UnityEngine;

public class Camera_Background : MonoBehaviour
{
	public Color color = new Color(1f, 1f, 1f, 1f);

	[Range(0f, 1f)]
	public float speedColor = 0.05f;

	private Camera cam;

	private void Start()
	{
		cam = GetComponent<Camera>();
		cam.backgroundColor = color;
	}

	private void Update()
	{
		cam.backgroundColor = Color.Lerp(cam.backgroundColor, color, speedColor);
	}
}
