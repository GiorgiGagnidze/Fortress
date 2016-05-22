using UnityEngine;
using System.Collections.Generic;

public static class AppData {
	public static int CurrentLevel {get; set;}
	public static int CurrentScore {get; set;}

	public static bool isSoundOn = true;
	public static bool isMusicOn = true;
	
	private static int lastLevel = 0;
	public static int LastLevel
	{
		get 
		{
			if (lastLevel == 0 && PlayerPrefs.HasKey(PREFS_LEVEL_KEY))
			{
					lastLevel = PlayerPrefs.GetInt(PREFS_LEVEL_KEY);
			}
			return lastLevel;
		}
		set 
		{
			lastLevel = value;
			PlayerPrefs.SetInt(AppData.PREFS_LEVEL_KEY, value);
		}
	}

	public const string PREFS_LEVEL_KEY = "lvl";
	public const int NUMBER_OF_LEVELS = 3;
	public const int ONE_SCORE = 10;
	public const string LEVEL_NAME = "GameScene";
}
