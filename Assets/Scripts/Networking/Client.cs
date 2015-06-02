using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Client : MonoBehaviour {

	string remoteIP = "127.0.0.1";
	int remotePort = 25000;
	NetworkView networkView;
	NetworkPlayer player;
	List<string> missions;
	bool missionOn = false;
	public bool loggedIn = false;
	public string username;
	public string password;
	string loginInfo = "Nicht eingeloggt.";

	/// <summary>
	/// Start this instance.
	/// </summary>
	void Awake ()
	{
		networkView = GetComponent <NetworkView> ();
		missions = new List<string> ();
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
	void disconnectFromServer() {
		//Network.Disconnect();
		missionOn = !missionOn;
		missions.Clear ();
		loggedIn = false;
		loginInfo = "Ausgeloggt.";
	}


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

	[RPC]
	public void CheckLogin(string username, string password)
	{
		player = Network.player;
		networkView.RPC ("AuthorizeLogin", RPCMode.Server, username, password, player);
	}

	[RPC]
	void Login(bool authorized)
	{
		if (authorized) {
			loginInfo = "Eingeloggt als:" + username;
			loggedIn = true;
		} else {
			disconnectFromServer();
		}
	}

	//Server RPC functions
	[RPC]
	void GetMissions (NetworkPlayer sender)
	{
		//empty
	}

	[RPC]
	void BuildMap ()
	{

	}

	[RPC]
	void AuthorizeLogin (string username, string password, NetworkPlayer player)
	{

	}

}
