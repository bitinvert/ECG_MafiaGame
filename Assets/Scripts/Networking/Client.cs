using UnityEngine;
using System.Collections;

public class Client : MonoBehaviour {

	string remoteIP = "127.0.0.1";
	int remotePort = 25000;
	NetworkView networkView;

	/// <summary>
	/// Start this instance.
	/// </summary>
	void Start ()
	{
		networkView = GetComponent <NetworkView> ();
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

			if (GUILayout.Button ("Say hello"))
			{
				SendMessage();
			}
		}
	}

	[RPC]
	void SendMessage()
	{
		networkView.RPC ("SaveMessage", RPCMode.Server, "Hello Server");
	}

	//Server RPC functions
	[RPC]
	void SaveMessage (string message)
	{
		//empty
	}
}
