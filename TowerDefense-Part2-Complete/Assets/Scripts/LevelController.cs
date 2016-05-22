using UnityEngine;
using System.Collections.Generic;

public class LevelController : MonoBehaviour {
	public GameObject[] Levels;
	public GameObject Locked;
	public GameObject Current;
	private int LevelIndex;
	private bool IsOpen;
	
	public void Init(int levelIndex,bool isOpen) 
	{
		LevelIndex = levelIndex;
		IsOpen = isOpen;
		Locked.SetActive(false);
		foreach (GameObject item in Levels)
		{
			item.SetActive(false);
		}
		if (isOpen)
			Current = Levels[levelIndex];
		else
			Current = Locked;
		Current.SetActive(true);
	}

	public void OnClick() 
	{
		if (IsOpen)
		{
			SoundScript.Instance.MakeSelectSound();
			AppData.CurrentLevel = LevelIndex;
			AppData.CurrentScore = 0;
			
			Application.LoadLevel(AppData.LEVEL_NAME+LevelIndex);
		}
	}
	
	
}
