using UnityEngine;
using System.Collections;

public class AutoLogin : MonoBehaviour {
	
	private SwitchMenu switchM;

	// Use this for initialization
	void Start () {
		tryLogin ();
	}

	/**
	 * Checks if there are Login-Information saved on the phone.
	 * If yes --> AutoLogin and switch to StartMenu
	 */
	void tryLogin() {
		string username = PlayerPrefs.GetString ("Username");
		string password = PlayerPrefs.GetString ("Password");
		
		if (!string.IsNullOrEmpty (username) && !string.IsNullOrEmpty (password))
		{

			GameObject go = GameObject.FindWithTag ("Client");
			Client client = (Client)go.GetComponent (typeof(Client));
			client.RequestLogin (username, password);

			StartCoroutine (CheckLoginStatus(client));
		}
	}

	IEnumerator CheckLoginStatus(Client client) {
		while (true) {

			if (client.loggedIn) {

				Debug.Log ("AutoLogin ok, switch now");

				break;
			}
			yield return null;
		}
		
		yield return null;
	}
}
