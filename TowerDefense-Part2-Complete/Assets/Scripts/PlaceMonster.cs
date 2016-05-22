using UnityEngine;

public class PlaceMonster : MonoBehaviour {

	public GameObject monsterPrefab;
    
    public GameObject monster0;
    public GameObject monster1;
    public GameObject monster2;
	private GameObject monster;
	private GameManagerBehavior gameManager;

	// Use this for initialization
	void Start () {
		gameManager = GameObject.Find("GameManager").GetComponent<GameManagerBehavior>();
	}
	
	// Update is called once per frame
	public void OnValueChanged(int index) {
	   switch (index)
       {
           case 0:
                monsterPrefab = monster0;
                break;
           case 1:
                monsterPrefab = monster1;
                break;
           case 2:
                monsterPrefab = monster2;
                break;
       }
	}
	
	private bool canPlaceMonster() {
		int cost = monsterPrefab.GetComponent<MonsterData> ().levels[0].cost;
		return monster == null && gameManager.Gold >= cost;
	}
	
	//1
	void OnMouseUp () {
  		//2
		if (canPlaceMonster ()) {
	    	//3
		    monster = (GameObject) Instantiate(monsterPrefab, transform.position, Quaternion.identity);
		    //4
            if (AppData.isSoundOn){
    		     AudioSource audioSource = gameObject.GetComponent<AudioSource>();
			     audioSource.PlayOneShot(audioSource.clip);
            }
            
			gameManager.Gold -= monster.GetComponent<MonsterData>().CurrentLevel.cost;
		} else if (canUpgradeMonster()) {
			monster.GetComponent<MonsterData>().increaseLevel();
            
            if (AppData.isSoundOn){
			     AudioSource audioSource = gameObject.GetComponent<AudioSource>();
			     audioSource.PlayOneShot(audioSource.clip);
            }

			gameManager.Gold -= monster.GetComponent<MonsterData>().CurrentLevel.cost;
		}
	}

	private bool canUpgradeMonster() {
		if (monster != null) {
			MonsterData monsterData = monster.GetComponent<MonsterData> ();
			MonsterLevel nextLevel = monsterData.getNextLevel();
			if (nextLevel != null) {
				return gameManager.Gold >= nextLevel.cost;
 			}
  		}
		return false;
	}
}
