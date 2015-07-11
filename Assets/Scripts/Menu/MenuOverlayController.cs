using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuOverlayController : MonoBehaviour {

	public GameObject attackButton;
	public GameObject healButton;

	public GameObject endTurnButton;
	public GameObject endTurnButton_Waiting;

	public PlayerController pc;

	private Client mClientPlayer;

	void Start() {
		mClientPlayer = Object.FindObjectOfType(typeof(Client)) as Client;
	}

	// Update is called once per frame
	void Update () {
		if(pc.pUnitActive != null){
			CheckMedic (pc.pUnitActive);
		}

		if(!mClientPlayer.IsMyTurn) {
			endTurnButton_Waiting.SetActive(true);
			endTurnButton.SetActive(false);

		} else {
			endTurnButton.SetActive(true);
			endTurnButton_Waiting.SetActive(false);
		}
	}

	private void CheckMedic(Unit u) 
	{
		if (u.GetType () == typeof(Medic)) 
		{
			healButton.SetActive (true);
			attackButton.SetActive (false);

		} else 
		{
			healButton.SetActive (false);
			attackButton.SetActive (true);
		}
	}
}
