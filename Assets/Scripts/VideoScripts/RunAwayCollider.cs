using UnityEngine;
using System.Collections;

public class RunAwayCollider : MonoBehaviour {
	

	void OnTriggerEntered (Collider other) {
		Debug.Log ("Run run away");
		this.gameObject.active = false;
	}
}
