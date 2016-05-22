using UnityEngine;

public class BulletBehavior : MonoBehaviour {

	public float speed = 10;
	public int damage;
	public GameObject target;
	public Vector3 startPosition;
	public Vector3 targetPosition;
	
	protected float distance;
	protected float startTime;
	
	private GameManagerBehavior gameManager;

	// Use this for initialization
	protected void Start () {
		startTime = Time.time;
		distance = Vector3.Distance (startPosition, targetPosition);
		GameObject gm = GameObject.Find("GameManager");
		gameManager = gm.GetComponent<GameManagerBehavior>();	
	}
	
	// Update is called once per frame
	protected virtual void Update () {
		// 1 
		float timeInterval = Time.time - startTime;
		gameObject.transform.position = Vector3.Lerp(startPosition, targetPosition, timeInterval * speed / distance);
		
		// 2 
		if (gameObject.transform.position.Equals(targetPosition)) {
			if (target != null) {
				// 3
				Transform healthBarTransform = target.transform.FindChild("HealthBar");
				HealthBar healthBar = 
					healthBarTransform.gameObject.GetComponent<HealthBar>();
				healthBar.currentHealth -= Mathf.Max(damage, 0);
				// 4
				if (healthBar.currentHealth <= 0) {
					Destroy(target);
                    
                    if (AppData.isSoundOn){
					   AudioSource audioSource = target.GetComponent<AudioSource>();
					   AudioSource.PlayClipAtPoint(audioSource.clip, transform.position);
                    }
					gameManager.Gold += 50;
                    gameManager.Raise();
				}
			}
			Destroy(gameObject);
		}	
	}
}
