using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Client : MonoBehaviour {
	
	private const string roomName = "RoomName";
	private RoomInfo[] roomsList;
	private int maxClients = 2;
	
	private PhotonView photonView;

	public Dictionary <string, Mission> missions { get { return _missions; } }
	private Dictionary <string, Mission> _missions;

	public User user { get { return _user; } }
	private User _user;

	public int turnNumber = 1;
	public int playerToMakeTurn;

	public string opponentName { get { return _opponentName; } }
	private string _opponentName;

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
		PhotonNetwork.playerName = user.username;
	}

	void OnLevelWasLoaded (int level) {

		if (level == 2) {
			if (PhotonNetwork.player.ID == 1) {
				_opponentName = PhotonNetwork.player.Get (2).name;
			} else {
				_opponentName = PhotonNetwork.player.Get (1).name;
			}
			/*
			TextAsset mapPrefab = (TextAsset)Resources.Load("TestWith3DModels_prefabHolder", typeof(TextAsset));
			TextAsset mapFields = (TextAsset)Resources.Load("TestWith3DModels_singleFields", typeof(TextAsset));

			//TextAsset map = (TextAsset)Resources.Load("TestWith3DModels", typeof(TextAsset));
			photonView.RPC ("InstantiateMap", PhotonTargets.Others, mapPrefab.text, mapFields.text);


			//photonView.RPC ("InstantiateMap", PhotonTargets.Others, "Assets/Resources/TestWith3DModels");
			*/
		}
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

	/// <summary>
	/// Check if there are currently two players in the game.
	/// </summary>
	/// <value><c>true</c> if there are two players return true; otherwise, <c>false</c>.</value>
	public bool CheckTwoPlayers {
		get { 
			return PhotonNetwork.playerList.Length == 2;
		}
	}

	/// <summary>
	/// Check if it's currently the players turn.
	/// </summary>
	/// <value><c>true</c> if it's the player's turn return true; otherwise, <c>false</c>.</value>
	public bool IsMyTurn {
		get { 
			return playerToMakeTurn == PhotonNetwork.player.ID; 
		}
	}

	/// <summary>
	/// Check if the user is part of the Mafia
	/// </summary>
	/// <value><c>true</c> if the user is Mafia; otherwise, <c>false</c>.</value>
	public bool IsMafia {
		get {
			return user.fraction == Fraction.MAFIA;
		}
	}

	/// <summary>
	/// Check if the user is part of the Police
	/// </summary>
	/// <value><c>true</c> if the user is Police; otherwise, <c>false</c>.</value>
	public bool IsPolice {
		get {
			return user.fraction == Fraction.POLICE;
		}
	}

	
	void OnReceivedRoomListUpdate()
	{
		roomsList = PhotonNetwork.GetRoomList();
		Debug.Log ("OnReceivedRoomListUpdate()");
	}
	
	
	void OnJoinedRoom()
	{
		if (CheckTwoPlayers) {

			photonView.RPC ("SwitchToGame", PhotonTargets.All);
		}
	}

	/// <summary>
	/// Indicate player change by setting the playertomaketurn to your oppent.
	/// </summary>
	public void playerChange () {
		if (IsMyTurn) {
			Debug.Log ("war meine Runde");
			PhotonPlayer nextPlayer = PhotonNetwork.player.GetNextFor(playerToMakeTurn);
			playerToMakeTurn = nextPlayer.ID;
			photonView.RPC ("HandOverTurn", nextPlayer, playerToMakeTurn);
		} else {
			Debug.Log ("war nicht meine Runde");
			Debug.Log (playerToMakeTurn);
			Debug.Log (PhotonNetwork.player.ID);
		}
	}


	/// <summary>
	/// Saves the player move in a message. The message's action will indicate
	/// what kind of rpc will be called for the opponent.
	/// </summary>
	/// <param name="message">Message.</param>
	public void SavePlayerMove (Message message) 
	{
		string timeStamp = DateTime.Now.ToString ();
		string actionType = message.action.ToString();
		float damage = message.damage;
		Vector3 targetField = message.targetField;

		switch (message.action) 
		{
			case ActionType.ATTACK:
			string attacker = message.involvedCharacters[0];
			string victim = message.involvedCharacters[1];
			photonView.RPC ("LoadAttack", PhotonTargets.Others, timeStamp, attacker, victim, damage, targetField);
			break;
			case ActionType.MOVEMENT:
			string character = message.involvedCharacters[0];
			Debug.Log ("move send");
			photonView.RPC ("LoadMove", PhotonTargets.Others, timeStamp, character , targetField);
			break;
			//more ActionTypes can follow

		}
	}

	/// <summary>
	/// Switch to Game Scene.
	/// </summary>
	[PunRPC]
	void SwitchToGame () 
	{
		playerToMakeTurn = 1;
		Application.LoadLevel ("GameScene");
		DontDestroyOnLoad (this);
	}
	
	/// <summary>
	/// Client instantiates received map from server.
	/// </summary>
	/// <param name="mapFields">xml view of map fields</param>
	/// <param name="mapPrefabs">xml view of map prefabs</param>
	[PunRPC]
	void InstantiateMap (string mapPrefab, string mapFields)
	{
		GameObject go = GameObject.FindWithTag ("LevelController");
		LevelController levelController = (LevelController)go.GetComponent (typeof(LevelController));
		Debug.Log(mapPrefab);
		Debug.Log(mapFields);
		levelController.LoadLevel (mapPrefab, mapFields);
		//The player who opened the game will start
		playerToMakeTurn = 1;
		Debug.Log ("instantiated");


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
	void LoadAttack (String timeStamp, string attacker, string victim, float damage, Vector3 targetField) 
	{
		DateTime currentTimeStamp = DateTime.Now;
		DateTime receivedTimeStamp = Convert.ToDateTime (timeStamp);
		TimeSpan diff = currentTimeStamp.Subtract (receivedTimeStamp);

		PlayerController playerController = FindObjectOfType(typeof(PlayerController)) as PlayerController;

		if (diff.Hours > 24) {
			Debug.Log ("Gegner hat zu lange gebraucht. Du hast gewonnen!");
		} else {
			Unit attackerUnit = playerController.pListUnits.Find(x => x.pStringName.Equals(attacker));

			if (targetField != null) {
				attackerUnit.pAStarPathfinding.FindPath(attackerUnit.transform.position, targetField, attackerUnit.mVec3Offset);
				attackerUnit.move(); 
				attackerUnit.ResetMoveVals ();
			}

			attackerUnit.Attack(playerController.pListUnits.Find(x => x.pStringName.Equals (victim)));

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
	void LoadMove (String timeStamp, string character, Vector3 targetField) 
	{
		DateTime currentTimeStamp = DateTime.Now;
		DateTime receivedTimeStamp = Convert.ToDateTime (timeStamp);
		TimeSpan diff = currentTimeStamp.Subtract (receivedTimeStamp);

		PlayerController playerController = FindObjectOfType(typeof(PlayerController)) as PlayerController;

		if (diff.Hours > 24) {
			Debug.Log ("Gegner hat zu lange gebraucht. Du hast gewonnen!");
		} else {
			Unit movedUnit = playerController.pListUnits.Find (x => x.pStringName.Equals(character));
			movedUnit.pAStarPathfinding.FindPath(movedUnit.transform.position, targetField, movedUnit.mVec3Offset);

			while (movedUnit.transform.position - movedUnit.mVec3Offset != targetField) {
				movedUnit.move ();
			}
			movedUnit.ResetValues ();
		}
	}

	private void TestData () {
		//shouldn't be done like that; load .txt file
		TextAsset pDesc = (TextAsset)Resources.Load("TheHarborJob_desc_p", typeof(TextAsset));
		TextAsset mDesc = (TextAsset)Resources.Load("TheHarborJob_desc_m", typeof(TextAsset));
		Mission harbor = new Mission ("The Harbor Job", mDesc.text, pDesc.text);
		missions.Add (harbor.missionId, harbor);

		TextAsset userFile = (TextAsset)Resources.Load("user", typeof(TextAsset));
		string [] userData = userFile.text.Split (new string [] {"="}, StringSplitOptions.RemoveEmptyEntries);
		_user = new User (userData[0], userData[1]);
	}
	
}
