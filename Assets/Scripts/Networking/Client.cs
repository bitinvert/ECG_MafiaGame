using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Client : MonoBehaviour {

	string remoteIP = "127.0.0.1";
	int remotePort = 25000;
	NetworkView networkView;
	List<string> missions;
	bool missionOn = false;

	/// <summary>
	/// Start this instance.
	/// </summary>
	void Start ()
	{
		networkView = GetComponent <NetworkView> ();
		missions = new List<string> ();
	}

	/// <summary>
	/// Connects to server.
	/// </summary>
	void connectToServer() {
		Network.Connect(remoteIP, remotePort);	
	}

	/// <summary>
	/// Disconnects from server.
	/// </summary>
	void disconnectFromServer() {
		Network.Disconnect();
	}

	/// <summary>
	/// Manage view of GUI.
	/// </summary>
	void OnGUI ()
	{	
		if (Network.peerType == NetworkPeerType.Disconnected)
		{	
			GUILayout.Label("Not connected to server.");
			if (GUILayout.Button ("Connect to server"))
			{
				connectToServer();
			}
		}
		else
		{
			if(Network.peerType == NetworkPeerType.Connecting)
			{
				GUILayout.Label("Connecting to server...");
			}
			else {
				GUILayout.Label("Connected to server.");
				GUILayout.Label("IP/port: " + Network.player.ipAddress + "/" + Network.player.port);
			}
			if (GUILayout.Button ("Disconnect"))
			{
				disconnectFromServer();
			}

			if (GUILayout.Button ("Missionen"))
			{
				RequestMissions();

			}
			if (missionOn)
			{
				foreach (string s in missions)
				{
					GUILayout.Button(s);
				}
			}
		}
	}

	/// <summary>
	/// Request missions from server.
	/// </summary>
	[RPC]
	void RequestMissions()
	{
		networkView.RPC ("GetMissions", RPCMode.Server);
	}

	/// <summary>
	/// Update missions list with received string.
	/// </summary>
	/// <param name="missionsString">Missions string.</param>
	[RPC]
	void ShowMissions(string missionsString)
	{
		string [] missionArray = missionsString.Split (new char[] {' '});
		foreach (string s in missionArray) 
		{
			missions.Add (s);
		}
		missionOn = true;

	}

	//Server RPC functions
	[RPC]
	void GetMissions ()
	{
		//empty
	}
}
