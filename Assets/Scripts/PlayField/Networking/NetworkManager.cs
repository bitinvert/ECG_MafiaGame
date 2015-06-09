using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour {
	
	private const string typeName = "MultiplayerNetworkGameTest";
	private const string gameName = "Game";
	private HostData[] hostList;
	public GameObject player;
	public Transform spawnPoint;


	/// <summary>
	/// Starts the server.
	/// </summary>
	private void StartServer ()
	{
		Network.InitializeServer (2, 25000, !Network.HavePublicAddress());
		MasterServer.RegisterHost (typeName, gameName);

	}

	/// <summary>
	/// Raises the server initialized event.
	/// </summary>
	void OnServerInitialized ()
	{
		SpawnPlayer ();
	}

	/// <summary>
	/// Join the running server.
	/// </summary>
	/// <param name="hostData">Host data.</param>
	private void JoinServer(HostData hostData)
	{
		Network.Connect (hostData);
	}

	/// <summary>
	/// Is raised, if someone connects to server.
	/// </summary>
	void OnConnectedToServer ()
	{
		SpawnPlayer ();
	}
	
	/// <summary>
	/// Spawns a player into the scene.
	/// </summary>
	private void SpawnPlayer()
	{
		Network.Instantiate (player, spawnPoint.position, Quaternion.identity, 0);
	}
	
	/// <summary>
	/// Refreshs the host list.
	/// </summary>
	private void RefreshHostList ()
	{
		MasterServer.RequestHostList (typeName);
	}
	
	/// <summary>
	/// Raise the master server event event.
	/// </summary>
	/// <param name="msEvent">Ms event.</param>
	void OnMasterServerEvent (MasterServerEvent msEvent)
	{
		if (msEvent == MasterServerEvent.HostListReceived) 
		{
			hostList = MasterServer.PollHostList();
		}
	}
	
	/// <summary>
	/// Manages GUI Components and View.
	/// </summary>
	void OnGUI ()
	{
		//if you're neither Server or Client...
		if (!Network.isClient && !Network.isServer) 
		{
			//... enable Button to start Server. 

			if (GUI.Button (new Rect(100, 100, 250, 100), "Start Server"))
			{
				StartServer ();
			}

			if (GUI.Button (new Rect(100, 250, 250, 100), "Refresh Host List"))
			{
				RefreshHostList ();
			}
			if (hostList != null)
			{
				for (int i = 0; i < hostList.Length; i++)
				{
					if (GUI.Button(new Rect(400, 100 + (110 * i), 300, 100), hostList[i].gameName))
					{
						JoinServer (hostList[i]);
					}
				}
			}
		}
	}
}
