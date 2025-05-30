using UnityEngine;

public class Audio_StepFoot_ReChange : MonoBehaviour
{
	public AudioClip[] soundsFoot;

	[Range(0f, 1f)]
	public float volume = 0.5f;

	public void ReChange(Audio_StepFoot _stepFoot)
	{
		_stepFoot.soundsFoot = soundsFoot;
		_stepFoot.volume = volume;
	}
}
