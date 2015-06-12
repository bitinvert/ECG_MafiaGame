using UnityEngine;
using System;
using Mono.Data.Sqlite;
using System.Data;
using System.Collections;
using System.Collections.Generic;

public class Server : MonoBehaviour {
	
	int listenPort = 25000;
	int maxClients = 2;
	int readyPlayer = 0;
	int currentTurn = 1;
	IDbConnection dbconn;
	IDbCommand dbcmd;
	Dictionary <string, List<NetworkPlayer>> missionMap;
	
	NetworkView networkView;
	
	/// <summary>
	/// Start this instance.
	/// </summary>
	void Start ()
	{
		networkView = GetComponent <NetworkView> ();
		missionMap = new Dictionary <string, List<NetworkPlayer>> ();
	}
	
	/// <summary>
	/// Starts the server. First initialize Server on port, then build connection to a database.
	/// </summary>
	private void startServer() {
		Network.InitializeServer(maxClients, listenPort, false);
		string conn = "URI=file:" + Application.dataPath + "/ServerDatabase.s3db"; 
		dbconn = new SqliteConnection(conn);
		dbconn.Open(); 
	}
	
	/// <summary>
	/// Shut down server and close database connection.
	/// </summary>
	private void stopServer() {
		Network.Disconnect();
		
		dbconn.Close();
		dbconn = null;
	}
	
	/// <summary>
	/// Manage view of GUI.
	/// </summary>
	void OnGUI ()
	{
		if (Network.peerType == NetworkPeerType.Disconnected) {
			GUILayout.Label("Network server is not running.");
			if (GUILayout.Button ("Start Server"))
			{				
				startServer();	
			}
		}
		else {
			if (Network.peerType == NetworkPeerType.Connecting)
				GUILayout.Label("Network server is starting up...");
			else { 
				GUILayout.Label("Network server is running.");        	
				showServerInformation(); 	
				showClientInformation();
			}
			if (GUILayout.Button ("Stop Server"))
			{				
				stopServer();	
			}
		}
	}
	
	/// <summary>
	/// Shows the client information.
	/// </summary>
	private void showClientInformation() {
		GUILayout.Label("Clients: " + Network.connections.Length + "/" + maxClients);
		foreach(NetworkPlayer p in Network.connections) {
			GUILayout.Label(" Player from ip/port: " + p.ipAddress + "/" + p.port);	
		}
	}
	
	/// <summary>
	/// Shows the server information.
	/// </summary>
	private void showServerInformation() {
		GUILayout.Label("IP: " + Network.player.ipAddress + " Port: " + Network.player.port);  
	}

	/// <summary>
	/// Executes client's registration request.
	/// </summary>
	/// <param name="username">Username of client</param>
	/// <param name="password">Password of client</param>
	/// <param name="sender">client's network player, so server knows which it has to answer</param>
	[RPC]
	void RegisterUser(string username, string password, NetworkPlayer sender)
	{
		dbcmd = dbconn.CreateCommand ();
		IDbDataParameter usernameParam = dbcmd.CreateParameter ();
		IDbDataParameter passwordParam = dbcmd.CreateParameter ();
		dbcmd.CommandText = "INSERT INTO Player VALUES(@username, @password);";
		
		usernameParam.DbType = DbType.String;
		usernameParam.Value = username;
		usernameParam.ParameterName = "username";
		
		passwordParam.DbType = DbType.String;
		passwordParam.Value = password;
		passwordParam.ParameterName = "password";
		
		dbcmd.Parameters.Add (usernameParam);
		dbcmd.Parameters.Add (passwordParam);
		
		if (dbcmd.ExecuteNonQuery () == 1) 
		{
			networkView.RPC ("Login", sender, true);
		} else {
			networkView.RPC ("Login", sender, false);
		}
		
		dbcmd.Dispose();
		dbcmd = null;
		
	}
	
	/// <summary>
	/// Authorizes client's login request.
	/// </summary>
	/// <param name="username">Username of client</param>
	/// <param name="password">Password of client</param>
	/// <param name="sender">client's network player, so server knows which it has to answer</param>
	[RPC]
	void AuthorizeLogin(string username, string password, NetworkPlayer sender)
	{
		dbcmd = dbconn.CreateCommand ();
		IDbDataParameter usernameParam = dbcmd.CreateParameter ();
		IDbDataParameter passwordParam = dbcmd.CreateParameter ();
		dbcmd.CommandText = "SELECT * FROM Player WHERE username==@username AND password==@password;";
		
		usernameParam.DbType = DbType.String;
		usernameParam.Value = username;
		usernameParam.ParameterName = "username";
		
		passwordParam.DbType = DbType.String;
		passwordParam.Value = password;
		passwordParam.ParameterName = "password";
		
		dbcmd.Parameters.Add (usernameParam);
		dbcmd.Parameters.Add (passwordParam);
		
		IDataReader reader = dbcmd.ExecuteReader ();
		
		if (reader.Read ()) 
		{
			networkView.RPC("Login", sender, true);
		} else {
			networkView.RPC("Login", sender, false);
		}
		
		reader.Close ();
		dbcmd.Dispose();
		dbcmd = null;
	}

	/// <summary>
	/// Register Client to a mission
	/// </summary>
	/// <param name="sender">Client who wants to join a mission.</param>
	/// <param name="missionName">Name of selected mission.</param>
	[RPC]
	void RegisterMission(NetworkPlayer sender, string missionName)
	{
		if (missionMap.ContainsKey(missionName)) 
		{
			if (missionMap [missionName].Count < 2)
			{
				missionMap [missionName].Add (sender);
			}
			else if (missionMap [missionName].Count == 2)
			{

				string mapFields = System.IO.File.ReadAllText(@"Assets/safedLevel_singleFields.xml");
				string mapPrefab = System.IO.File.ReadAllText(@"Assets/safedLevel_prefabHolder.xml");

				foreach (NetworkPlayer networkPlayer in missionMap[missionName])
				{
					networkView.RPC ("InstantiateMap", networkPlayer, mapPrefab, mapFields);
				}
			}
		} else {
			List <NetworkPlayer> members = new List <NetworkPlayer>();
			members.Add (sender);
			missionMap.Add (missionName, members);
		}
	}

	/// <summary>
	/// Draws the beginner of a game. Only if both players are ready to start.
	/// </summary>
	/// <param name="sender">Sender.</param>
	/// <param name="missionName">Mission name.</param>
	[RPC]
	void DrawBeginner (NetworkPlayer sender, string missionName)
	{
		if (missionMap [missionName].Contains (sender)) 
		{
			readyPlayer += 1;
		}

		if (readyPlayer == 2) 
		{

			if (UnityEngine.Random.Range(1, 3) == 1){
				networkView.RPC("SetTurnValue", missionMap[missionName][0], 1);
				networkView.RPC("SetTurnValue", missionMap[missionName][1], 2);
			} else{
				networkView.RPC("SetTurnValue", missionMap[missionName][0], 2);
				networkView.RPC("SetTurnValue", missionMap[missionName][1], 1);
			}
		}
	}

	/// <summary>
	/// Checks if the move was done in a valid turn.
	/// </summary>
	/// <param name="turnValue">Turn value.</param>
	[RPC]
	void CheckMove (int turnValue)
	{
		if (turnValue != currentTurn) {
			Debug.Log (turnValue + " anderer Spieler ist dran");
		} else {
			if (turnValue == 1)
			{
				currentTurn = 2;
			} else{
				currentTurn = 1;
			}
		}
	}

	/*
	Not used yet.

	/// <summary>
	/// Get missions from database and send them to clients.
	/// </summary>
	[RPC]
	void GetMissions(NetworkPlayer sender)
	{
		dbcmd = dbconn.CreateCommand ();
		dbcmd.CommandText = "SELECT * FROM Missions;";
		IDataReader reader = dbcmd.ExecuteReader ();
		string missions = "";

		while (reader.Read()) 
		{
			missions += reader.GetString (0) + " ";
		}
		missions = missions.Trim ();
		//rpcmode nur an aufrufer zurueck
		networkView.RPC ("ShowMissions", sender, missions);

		reader.Close ();
		dbcmd.Dispose();
		dbcmd = null;
	}
	*/
	
	//RPC client functions
	[RPC]
	public void RequestRegistration(string username, string password)
	{
	}
	
	[RPC]
	void CheckLogin ()
	{
	}
	
	[RPC]
	void Login (bool authorized)
	{
	}

	[RPC]
	void JoinMission ()
	{
	}

	[RPC]
	void InstantiateMap (string fields, string prefabs)
	{
	}

	[RPC]
	void SendMove ()
	{
	}

	[RPC]
	void SetTurnValue (int value)
	{
	}

	/*
	[RPC]
	void ShowMissions(string missions)
	{
		//empty
	}
	*/
}
