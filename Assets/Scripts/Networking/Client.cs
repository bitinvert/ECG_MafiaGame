using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Client : MonoBehaviour {
	
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

	/// <summary>
	/// Called if scene was switched.
	/// Level 2 = Gamescene; get enemy's name to set in gui
	/// </summary>
	/// <param name="level">Level.</param>
	void OnLevelWasLoaded (int level) {

		if (level == 2) {
			if (PhotonNetwork.player.ID == 1) {
				_opponentName = PhotonNetwork.player.Get (2).name;
			} else {
				_opponentName = PhotonNetwork.player.Get (1).name;
			}
		}
	}

	/// <summary>
	/// Create a new room(game) for a selected mission.
	/// </summary>
	/// <param name="missionName">Will be used as the game (room) name.</param>
	public void CreateMission (string missionName) {
		RoomOptions roomOptions = new RoomOptions() { isVisible = true, isOpen = true, maxPlayers = 2 };
		PhotonNetwork.JoinOrCreateRoom (missionName, roomOptions, TypedLobby.Default);
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

	/// <summary>
	/// If someone joins a game, switch scene to game scene.
	/// </summary>
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
			PhotonPlayer nextPlayer = PhotonNetwork.player.GetNextFor(playerToMakeTurn);
			playerToMakeTurn = nextPlayer.ID;
			photonView.RPC ("HandOverTurn", nextPlayer, playerToMakeTurn);
		} else {
			Debug.Log (playerToMakeTurn);
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
		int damage = message.damage;
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
			photonView.RPC ("LoadMove", PhotonTargets.Others, timeStamp, character , targetField);
			break;
			//more ActionTypes can follow

			case ActionType.SPECIAL:
            string specialUser = message.involvedCharacters[0];
            string specialTarget = message.involvedCharacters[1];
            photonView.RPC("LoadSpecial", PhotonTargets.Others, timeStamp, specialUser, specialTarget);
			break;
            case ActionType.BREAK_SAFE:
            string character1 = message.involvedCharacters[0];
            string safe = message.involvedCharacters[1];
            photonView.RPC("LoadSafeBreak", PhotonTargets.Others, timeStamp, character1, safe);
			break;

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
	void LoadAttack (String timeStamp, string attacker, string victim, int damage, Vector3 targetField) 
	{
		DateTime currentTimeStamp = DateTime.Now;
		DateTime receivedTimeStamp = Convert.ToDateTime (timeStamp);
		TimeSpan diff = currentTimeStamp.Subtract (receivedTimeStamp);

		PlayerController playerController = FindObjectOfType(typeof(PlayerController)) as PlayerController;

		if (diff.Hours > 24) {
			Debug.Log ("Gegner hat zu lange gebraucht. Du hast gewonnen!");
		} else {
			Unit attackerUnit = playerController.pListUnits.Find(x => x.pStringName.Equals(attacker));
			/*
			if (targetField != null) {
				attackerUnit.pAStarPathfinding.FindPath(attackerUnit.transform.position, targetField, attackerUnit.mVec3Offset);
				attackerUnit.move(); 
				attackerUnit.ResetMoveVals ();
			}
			*/
			//attackerUnit.Attack(playerController.pListUnits.Find(x => x.pStringName.Equals (victim)));
			Unit victimUnit = playerController.pListUnits.Find(x => x.pStringName.Equals (victim));
			victimUnit.pIntHealth -= damage;
			if (victimUnit.pIntHealth <= 0)
			{
				victimUnit.Die();
			}
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
			movedUnit.targetField = targetField;
			movedUnit.moving = true;
		}
	}
	
	/// <summary>
	///  Load parameters for updating the game. In this case a character used its special ability on another.
	/// </summary>
	/// <param name="timeStamp">Indicates when the move was sent.</param>
	/// <param name="character">Indicates the unit which used its special ability.</param>
	/// <param name="target">Indicates the unit on which the special ability was used.</param>
	[PunRPC]
    void LoadSpecial(String timeStamp, string character, string target)
    {
        DateTime currentTimeStamp = DateTime.Now;
		DateTime receivedTimeStamp = Convert.ToDateTime (timeStamp);
		TimeSpan diff = currentTimeStamp.Subtract (receivedTimeStamp);

		PlayerController playerController = FindObjectOfType(typeof(PlayerController)) as PlayerController;

        if (diff.Hours > 24)
        {
            Debug.Log("Gegner hat zu lange gebraucht. Du hast gewonnen!");
        }
        else
        {
            Unit specialUser = playerController.pListUnits.Find(x => x.pStringName.Equals(character));
            Unit specialTarget = playerController.pListUnits.Find(x => x.pStringName.Equals(target));

            specialUser.UseSpecial(specialTarget);

        }
    }

	[PunRPC]
	void LoadSafeBreak(String timeStamp, string character, string safeId)
	{
		DateTime currentTimeStamp = DateTime.Now;
		DateTime receivedTimeStamp = Convert.ToDateTime (timeStamp);
		TimeSpan diff = currentTimeStamp.Subtract (receivedTimeStamp);
		
		PlayerController playerController = FindObjectOfType(typeof(PlayerController)) as PlayerController;
		
		if (diff.Hours > 24)
		{
			Debug.Log("Gegner hat zu lange gebraucht. Du hast gewonnen!");
		}
		else
		{
			Debug.Log (safeId);
			Unit safeBreaker = playerController.pListUnits.Find(x => x.pStringName.Equals(character));
			Safe safeToBreak = null;
			GameObject [] gos = GameObject.FindGameObjectsWithTag("Objective");

			Debug.Log(gos.Length);

			foreach (GameObject go in gos) 
			{

				Safe safe = (Safe)go.GetComponent(typeof(Safe));
				if (safe != null) 
				{
					if (safe.identifier.Equals(safeId))
					{
						safeToBreak = safe;
						break;
					}
				}

			}

			if (safeToBreak != null) {
				safeBreaker.pOIObjective = safeToBreak;
				safeToBreak.InteractWithObjective(safeBreaker);
				safeBreaker.bag.SetActive(true);
				safeBreaker.pOIObjective.gameObject.SetActive (false);
			}


			//Unit safe = playerController.pListTreasures.Find(x => x.pStringName.Equals(safe));
			//safeBreaker.BreakSafe(safe);
		}

	}


	/// <summary>
	/// Loading Test data for default game.
	/// </summary>
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
