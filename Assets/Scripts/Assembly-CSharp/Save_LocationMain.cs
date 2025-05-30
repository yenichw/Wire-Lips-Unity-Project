using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[AddComponentMenu("Functions/Save/Location/Save main location")]
public class Save_LocationMain : MonoBehaviour
{
	[Serializable]
	public class SaveLocationId
	{
		public string idName;

		public UnityEvent eventLoad;

		public GameObject[] destroy;

		public GameObject[] activation;

		public GameObject[] deactivation;

		[Header("After always")]
		public GameObject[] _destroy;

		public GameObject[] _activation;

		public GameObject[] _deactivation;
	}

	private Animator saveIcon;

	private bool canSave;

	public string nameSave;

	public SaveLocationId[] loadSave;

	[Header("Test")]
	public bool useTest;

	public int numberLoad;

	private void Start()
	{
		saveIcon = GameObject.FindWithTag("Interface").gameObject.transform.Find("SaveIcon").GetComponent<Animator>();
		Load();
		StartCoroutine(timeCanSave());
		if (!useTest)
		{
			return;
		}
		loadSave[numberLoad].eventLoad.Invoke();
		for (int i = 0; i < loadSave[numberLoad].destroy.Length; i++)
		{
			if (loadSave[numberLoad].destroy[i] != null)
			{
				UnityEngine.Object.Destroy(loadSave[numberLoad].destroy[i]);
			}
		}
		for (int j = 0; j < loadSave[numberLoad].activation.Length; j++)
		{
			if (loadSave[numberLoad].activation[j] != null)
			{
				loadSave[numberLoad].activation[j].SetActive(value: true);
			}
		}
		for (int k = 0; k < loadSave[numberLoad].deactivation.Length; k++)
		{
			if (loadSave[numberLoad].deactivation[k] != null)
			{
				loadSave[numberLoad].deactivation[k].SetActive(value: false);
			}
		}
		for (int l = 0; l < loadSave.Length; l++)
		{
			for (int m = 0; m < loadSave[l].destroy.Length; m++)
			{
				if (loadSave[l]._destroy[m] != null)
				{
					UnityEngine.Object.Destroy(loadSave[l]._destroy[m]);
				}
			}
			for (int n = 0; n < loadSave[l].activation.Length; n++)
			{
				if (loadSave[l]._activation[n] != null)
				{
					loadSave[l]._activation[n].SetActive(value: true);
				}
			}
			for (int num = 0; num < loadSave[l].deactivation.Length; num++)
			{
				if (loadSave[l]._deactivation[num] != null)
				{
					loadSave[l]._deactivation[num].SetActive(value: false);
				}
			}
		}
	}

	private IEnumerator timeCanSave()
	{
		yield return new WaitForSeconds(3f);
		canSave = true;
	}

	public void Save(int x)
	{
		if (canSave)
		{
			saveIcon.SetTrigger("Save");
		}
	}

	private void Load()
	{
	}
}
