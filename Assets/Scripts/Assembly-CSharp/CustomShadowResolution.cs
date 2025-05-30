using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Light))]
[AddComponentMenu("RealToon/Tools/Custom Shadow Resolution")]
public class CustomShadowResolution : MonoBehaviour
{
	[Header("Custom Shadow Resolution V1.0.0")]
	[Header("Note: Higher Shadow Resolution = More GPU RAM Usage.")]
	[Space(10f)]
	[Tooltip("Input value")]
	public int Value = 2048;

	[Tooltip("Final Resolution (Value * 2)")]
	public int FinalResolution = 4096;

	[Space(10f)]
	[Tooltip("Reset to default value")]
	public bool Reset;

	private void Update()
	{
		GetComponent<Light>().shadowCustomResolution = FinalResolution;
		FinalResolution = Value * 2;
		if (Reset)
		{
			Value = 2048;
			FinalResolution = 4096;
			Reset = false;
		}
		if (Value < 0)
		{
			Value = 0;
			FinalResolution = 0;
		}
	}
}
