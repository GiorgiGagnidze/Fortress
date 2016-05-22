using UnityEngine;
using UnityEngine.UI;

public class GameManagerBehavior : MonoBehaviour {

	public Text goldLabel;
	private int gold;
    
    public Text scoreLabel;
    
    private AudioSource music;
    
    private bool isPaused = false;
	
	private string lastTooltip = "";
	private string beforeLastTooltip = "";
    
    public int maxWaves;
	public int Gold {
  		get { return gold; }
  		set {
			gold = value;
    		goldLabel.GetComponent<Text>().text = "GOLD: " + gold;
		}
	}

	public Text waveLabel;
	public GameObject[] nextWaveLabels;

	public bool gameOver = false;

	private int wave;
	public int Wave {
		get { return wave; }
		set {
			wave = value;
			if (!gameOver) {
				for (int i = 0; i < nextWaveLabels.Length; i++) {
					nextWaveLabels[i].GetComponent<Animator>().SetTrigger("nextWave");
				}
			}
			waveLabel.text = "WAVE: " + (wave + 1) + "/" + maxWaves;
		}
	}

	public Text healthLabel;
	public GameObject[] healthIndicator;

	private int health;
	public int Health {
		get { return health; }
		set {
			// 1
			if (value < health) {
				Camera.main.GetComponent<CameraShake>().Shake();
			}
			// 2
			health = value;
			healthLabel.text = "HEALTH: " + health;
			// 2
			if (health <= 0 && !gameOver) {
				gameOver = true;
                GameObject gameText = GameObject.FindGameObjectWithTag ("GameWon");
			    gameText.GetComponent<Animator>().SetBool("gameOver", false);
				GameObject gameOverText = GameObject.FindGameObjectWithTag ("GameOver");
				gameOverText.GetComponent<Animator>().SetBool("gameOver", true);
			}
			// 3 
			for (int i = 0; i < healthIndicator.Length; i++) {
				if (i < Health) {
					healthIndicator[i].SetActive(true);
				} else {
					healthIndicator[i].SetActive(false);
				}
			}
		}
	}

	// Use this for initialization
	void Start () {
		Gold = 1000;
		Wave = 0;
		Health = 5;
        scoreLabel.text = "SCORE: "+AppData.CurrentScore;
        music=Camera.main.GetComponent<AudioSource>();
        if (AppData.isMusicOn)
            music.Play();
	}
	
	public void Raise () {
       AppData.CurrentScore+=AppData.ONE_SCORE;
	   scoreLabel.text = "SCORE: "+AppData.CurrentScore;
	}
    
    public void Fall() {
        if (AppData.CurrentScore > 0) {
            AppData.CurrentScore -=AppData.ONE_SCORE;
            scoreLabel.text = "SCORE: "+AppData.CurrentScore;
        }
    }
    
    void OnGUI()
	{
		const int buttonWidth = 100;
		const int buttonHeight = 35;
		const int menuButtonWidth = 220;
	
		// Determine the button's place on screen
		// Center in X, 2/3 of the height in Y
		GUIStyle myStyle = new GUIStyle();
		myStyle.fontSize = 30;
		myStyle.normal.textColor = Color.yellow;
     	myStyle.hover.textColor = Color.magenta;
		
		Rect buttonRect = new Rect(
			Screen.width/2 - buttonWidth/2,
			0,
			buttonWidth,
			buttonHeight
			);
		Rect resumeRect = new Rect(
			Screen.width/2 - menuButtonWidth/2,
			Screen.height/2 - 2*buttonHeight,
			menuButtonWidth,
			buttonHeight
			);
		Rect soundRect = new Rect(
			Screen.width/2 - menuButtonWidth/2,
			Screen.height/2 - buttonHeight,
			menuButtonWidth,
			buttonHeight
			);
		Rect musicRect = new Rect(
			Screen.width/2 - menuButtonWidth/2,
			Screen.height/2,
			menuButtonWidth,
			buttonHeight
			);
		Rect menuRect = new Rect(
			Screen.width/2 - menuButtonWidth/2,
			Screen.height/2 + buttonHeight,
			menuButtonWidth,
			buttonHeight
			);
		
			
		GUIStyle myButtonStyle = new GUIStyle(GUI.skin.button);
		myButtonStyle.fontSize = 30;
			
		// Set color for selected and unselected buttons
		myButtonStyle.normal.textColor = Color.yellow;
		myButtonStyle.hover.textColor = Color.blue;
		
		if (isPaused)
		{
			if(GUI.Button(buttonRect,new GUIContent("Menu", "kbutton"),myButtonStyle))
			{
				Time.timeScale = 1;
				SoundScript.Instance.MakePauseSound();
				isPaused = false;
			}
			if(GUI.Button(resumeRect,new GUIContent("Resume Game", "fbutton"),myButtonStyle))
			{
				Time.timeScale = 1;
				SoundScript.Instance.MakePauseSound();
				isPaused = false;
			}
			if(GUI.Button(menuRect,new GUIContent("Main Menu", "ebutton"),myButtonStyle))
			{
				Time.timeScale = 1;
				SoundScript.Instance.MakeSelectSound();
				isPaused = false;
				Destroy(GameObject.Find("Sound"));
                AppData.CurrentScore = 0;
				Application.LoadLevel("Menu");
			}
			if (AppData.isMusicOn)
			{
				if(GUI.Button(musicRect,new GUIContent("Music: Off", "dbutton"),myButtonStyle))
				{
					AppData.isMusicOn = false;
                    music.Stop();
				}
					
			} else {
				if(GUI.Button(musicRect,new GUIContent("Music: On", "cbutton"),myButtonStyle)) 
				{
					SoundScript.Instance.MakeSelectSound();
					AppData.isMusicOn = true;
                    music.Play();
				}
					
			}
			if (AppData.isSoundOn)
			{
				if(GUI.Button(soundRect,new GUIContent("Sound: Off", "bbutton"),myButtonStyle))
					AppData.isSoundOn = false;
			} else {
				if(GUI.Button(soundRect,new GUIContent("Sound: On", "abutton"),myButtonStyle)) {
					AppData.isSoundOn = true;
					SoundScript.Instance.MakeSelectSound();
				}
			}
		} else 
		{
			if(GUI.Button(buttonRect,new GUIContent("Menu", "kbutton"),myButtonStyle))
			{
				SoundScript.Instance.MakePauseSound();
				isPaused = true;
				Time.timeScale = 0;
			}
		}
			
		
		string tool = GUI.tooltip;
		if(tool.Contains("button") && tool != lastTooltip && tool != beforeLastTooltip){
			SoundScript.Instance.MakeHoverSound();
		}
		beforeLastTooltip = lastTooltip;
		lastTooltip =tool;     
	}
}
