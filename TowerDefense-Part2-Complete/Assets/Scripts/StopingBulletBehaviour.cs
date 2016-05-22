using UnityEngine;

public class StopingBulletBehaviour : BulletBehavior {

	override protected void Update () {
       if (gameObject.transform.position.Equals(targetPosition)) {
           
       }
       float timeInterval = Time.time - startTime;
		gameObject.transform.position = Vector3.Lerp(startPosition, targetPosition, timeInterval * speed / distance);
		
		// 2 
		if (gameObject.transform.position.Equals(targetPosition)) {
			if (target != null) {
				MoveEnemy enemy = target.GetComponent<MoveEnemy>();
                enemy.CurrentSpeed /= 2;
			}
			Destroy(gameObject);
		}	
	}
}
