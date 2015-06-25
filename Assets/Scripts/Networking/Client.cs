using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Client : MonoBehaviour {
	
	private const string roomName = "RoomName";
	private RoomInfo[] roomsList;
	private int maxClients = 2;
	private string txt;
	
	private PhotonView PhotonView;
	
	//public bool loggedIn = false;
	//public bool registered = false;
	//string username;
	//string password;
	//string message;
	public int turnNumber = 1;
	public int playerToMakeTurn;
	public LevelController levelController;	
	//public bool connected = false;
	
	void Awake(){
		//Application.LoadLevel ("MainMenu");
	}
	
	void Start()
	{
		PhotonNetwork.ConnectUsingSettings("0.1"); 	
		PhotonView = GetComponent <PhotonView> ();
		//connectionInfos ();
	}
	
	void OnGUI()
	{
		if (txt != null) {
			GUILayout.Label (txt);
		} else {
			GUILayout.Label ("kein read");
		}
		if (!PhotonNetwork.connected)
		{
			GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
		}
		else if (PhotonNetwork.room == null)
		{
			// Create Room
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
		if (GUILayout.Button ("move")) {
			if (isMyTurn){
				GUILayout.Label ("Ich war dran." + PhotonNetwork.player.ID);
				playerToMakeTurn = PhotonNetwork.player.GetNextFor(playerToMakeTurn).ID;
			} else {
				GUILayout.Label ("Ich war nicht dran." + PhotonNetwork.player.ID);
			}
			GUILayout.Label ("Spieler " + playerToMakeTurn + " ist dran.");
		}
	}
	
	private void twoPlayersCheck(){
		if (PhotonNetwork.countOfPlayersInRooms == 2)
			Application.LoadLevel ("MainMenu1");
	}
	
	public bool isMyTurn {
		get { 
			return playerToMakeTurn == PhotonNetwork.player.ID; 
		}
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
	
	
	void OnJoinedRoom()
	{

		string mapFields = System.IO.File.ReadAllText(@"Assets/XmlLevels/TestWith3DModels_singleFields.xml");
		string mapPrefab = System.IO.File.ReadAllText(@"Assets/XmlLevels/TestWith3DModels_prefabHolder.xml");
		txt = mapFields + mapPrefab;
		PhotonView.RPC ("InstantiateMap", PhotonTargets.All, mapPrefab, mapFields);

	}
	/*
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
	/// Client instantiates received map from server.
	/// </summary>
	/// <param name="mapFields">xml view of map fields</param>
	/// <param name="mapPrefabs">xml view of map prefabs</param>
	[PunRPC]
	void InstantiateMap (string mapPrefabs, string mapFields)
	{
		levelController.LoadLevel (mapPrefabs, mapFields);
		//set randomly who can start
		/*
		if (UnityEngine.Random.Range (0, 2) == 0) {
			playerToMakeTurn = 0; //spieler mit id 0 soll anfangen
		} else {
			playerToMakeTurn = 1;
		}
		*/
		playerToMakeTurn = 1;
	}
	
	
	
}