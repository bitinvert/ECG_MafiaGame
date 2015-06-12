using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Client : MonoBehaviour {

	string remoteIP = "127.0.0.1";
	int remotePort = 25000;
	NetworkView networkView;
	//List<string> missions;
	string testMission = "bankraub";
	//bool missionOn = false;
	public bool loggedIn = false;
	string username;
	string password;
	int turnValue;
	public LevelController levelController;
	
	/// <summary>
	/// Start this instance.
	/// </summary>
	void Awake ()
	{
		networkView = GetComponent <NetworkView> ();
		//missions = new List<string> ();
		connectToServer ();
		//DontDestroyOnLoad (this);
		//Application.LoadLevel ("MainMenu");
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
	/// <param name="username">Username of client</param>
	/// <param name="password">Password of client</param>
	[RPC]
	public void RequestRegistration(string username, string password){
		networkView.RPC ("RegistrateUser", RPCMode.Server, username, password, Network.player);
	}
	
	/// <summary>
	/// Client sends login-request to server.
	/// </summary>
	/// <param name="username">Username of client</param>
	/// <param name="password">Password of client</param>
	[RPC]
	public void RequestLogin(string username, string password)
	{
		networkView.RPC ("AuthorizeLogin", RPCMode.Server, username, password, Network.player);
	}
	
	/// <summary>
	/// Login Client.
	/// </summary>
	/// <param name="authorized">Indicates if user login was successful or not.</param>
	[RPC]
	void Login(bool authorized)
	{
		if (authorized) {
			loggedIn = true;
		} else {
			Logout ();
		}
	}
	
	/// <summary>
	/// Client sends request to server to join a selected mission.
	/// </summary>
	[RPC]
	void JoinMission ()
	{
		networkView.RPC ("RegisterMission", RPCMode.Server, Network.player, testMission);
	}
	
	/// <summary>
	/// Client instantiates received map from server.
	/// </summary>
	/// <param name="mapFields">xml view of map fields</param>
	/// <param name="mapPrefabs">xml view of map prefabs</param>
	[RPC]
	void InstantiateMap (string mapPrefabs, string mapFields)
	{
		levelController.LoadLevel (mapPrefabs, mapFields);
	}

	[RPC]
	void ReadyToStart ()
	{
		//After placing all your pieces on your gameboard you're ready to start the game
		networkView.RPC ("DrawBeginner", RPCMode.Server, Network.player, testMission);
	}

	[RPC]
	void SendMove ()
	{
		//After making your actions, pack them in a message and send it to the server
		networkView.RPC ("CheckMove", RPCMode.Server, turnValue);
	}

	[RPC]
	void SetTurnValue (int value)
	{
		turnValue = value;
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
	
	[RPC]
	void RegisterMission(NetworkPlayer sender, string missionName)
	{
	}

	[RPC]
	void DrawBeginner (NetworkPlayer sender, string missionName)
	{
	}

	[RPC]
	void CheckMove (int turnValue)
	{
	}

	/* Not used yet
	[RPC]
	void GetMissions (NetworkPlayer sender)
	{
		//empty
	}

	*/
	
	/// <summary>
	/// Test GUI.
	/// </summary>
	void OnGUI ()
	{
		if (GUILayout.Button ("Join "+ testMission)) 
		{
			JoinMission ();
		}
		if (GUILayout.Button ("Start")) {
			ReadyToStart ();
		}
		GUILayout.Label (turnValue+"");

		if (turnValue != 0)
		{
			if (GUILayout.Button ("Move"))
			{
				SendMove ();
			}
		}
	}
	
	
}
