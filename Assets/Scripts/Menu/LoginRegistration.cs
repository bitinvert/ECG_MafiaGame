using UnityEngine;
using System.Collections;
using UnityEngine.UI;


/**
 * Anm.: Still missing:
 * 	     - Meldung, dass Logindaten nicht gefunden wurden
 *       - Meldung, dass Registrierung nicht erfolgreich, da Daten bereits vorhanden
 */
public class LoginRegistration : MonoBehaviour {

	[SerializeField]
	private InputField usernameInputField = null;
	[SerializeField]
	private InputField passwordInputField = null;

	private SwitchMenu switchToMenu;
	private bool loggedInOnce = false;

	void Start()
	{
		switchToMenu = GetComponentInParent<SwitchMenu>();

		TryAutoLogin();
	}
	
	/**
	 * Logout and switch to Login Screen
	 */
	public void Logout() {
		GameObject go = GameObject.FindWithTag ("Client");
		Client client = (Client)go.GetComponent (typeof(Client));

		client.Logout ();

		Debug.Log ("Logout successful");
		switchToMenu.Switch ();
	}

	/**
	 * Checks if there are Login-Information saved on the phone.
	 * If yes --> AutoLogin and switch to StartMenu only when not already logged in
	 * 
	 * Note: We need to check in a coroutine if the client is connected since this function
	 * will be called immediatly after loading the scene; sometimes the connection
	 * will need more time to etablish.
	 */
	public void TryAutoLogin() 
	{
		string username = PlayerPrefs.GetString ("Username");
		string password = PlayerPrefs.GetString ("Password");
		
		if (!string.IsNullOrEmpty (username) && !string.IsNullOrEmpty (password))
		{
			GameObject go = GameObject.FindWithTag ("Client");
			Client client = (Client)go.GetComponent (typeof(Client));

			if(!client.loggedIn && !loggedInOnce)
			{
				StartCoroutine(CheckConnectionStatus(client, username, password));
						
				StartCoroutine (CheckLoginStatus(client));
			}
		}
	}

	/**
	 * As soon as the client connection is etablished, try to request a login with
	 * the saved data.
	 */
	IEnumerator CheckConnectionStatus(Client client, string username, string password) 
	{
		while (true)
		{
			Debug.Log ("AutoLogin as Username: " + username);
			if(client.connected) {
				client.RequestLogin (username, password);
				break;
			}
			yield return null;
		}
		yield return null;
	}

	/**
	 * Get the InputFields values and try to log in. 
	 */
	public void Login() 
	{
		GameObject go = GameObject.FindWithTag ("Client");
		Client client = (Client)go.GetComponent (typeof(Client));
		
		client.RequestLogin (usernameInputField.text, passwordInputField.text);
		
		StartCoroutine (CheckLoginStatus(client));
	}

	/**
	 * If Login successful --> save the data on the phone and switch to StartMenu
	 */
	IEnumerator CheckLoginStatus(Client client) 
	{
		while (true) {
			if (client.loggedIn) 
			{		
				PlayerPrefs.SetString("Username", usernameInputField.text);
				PlayerPrefs.SetString("Password", passwordInputField.text);
				PlayerPrefs.Save();
				
				Debug.Log ("login ok, switch now");
				this.loggedInOnce = true;
				switchToMenu.Switch();
				
				break;
			}
			yield return null;
		}
		
		yield return null;
	}

	/**
	 * Get the InputFields values and try to create a new account
	 */
	public void CreateAcc() 
	{
		GameObject go = GameObject.FindWithTag ("Client");
		Client client = (Client)go.GetComponent (typeof(Client));
		
		client.RequestRegistration (usernameInputField.text, passwordInputField.text);
		
		StartCoroutine (CheckRegistrationStatus (client));
	}

	/**
	 * If Registration successful --> Login now
	 */
	IEnumerator CheckRegistrationStatus(Client client) 
	{
		while (true)
		{
			if(client.registered) 
			{
				Debug.Log ("Registration successful");

				if(client.loggedIn) 
				{
					Debug.Log ("login ok");
					this.loggedInOnce = true;
					switchToMenu.Switch();
				}

				break;
			}
			yield return null;
		}
		yield return null;
	}
}
