using UnityEngine;
using UnityEngine.Events;

public class Save_CaseStatic : MonoBehaviour
{
	public bool onStart = true;

	public int case1;

	public UnityEvent ifCase1;

	public int case2;

	public UnityEvent ifCase2;

	public int case3;

	public UnityEvent ifCase3;

	private void Start()
	{
		if (onStart)
		{
			if (case1 == GlobalGame.saveCase1)
			{
				ifCase1.Invoke();
			}
			if (case2 == GlobalGame.saveCase2)
			{
				ifCase2.Invoke();
			}
			if (case3 == GlobalGame.saveCase3)
			{
				ifCase3.Invoke();
			}
		}
	}

	public void SaveStatic1(int x)
	{
		GlobalGame.saveCase1 = x;
	}

	public void SaveStatic2(int x)
	{
		GlobalGame.saveCase2 = x;
	}

	public void SaveStatic3(int x)
	{
		GlobalGame.saveCase3 = x;
	}
}
