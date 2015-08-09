using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MissionChoice : MonoBehaviour {

	Client client;

	// A reference to the Mission Button's text
	public Text messageField;

	// A reference to the Window containing the Mission's description text
	public Text missionDescriptionField;

	// The message that is supposed to be written into the messageField if neccessary
	public string loadingText = "loading";

	/**
	 * Get a reference to the active Client GameObject and set the Mission's description text
	 * to the Description retrieved from the Client object.
	 */
	void Start () {
		GameObject go = GameObject.FindWithTag ("Client");
		client = (Client)go.GetComponent (typeof(Client));

		if (client != null) {
			if (client.user.fraction == Fraction.MAFIA) {
				missionDescriptionField.text = client.missions["The Harbor Job"].mafiaDescription;
			} else if (client.user.fraction == Fraction.POLICE) {
				missionDescriptionField.text = client.missions["The Harbor Job"].policeDescription;
			}
		} else {
			Debug.Log ("no client");
		}

	}

	/**
	 * If called, retrieve the corresponding mission and try to start it
	 * and call CheckMissionStatus()
	 */
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

	/**
	 * Start a Coroutine to show the User that the Programm is trying to
	 * etablish a connection with another player in order to start the mission.
	 */
	private void CheckMissionStatus () 
	{
		StartCoroutine(checkStatus());
	}

	/**
	 * Switch between "loading." "loading.." and "loading..." every 0.5 Seconds.
	 */
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
