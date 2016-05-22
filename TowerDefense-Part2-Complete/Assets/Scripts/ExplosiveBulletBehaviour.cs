using UnityEngine;
using System.Collections.Generic;

public class ExplosiveBulletBehaviour : BulletBehavior {
	public GameObject bulletPrefab;
	// Update is called once per frame
	override protected void Update () {
	   base.Update();
       if (gameObject.transform.position.Equals(targetPosition)) {
           foreach (GameObject enemy in enemiesInRange) {
               Shoot(enemy.GetComponent<Collider2D>());
           }
           Vector3 v1 = new Vector3(transform.position.x+2,transform.position.y,transform.position.z);
           Vector3 v2 = new Vector3(transform.position.x,transform.position.y+2,transform.position.z);
           Vector3 v3 = new Vector3(transform.position.x-2,transform.position.y,transform.position.z);
           Vector3 v4 = new Vector3(transform.position.x,transform.position.y-2,transform.position.z);
           ShootParticle(v1);
           ShootParticle(v2);
           ShootParticle(v3);
           ShootParticle(v4);
       }
	}
    
    public List<GameObject> enemiesInRange;
    
    void OnEnemyDestroy (GameObject enemy) {
		enemiesInRange.Remove (enemy);
	}
	
	void OnTriggerEnter2D (Collider2D other) {
		if (other.gameObject.tag.Equals("Enemy")) {
			enemiesInRange.Add(other.gameObject);
			EnemyDestructionDelegate del =
				other.gameObject.GetComponent<EnemyDestructionDelegate>();
			del.enemyDelegate += OnEnemyDestroy;
		}
	}
	
	void OnTriggerExit2D (Collider2D other) {
		if (other.gameObject.tag.Equals("Enemy")) {
			enemiesInRange.Remove(other.gameObject);
			EnemyDestructionDelegate del =
				other.gameObject.GetComponent<EnemyDestructionDelegate>();
			del.enemyDelegate -= OnEnemyDestroy;
		}
	}
    
    void Shoot(Collider2D target) {
		// 1 
		Vector3 startPosition = gameObject.transform.position;
		Vector3 targetPosition = target.transform.position;
		startPosition.z = bulletPrefab.transform.position.z;
		targetPosition.z = bulletPrefab.transform.position.z;
		
		// 2 
		GameObject newBullet = (GameObject)Instantiate (bulletPrefab);
		newBullet.transform.position = startPosition;
		BulletBehavior bulletComp = newBullet.GetComponent<BulletBehavior>();
		bulletComp.target = target.gameObject;
		bulletComp.startPosition = startPosition;
		bulletComp.targetPosition = targetPosition;

	}
    
    void ShootParticle(Vector3 targetPosition) {
		Vector3 startPosition = gameObject.transform.position;
		startPosition.z = bulletPrefab.transform.position.z;
		targetPosition.z = bulletPrefab.transform.position.z;
		// 2 
		GameObject newBullet = (GameObject)Instantiate (bulletPrefab);
		newBullet.transform.position = startPosition;
		BulletBehavior bulletComp = newBullet.GetComponent<BulletBehavior>();
		bulletComp.startPosition = startPosition;
		bulletComp.targetPosition = targetPosition;

	}
}
