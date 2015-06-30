using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MissionChoice : MonoBehaviour {

	Client client;

	public InputField messageField;

	// Use this for initialization
	void Start () {

	}
	
	public void OpenMission (string missionName) {
		GameObject go = GameObject.FindWithTag ("Client");
		Client client = (Client)go.GetComponent (typeof(Client));

		if (client != null) {
			client.CreateMission (missionName);
			this.CheckMissionStatus();

		} else {
			Debug.Log ("no client");
		}

	}

	public void JoinMission (string missionName) {
		client.JoinMission (missionName);
	}

	private void CheckMissionStatus () 
	{
		StartCoroutine(checkStatus());
	}

	IEnumerator checkStatus() 
	{
		int i = 0;

		while (true) {

			switch(i) {
				case 0: 
					messageField.text = "searching for opponent";
					break;
				case 1:
					messageField.text = "searching for opponent.";
					break;
				case 2:
					messageField.text = "searching for opponent..";
					break;
				case 3:
					messageField.text = "searching for opponent...";
					break;
				case 4:
					i = 0;
					break;
			}

			i++;

			yield return new WaitForSeconds(0.5f);
		}
	}
}
