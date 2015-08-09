using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InformationText : MonoBehaviour {

	// A reference to the nameField, where the enemy's name is supposed to be
	public Text enemyName;

	/**
	 * Get the enemy's name and set it.
	 */
	void Start () {
		GameObject go = GameObject.FindWithTag ("Client");
		Client client = (Client)go.GetComponent (typeof(Client));
		enemyName.text = client.opponentName;
	}

}
