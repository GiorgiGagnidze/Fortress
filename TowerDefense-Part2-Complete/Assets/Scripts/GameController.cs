using UnityEngine;

public class GameController : MonoBehaviour {
	public LevelController[] Levels;
	private string lastTooltip = "";
	private string beforeLastTooltip = "";

	// Use this for initialization
	void Start () {
		for (int i = 0; i < AppData.NUMBER_OF_LEVELS; i++)
		{
			if (i > AppData.LastLevel)
				Levels[i].Init(i,false);
			else
				Levels[i].Init(i,true);
		}
		
	}
	
	void OnGUI() 
	{
		const int buttonHeight = 35;
		const int menuButtonWidth = 200;
	
		// Determine the button's place on screen
		// Center in X, 2/3 of the height in Y
		GUIStyle myButtonStyle = new GUIStyle(GUI.skin.button);
		myButtonStyle.fontSize = 20;
			
		// Set color for selected and unselected buttons
		myButtonStyle.normal.textColor = Color.yellow;
		myButtonStyle.hover.textColor = Color.blue;
		
		Rect menuRect = new Rect(
			Screen.width/2-menuButtonWidth/2,
			(Screen.height/4)*3,
			menuButtonWidth,
			buttonHeight
			);
		if(GUI.Button(menuRect,new GUIContent("Main Menu", "kbutton"),myButtonStyle))
		{
			SoundScript.Instance.MakeSelectSound();
			Destroy(GameObject.Find("Sound"));
			Application.LoadLevel("Menu");
		}
		
		string tool = GUI.tooltip;
		if(tool.Contains("button") && tool != lastTooltip && tool != beforeLastTooltip){
			if (SoundScript.Instance!= null)
				SoundScript.Instance.MakeHoverSound();
		}
		beforeLastTooltip = lastTooltip;
		lastTooltip =tool; 
	}
}
