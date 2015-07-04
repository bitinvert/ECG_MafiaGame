using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SetPlayerName : MonoBehaviour {

	public Text nameField;
	private GameObject client;
	private bool nameSet;

	// Use this for initialization
	void Start () {
		client = GameObject.FindGameObjectWithTag ("Client");
		nameSet = false;
	}
	
	// Update is called once per frame
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
