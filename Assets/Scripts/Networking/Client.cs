using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Client : MonoBehaviour {

	string remoteIP = "127.0.0.1";
	int remotePort = 25000;
	NetworkView networkView;
	//List<string> missions;
	//bool missionOn = false;
	public bool loggedIn = false;
	string username;
	string password;

	/// <summary>
	/// Start this instance.
	/// </summary>
	void Awake ()
	{
		networkView = GetComponent <NetworkView> ();
		//missions = new List<string> ();
		connectToServer ();
		DontDestroyOnLoad (this);
		Application.LoadLevel ("MainMenu");
	}

	/// <summary>
	/// Connects to server.
	/// </summary>
	void connectToServer() {
		Network.Connect(remoteIP, remotePort);
	}

	void OnConnectedToServer() {
		Debug.Log("Connected to server");
	}

	/// <summary>
	/// Disconnects from server.
	/// </summary>
	public void Logout() {
		//Network.Disconnect();
		//missionOn = !missionOn;
		//missions.Clear ();
		loggedIn = false;
	}

	/// <summary>
	/// Client sends registration-request to server.
	/// </summary>
	/// <param name="username">Username.</param>
	/// <param name="password">Password.</param>
	[RPC]
	public void RequestRegistration(string username, string password){
		networkView.RPC ("RegistrateUser", RPCMode.Server, username, password, Network.player);
	}

	/// <summary>
	/// Client sends login-request to server.
	/// </summary>
	/// <param name="username">Username.</param>
	/// <param name="password">Password.</param>
	[RPC]
	public void RequestLogin(string username, string password)
	{
		networkView.RPC ("AuthorizeLogin", RPCMode.Server, username, password, Network.player);
	}
	
	/// <summary>
	/// Login Client.
	/// </summary>
	/// <param name="authorized">If set to <c>true</c> authorized.</param>
	[RPC]
	void Login(bool authorized)
	{
		if (authorized) {
			loggedIn = true;
		} else {
			Logout ();
		}
	}

	/* Not used yet
	/// <summary>
	/// Request missions from server.
	/// </summary>
	[RPC]
	void RequestMissions()
	{
		networkView.RPC ("GetMissions", RPCMode.Server, Network.player);
	}

	/// <summary>
	/// Update missions list with received string.
	/// </summary>
	/// <param name="missionsString">Missions string.</param>
	[RPC]
	void ShowMissions(string missionsString)
	{
		string [] missionArray = missionsString.Split (new char[] {' '});
		missions.Clear ();
		foreach (string s in missionArray) 
		{
			missions.Add (s);
		}
		missionOn = true;
	}

	[RPC]
	void RequestMap()
	{
		networkView.RPC ("BuildMap", RPCMode.Server);
	}
	*/

	//Server RPC functions
	[RPC]
	void RegistrateUser (string username, string password, NetworkPlayer player)
	{
	}

	[RPC]
	void AuthorizeLogin (string username, string password, NetworkPlayer player)
	{
	}
	/* Not used yet
	[RPC]
	void GetMissions (NetworkPlayer sender)
	{
		//empty
	}

	[RPC]
	void BuildMap ()
	{

	}
	*/


}
