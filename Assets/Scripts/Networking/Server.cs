using UnityEngine;
using System;
using Mono.Data.Sqlite;
using System.Data;
using System.Collections;
using System.Collections.Generic;

public class Server : MonoBehaviour {

	int listenPort = 25000;
	int maxClients = 2;
	IDbConnection dbconn;
	IDbCommand dbcmd;

	NetworkView networkView;
	
	/// <summary>
	/// Start this instance.
	/// </summary>
	void Start ()
	{
		networkView = GetComponent <NetworkView> ();
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
	/// Get missions from database and send them to clients.
	/// </summary>
	[RPC]
	void GetMissions()
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
		networkView.RPC ("ShowMissions", RPCMode.Others, missions);

		reader.Close ();
		dbcmd.Dispose();
		dbcmd = null;
	}

	//RPC client functions
	[RPC]
	void RequestMissions()
	{
		//empty
	}

	[RPC]
	void ShowMissions(string missions)
	{
		//empty
	}
}
