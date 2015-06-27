using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Client : MonoBehaviour {
	
	private const string roomName = "RoomName";
	private RoomInfo[] roomsList;
	private int maxClients = 2;
	
	private PhotonView photonView;
	
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
		photonView = GetComponent <PhotonView> ();
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
		if (GUI.Button (new Rect(50, 50, 100, 100), "Spielzug beenden")) {
			if (isMyTurn){
				Debug.Log ("Ich war dran." + PhotonNetwork.player.ID);
				PhotonPlayer nextPlayer = PhotonNetwork.player.GetNextFor(playerToMakeTurn);
				playerToMakeTurn = nextPlayer.ID;
				Debug.Log (playerToMakeTurn + " ist als nächstes dran.");
				photonView.RPC ("HandOverTurn", nextPlayer, playerToMakeTurn);
			} else {
				Debug.Log ("Ich war nicht dran. " + playerToMakeTurn + " war dran.");
			}
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
		Debug.Log (PhotonNetwork.playerList.Length);
		if (PhotonNetwork.playerList.Length == 2) {
			string mapFields = System.IO.File.ReadAllText(@"Assets/XmlLevels/TestWith3DModels_singleFields.xml");
			string mapPrefab = System.IO.File.ReadAllText(@"Assets/XmlLevels/TestWith3DModels_prefabHolder.xml");
			playerToMakeTurn = PhotonNetwork.player.ID;
			photonView.RPC ("InstantiateMap", PhotonTargets.All, mapPrefab, mapFields);
		}
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

	void SavePlayerMove (Message message) 
	{
		string timeStamp = DateTime.Now.ToString ();
		string actionType = message.action.ToString();
		float damage = message.damage;
		Vector3[] move = message.movement;

		switch (message.action) 
		{
			case ActionType.ATTACK:
			GameObject attacker = message.involvedCharacters[0];
			GameObject victim = message.involvedCharacters[1];
			photonView.RPC ("LoadAttack", PhotonTargets.Others, timeStamp, attacker, victim, damage, move);
			break;
			case ActionType.MOVEMENT:
			GameObject character = message.involvedCharacters[0];
			photonView.RPC ("LoadMove", PhotonTargets.Others, timeStamp, character, move);
			break;
			//more ActionTypes can follow

		}
	}
	
	/// <summary>
	/// Client instantiates received map from server.
	/// </summary>
	/// <param name="mapFields">xml view of map fields</param>
	/// <param name="mapPrefabs">xml view of map prefabs</param>
	[PunRPC]
	void InstantiateMap (string mapPrefabs, string mapFields)
	{
		levelController.LoadLevel (mapPrefabs, mapFields);
		//The player who opened the game will start
		playerToMakeTurn = 1;
	}

	[PunRPC]
	void HandOverTurn (int player) 
	{
		playerToMakeTurn = player;
	}

	[PunRPC]
	void LoadAttack (String timeStamp, GameObject attacker, GameObject victim, float damage, Vector3[] move) 
	{
		DateTime currentTimeStamp = DateTime.Now;
		DateTime receivedTimeStamp = Convert.ToDateTime (timeStamp);
		TimeSpan diff = currentTimeStamp.Subtract (receivedTimeStamp);
		if (diff.Hours > 24) {
			Debug.Log ("Gegner hat zu lange gebraucht. Du hast gewonnen!");
		} else {
			//playerController.pUnitList.get(attacker).Move(move);
			//playerController.pUnitList.get(attacker).Attack(victim);
			//how to handle die?
		}
	}

	[PunRPC]
	void LoadMove (String timeStamp, GameObject character, Vector3[] move) 
	{
		DateTime currentTimeStamp = DateTime.Now;
		DateTime receivedTimeStamp = Convert.ToDateTime (timeStamp);
		TimeSpan diff = currentTimeStamp.Subtract (receivedTimeStamp);
		if (diff.Hours > 24) {
			Debug.Log ("Gegner hat zu lange gebraucht. Du hast gewonnen!");
		} else {
			//playerController.pUnitList.get(attacker).Move(move);
			//playerController.pUnitList.get(attacker).Attack(victim);
			//how to handle die?
		}
	}
	
	
}