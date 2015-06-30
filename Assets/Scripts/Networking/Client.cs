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

	public Dictionary <string, Mission> missions { get { return _missions; } }
	private Dictionary <string, Mission> _missions;

	public User user { get { return _user; } }
	private User _user;

	public int turnNumber = 1;
	public int playerToMakeTurn;

	public LevelController levelController;	
	public PlayerController playerController;
	//public bool connected = false;
	//public bool map;

	void Awake(){
		Application.LoadLevel ("MainMenu1");
		DontDestroyOnLoad (this);
	}

	void Start()
	{
		PhotonNetwork.ConnectUsingSettings("0.1"); 	
		photonView = GetComponent <PhotonView> ();
		_missions = new Dictionary<string, Mission> ();
		TestData ();
		//connectionInfos ();
	}

	void OnLevelWasLoaded (int level) {
		//TODO
	}
	/*
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
		if (user != null) {
			GUI.Label (new Rect (100, 100, 200, 200), "Hallo " + user.username);
		}
	}
	*/

	/// <summary>
	/// Create a new room(game) for a selected mission.
	/// </summary>
	/// <param name="missionName">Will be used as the game (room) name.</param>
	public void CreateMission (string missionName) {
		RoomOptions roomOptions = new RoomOptions() { isVisible = true, isOpen = true, maxPlayers = 2 };
		if (PhotonNetwork.JoinOrCreateRoom (missionName, roomOptions, TypedLobby.Default)) {
			Debug.Log ("game created");
		}
	}

	/// <summary>
	/// Joins a mission given by its name.
	/// </summary>
	/// <param name="missionName">Name of the mission.</param>
	public void JoinMission (string missionName) {
		PhotonNetwork.JoinRoom(missionName);
	}

	public bool CheckTwoPlayers {
		get { 
			return PhotonNetwork.playerList.Length == 2;
		}
	}
	
	public bool IsMyTurn {
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
		if (CheckTwoPlayers) {
			Debug.Log ("map instantiate");
			TextAsset mapPrefab = (TextAsset)Resources.Load("TestWith3DModels_prefabHolder", typeof(TextAsset));
			TextAsset mapFields = (TextAsset)Resources.Load("TestWith3DModels_singleFields", typeof(TextAsset));
			//string strmapFields = System.IO.File.ReadAllText(@"Assets/Resources/TestWith3DModels_singleFields.xml");
			//string strmapPrefab = System.IO.File.ReadAllText(@"Assets/Resources/TestWith3DModels_prefabHolder.xml");

			photonView.RPC ("InstantiateMap", PhotonTargets.AllViaServer, mapPrefab.text, mapFields.text);
			//photonView.RPC ("InstantiateMap", PhotonTargets.All, strmapPrefab, strmapFields);
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

	void playerChange () {
		if (IsMyTurn) {
			PhotonPlayer nextPlayer = PhotonNetwork.player.GetNextFor(playerToMakeTurn);
			playerToMakeTurn = nextPlayer.ID;
			photonView.RPC ("HandOverTurn", nextPlayer, playerToMakeTurn);
		}
	}


	/// <summary>
	/// Saves the player move in a message. The message's action will indicate
	/// what kind of rpc will be called for the opponent.
	/// </summary>
	/// <param name="message">Message.</param>
	void SavePlayerMove (Message message) 
	{
		string timeStamp = DateTime.Now.ToString ();
		string actionType = message.action.ToString();
		float damage = message.damage;
		GameObject targetField = message.targetField;

		switch (message.action) 
		{
			case ActionType.ATTACK:
			GameObject attacker = message.involvedCharacters[0];
			GameObject victim = message.involvedCharacters[1];
			photonView.RPC ("LoadAttack", PhotonTargets.Others, timeStamp, attacker, victim, damage, targetField);
			break;
			case ActionType.MOVEMENT:
			GameObject character = message.involvedCharacters[0];
			photonView.RPC ("LoadMove", PhotonTargets.Others, timeStamp, character, targetField);
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
		//Application.LoadLevel ("GameScene");
		//DontDestroyOnLoad (this);
		levelController.LoadLevel (mapPrefabs, mapFields);
		//The player who opened the game will start
		playerToMakeTurn = 1;

		Debug.Log ("instantiated");
		//map = true;

	}

	/// <summary>
	/// Hands the turn over to the opponent.
	/// </summary>
	/// <param name="opponent">Id of opponent</param>
	[PunRPC]
	void HandOverTurn (int opponent) 
	{
		playerToMakeTurn = opponent;
	}

	/// <summary>
	/// Load parameters for updating the game. In this case the opponent launched an attack on an unit.
	/// </summary>
	/// <param name="timeStamp">Indicates when the move was sent.</param>
	/// <param name="attacker">Indicates which unit launched the attack.</param>
	/// <param name="victim">Indicates the target of the attack.</param>
	/// <param name="damage">Indicates the taken damage for the victim.</param>
	/// <param name="targetField">Indicates the target field of the attacker, if it moved before it attacked.</param>
	[PunRPC]
	void LoadAttack (String timeStamp, GameObject attacker, GameObject victim, float damage, GameObject targetField) 
	{
		DateTime currentTimeStamp = DateTime.Now;
		DateTime receivedTimeStamp = Convert.ToDateTime (timeStamp);
		TimeSpan diff = currentTimeStamp.Subtract (receivedTimeStamp);
		if (diff.Hours > 24) {
			Debug.Log ("Gegner hat zu lange gebraucht. Du hast gewonnen!");
		} else {
			Unit attackerUnit = playerController.pListUnits.Find(x => x.pStringName == attacker.name);
			if (targetField != null) {
				attackerUnit.pGOTarget = targetField;
				attackerUnit.move(); 
			}
			attackerUnit.Attack(playerController.pListUnits.Find(x => x.pStringName == victim.name));

			//how to handle die?
		}
	}

	/// <summary>
	///  Load parameters for updating the game. In this case a character was mereley moved.
	/// </summary>
	/// <param name="timeStamp">Indicates when the move was sent.</param>
	/// <param name="character">Indicates the unit which was moved.</param>
	/// <param name="targetField">Indicates the targetField to which the character will be moved.</param>
	[PunRPC]
	void LoadMove (String timeStamp, GameObject character, GameObject targetField) 
	{
		DateTime currentTimeStamp = DateTime.Now;
		DateTime receivedTimeStamp = Convert.ToDateTime (timeStamp);
		TimeSpan diff = currentTimeStamp.Subtract (receivedTimeStamp);
		if (diff.Hours > 24) {
			Debug.Log ("Gegner hat zu lange gebraucht. Du hast gewonnen!");
		} else {
			Unit movedUnit = playerController.pListUnits.Find (x => x.pStringName == character.name);
			movedUnit.pGOTarget = targetField;
			movedUnit.move ();
		}
	}

	private void TestData () {
		//shouldn't be done like that; load .txt file
		string pDesc = "Sir, my informer Dino Scarbonelli told me about a huge load of illegal merchandise at the harbour area. There are at least three safes full of jewelry, drugs and hard cash. We should get there quick, confiscate the stuff and bust the bad guys. I think we need some specialists for this task. At least an officer who knows how to deal with a picklock and some protection. Crack the safes. Get the illegal merchandise to your escape point. Protect the carriers. If they die, their treasure is lost.";
		string mDesc = "Hey Boss, our man at the docks Dino Scarbonelli told me about an opportunity to make some money. There are at least three safes full of jewelry, drugs and hard cash  located at the harbor area. Which of our guys are the right ones for this job? You might need a safecracker and some gun power. Be aware of the cops, it might be a trap. Crack the safes. Get the loot to your escape point. Protect the carriers. If they die, their treasure is lost. ";
		Mission harbor = new Mission ("The Harbor Job", mDesc, pDesc);
		missions.Add (harbor.missionId, harbor);

		TextAsset userFile = (TextAsset)Resources.Load("user", typeof(TextAsset));
		_user = new User (userFile.text);
	}
	
}