﻿using UnityEngine;
using System.Collections;

public class Login : MonoBehaviour {

	public bool loggedIn;

	public void Start() {
		loggedIn = false;
	}

	public void TestLogin() {
		GameObject go = GameObject.FindWithTag ("Client");
		Client client = (Client)go.GetComponent (typeof(Client));
		client.CheckLogin ("jane", "jane");
		StartCoroutine (CheckLoginStatus(client));
	}

	IEnumerator CheckLoginStatus(Client client) {
		while (true) {
			if (client.loggedIn) {
				Debug.Log ("yay");
				this.loggedIn = true;
				break;
			}
			yield return null;
		}

		yield return null;
	}
}
