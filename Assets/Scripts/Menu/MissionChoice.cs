using UnityEngine;
using System.Collections;

public class MissionChoice : MonoBehaviour {

	Client client;

	// Use this for initialization
	void Start () {

	}
	
	public void OpenMission (string missionName) {
		GameObject go = GameObject.FindWithTag ("Client");
		Client client = (Client)go.GetComponent (typeof(Client));

		if (client != null) {
			client.CreateMission (missionName);
		} else {
			Debug.Log ("no client");
		}

	}

	public void JoinMission (string missionName) {
		client.JoinMission (missionName);
	}

}
