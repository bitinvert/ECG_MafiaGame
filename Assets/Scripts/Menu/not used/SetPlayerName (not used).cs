using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SetPlayerName : MonoBehaviour {
	
	private GameObject client;

	// A reference to the nameField, where the enemy's name is supposed to be
	public Text nameField;

	// A bool to make sure, that the name is only set once
	private bool nameSet;
	
	void Start () {
		client = GameObject.FindGameObjectWithTag ("Client");
		nameSet = false;
	}
	
	/**
	 * Get the enemy's name and set it.
	 */
	void Update () {
		if ((client == null) && (!nameSet)) {

			client = GameObject.FindGameObjectWithTag ("Client");

		} else if(!nameSet)
		{
			Client c = client.GetComponent<Client>();
			nameField.text = c.user.username;
			nameSet = true;
		}
	}
}
