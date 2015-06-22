using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Client : MonoBehaviour {
	
	private const string roomName = "RoomName";
	private RoomInfo[] roomsList;
	private int maxClients = 2;

	private PhotonView PhotonView;	
	//List<string> missions;
	string testMission = "bankraub";
	//bool missionOn = false;
	public bool loggedIn = false;
	public bool registered = false;
	string username;
	string password;
	string message;
	int turnValue;
	public LevelController levelController;	
	public bool connected = false;
	
	void Awake(){
		//Application.LoadLevel ("MainMenu");
	}
	
	void Start()
	{
		PhotonNetwork.ConnectUsingSettings("0.1"); 	
		//connectionInfos ();
	}
	
	void OnGUI()
	{
		if (!PhotonNetwork.connected)
		{
			GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
		}
		else if (PhotonNetwork.room == null)
		{
			// Create Room
			Debug.Log("Room Request");
			if (GUI.Button(new Rect(100, 100, 250, 100), "Create Game")){
				//RoomOptions roomOptions = new RoomOptions() { isVisible = false, maxPlayers = 2 };
				//PhotonNetwork.CreateRoom(roomName + Guid.NewGuid().ToString("N"), true, true, 5);
				PhotonNetwork.CreateRoom(roomName, true, true, 2);

				//connectionInfos ();
				GUILayout.Label("Waiting for Opponent");
			}
			
			// Join Room
			if (roomsList != null)
			{
				for (int i = 0; i < roomsList.Length; i++)
				{
					if (GUI.Button(new Rect(100, 250 + (110 * i), 250, 100), "Join Game" + roomsList[i].name)){
						PhotonNetwork.JoinRoom(roomsList[i].name);
						connectionInfos ();
					}
				}
			}
		}
		connectionInfos ();
	}

	private void twoPlayersCheck(){
		if (PhotonNetwork.countOfPlayersInRooms == 2)
			Application.LoadLevel ("MainMenu1");
	}
	
	private void connectionInfos(){
		showServerInformation(); 	
		showClientInformation();
	}
	
	private void showClientInformation(){
		//GUILayout.Label("Clients: " + Network.connections.Length + "/" + maxClients);
		GUILayout.Label("Clients in Lobby: " + PhotonNetwork.countOfPlayers + "/" + maxClients);
		foreach(NetworkPlayer p in Network.connections){
			GUILayout.Label(" Player from ip/port: " + p.ipAddress + "/" + p.port);
		}
	}
	
	private void showServerInformation(){
		GUILayout.Label("IP: " + Network.player.ipAddress + " Port: " + Network.player.port);

	}
	
	void OnReceivedRoomListUpdate()
	{
		roomsList = PhotonNetwork.GetRoomList();
		Debug.Log ("OnReceivedRoomListUpdate()");
	}

	/*
	void OnJoinedRoom()
	{
		Debug.Log("Connected to Room");
		PhotonNetwork.isMessageQueueRunning = false;
		Application.LoadLevel ("MainMenu1");		
	}

	void OnLevelWasLoaded( int level )
	{
		Debug.Log( "OnLevelWasLoaded: " + Application.loadedLevelName );
		
		//Resume the Photon message queue so we get all the updates.
		PhotonNetwork.isMessageQueueRunning = true;
		
		//Time is frozen at the end of a round, so make sure that we resume it when we load a new level
		Time.timeScale = 1f;
	}
	*/

	/// <summary>
	/// Client sends registration-request to server.
	/// </summary>
	/// <param name="username">Username of client</param>
	/// <param name="password">Password of client</param>
	[RPC]
	public void RequestRegistration(string username, string password){
		//networkView.RPC ("RegisterUser", RPCMode.Server, username, password, Network.player);
	}
	
	/// <summary>
	/// Client sends login-request to server.
	/// </summary>
	/// <param name="username">Username of client</param>
	/// <param name="password">Password of client</param>
	[RPC]
	public void RequestLogin(string username, string password)
	{
		//networkView.RPC ("AuthorizeLogin", RPCMode.Server, username, password, Network.player);
	}
	
	/// <summary>
	/// Register a user.
	/// </summary>
	/// <param name="authorized">indicates if register request was successful or not</param>
	[RPC]
	void Register (bool authorized, string message)
	{
		if (authorized) {
			registered = true;
			loggedIn = true;
			this.message = message;
		} else {
			this.message = message;
		}
	}
	/// <summary>
	/// Login Client.
	/// </summary>
	/// <param name="authorized">Indicates if user login was successful or not.</param>
	[RPC]
	void Login(bool authorized, string message)
	{
		if (authorized) {
			loggedIn = true;
			this.message = message;
		} else {
			this.message = message;
		}
	}
	
	/// <summary>
	/// Client sends request to server to join a selected mission.
	/// </summary>
	[RPC]
	void JoinMission ()
	{
		//networkView.RPC ("RegisterMission", RPCMode.Server, Network.player, testMission);
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
		//networkView.RPC ("DrawBeginner", RPCMode.Server, Network.player, testMission);
	}
	
	[RPC]
	void SendMove ()
	{
		//After making your actions, pack them in a message and send it to the server
		//etworkView.RPC ("CheckMove", RPCMode.Server, turnValue);
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
	void RegisterUser (string username, string password, NetworkPlayer player)
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

	[RPC]
	void GetMissions (NetworkPlayer sender)
	{
		//empty
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

	/*
	void OnGUI ()
	{
		GUILayout.Label (message);
		if (GUILayout.Button ("registrate")){
			RequestRegistration ("test", "test");
		}
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

	
	}*/
}