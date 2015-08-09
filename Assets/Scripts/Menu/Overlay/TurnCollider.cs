using UnityEngine;
using System.Collections;

public class TurnCollider : MonoBehaviour {

	private Client mClientPlayer;

	// A reference to the collider
	public GameObject coll;

	void Start () {
		mClientPlayer = Object.FindObjectOfType(typeof(Client)) as Client;
	}
	
	/**
	 * Check periodically whether it's the player's turn or not.
	 * If it's not the player's turn, activate a Collider to ensure the player
	 * not being able to make any move.
	 * Deactivate the Collider if it's the player's turn.
	 */
	void Update () {
		if(!mClientPlayer.IsMyTurn) {
			coll.SetActive(true);
		} else {
			coll.SetActive(false);
		}
	}
}
