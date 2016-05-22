using UnityEngine;

public class GameOver : MonoBehaviour {
    
    public bool toContinue;
	// Use this for initialization
	void RestartLevel () {
        if (toContinue){
            AppData.CurrentLevel++;
            if (AppData.LastLevel != AppData.NUMBER_OF_LEVELS-1)
                AppData.LastLevel++;
            if (AppData.CurrentLevel == AppData.NUMBER_OF_LEVELS)
            {
                AppData.CurrentLevel = 0;
                AppData.CurrentScore = 0;
                SoundScript.Instance.MakeSelectSound();
                Destroy(GameObject.Find("Sound"));
                Application.LoadLevel ("Menu");
            } else {
                Application.LoadLevel (AppData.LEVEL_NAME+AppData.CurrentLevel);
            }
        } else {
            Application.LoadLevel (Application.loadedLevel);
        }
	}

}
