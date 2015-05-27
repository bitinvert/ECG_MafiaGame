using UnityEngine;
using System;
using Mono.Data.Sqlite;
using System.Data;
using System.Collections;

public class Server : MonoBehaviour {

	int listenPort = 25000;
	int maxClients = 2;
	IDbConnection dbconn;
	IDbCommand dbcmd;


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
	void SaveMessage(string message)
	{
		dbcmd = dbconn.CreateCommand ();
		//NEVER DO THIS! SQL-INJECTION!
		//TODO: setting parameters to command
		dbcmd.CommandText = "INSERT INTO Messages VALUES ('" + message + "');";
		dbcmd.ExecuteNonQuery ();
		dbcmd.Dispose();
		dbcmd = null;
	}

	//RPC client functions
	[RPC]
	void SendMessage()
	{
		//empty
	}
}
