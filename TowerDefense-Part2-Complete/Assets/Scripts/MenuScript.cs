using UnityEngine;

/// <summary>
/// Title screen script
/// </summary>
public class MenuScript : MonoBehaviour
{
  private string lastTooltip = "";
	private string beforeLastTooltip = "";

  void OnGUI()
  {
    const int buttonWidth = 84;
    const int buttonHeight = 35;

    // Determine the button's place on screen
    // Center in X, 2/3 of the height in Y
    Rect buttonRect = new Rect(
          Screen.width / 2 - buttonWidth/2,
          (1 * Screen.height / 2) + (buttonHeight / 2),
          buttonWidth,
          buttonHeight
        );
    Rect exit = new Rect(
          Screen.width / 2 - buttonWidth/2,
          (1 * Screen.height / 2) + 3*(buttonHeight / 2),
          buttonWidth,
          buttonHeight
        );

    // Draw a button to start the game
    if(GUI.Button(buttonRect,new GUIContent("Start!", "kbutton")))
    {
      SoundScript.Instance.MakeSelectSound();
      AppData.CurrentScore = 0;
      Application.LoadLevel("Main");
    }
    if(GUI.Button(exit,new GUIContent("Quit!", "mbutton")))
    {
      SoundScript.Instance.MakeSelectSound();
      Application.Quit();
    }
    
    
    string tool = GUI.tooltip;
		if(tool.Contains("button") && tool != lastTooltip && tool != beforeLastTooltip){
			SoundScript.Instance.MakeHoverSound();
		}
		beforeLastTooltip = lastTooltip;
		lastTooltip =tool; 
  }
}