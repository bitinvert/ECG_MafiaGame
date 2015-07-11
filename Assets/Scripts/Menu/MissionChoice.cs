using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MissionChoice : MonoBehaviour {

	Client client;

	public Text messageField;
	public Text missionDescriptionField;
	public string loadingText = "loading";

	// Use this for initialization
	void Start () {
		GameObject go = GameObject.FindWithTag ("Client");
		client = (Client)go.GetComponent (typeof(Client));

		if (client != null) {
			missionDescriptionField.text = client.missions["The Harbor Job"].mafiaDescription;
			Debug.Log (client.missions["The Harbor Job"].mafiaDescription);
		} else {
			Debug.Log ("no client");
		}

	}
	
	public void OpenMission (string missionName) {
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
					messageField.text = loadingText;
					break;
				case 1:
					messageField.text = loadingText+ ".";
					break;
				case 2:
					messageField.text = loadingText+ "..";
					break;
				case 3:
					messageField.text = loadingText + "...";
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
