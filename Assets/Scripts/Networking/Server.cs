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
			//rpcmode nur an aufrufer zurueck
			networkView.RPC("Login", sender, true);
			//networkView.RPC("Login", RPCMode.Others, true);
		} else {
			//rpcmode nur an aufrufer zurueck
			networkView.RPC("Login", sender, false);
			//networkView.RPC("Login", RPCMode.Others, false);
		}

		reader.Close ();
		dbcmd.Dispose();
		dbcmd = null;
	}


	[RPC]
	void BuildMap()
	{
		//generate map

	}

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

	//RPC client functions
	[RPC]
	void CheckLogin ()
	{

	}

	[RPC]
	void Login (bool authorized)
	{
	}

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

	[RPC]
	void RequestMap ()
	{
	}
}
