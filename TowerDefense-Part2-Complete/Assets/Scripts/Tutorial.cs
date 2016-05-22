using UnityEngine;

public class Tutorial : MonoBehaviour {

	public void DisplayTutorial() {
		gameObject.GetComponent<Animator> ().SetTrigger ("displayTutorial");
	}
}
