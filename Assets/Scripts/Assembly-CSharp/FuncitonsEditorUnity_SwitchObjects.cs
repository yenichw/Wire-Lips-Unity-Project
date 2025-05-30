using UnityEngine;
using UnityEngine.Events;

public class FuncitonsEditorUnity_SwitchObjects : MonoBehaviour
{
	public SwitchObjectEditor switchEpisode1;

	public SwitchObjectEditor switchEpisode2;

	public SwitchObjectEditor switchEpisode3;

	public SwitchObjectEditor switchEpisode4;

	public SwitchObjectEditor switchEpisode5;

	[Space(10f)]
	[Header("always switch")]
	public UnityEvent _event;

	public GameObject[] objectDeactive;

	public GameObject[] objectActive;

	private void Switch(int _x)
	{
		_event.Invoke();
		if (objectDeactive.Length != 0)
		{
			for (int i = 0; i < objectDeactive.Length; i++)
			{
				objectDeactive[i].SetActive(value: false);
			}
		}
		if (objectActive.Length != 0)
		{
			for (int j = 0; j < objectActive.Length; j++)
			{
				objectActive[j].SetActive(value: true);
			}
		}
		SwitchObjectEditor switchObjectEditor = new SwitchObjectEditor();
		if (_x == 1)
		{
			switchObjectEditor = switchEpisode1;
		}
		if (_x == 2)
		{
			switchObjectEditor = switchEpisode2;
		}
		if (_x == 3)
		{
			switchObjectEditor = switchEpisode3;
		}
		if (_x == 4)
		{
			switchObjectEditor = switchEpisode4;
		}
		if (_x == 5)
		{
			switchObjectEditor = switchEpisode5;
		}
		for (int k = 0; k < switchObjectEditor.deactive.Length; k++)
		{
			switchObjectEditor.deactive[k].SetActive(value: false);
		}
		for (int l = 0; l < switchObjectEditor.active.Length; l++)
		{
			switchObjectEditor.active[l].SetActive(value: true);
		}
		switchObjectEditor._event.Invoke();
	}

	[ContextMenu("Episode 1")]
	private void SwitchEpisode1()
	{
		Switch(1);
	}

	[ContextMenu("Episode 2")]
	private void SwitchEpisode2()
	{
		Switch(2);
	}

	[ContextMenu("Episode 3")]
	private void SwitchEpisode3()
	{
		Switch(3);
	}

	[ContextMenu("Episode 4")]
	private void SwitchEpisode4()
	{
		Switch(4);
	}

	[ContextMenu("Episode 5")]
	private void SwitchEpisode5()
	{
		Switch(5);
	}
}
