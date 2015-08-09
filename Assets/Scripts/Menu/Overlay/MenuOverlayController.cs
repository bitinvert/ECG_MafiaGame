using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuOverlayController : MonoBehaviour {

	// References to the graphical Buttons in the Overlay
	public GameObject attackButton;
	public GameObject healButton;

	public GameObject endTurnButton;
	public GameObject endTurnButton_Waiting;

	// Used to determine the most recently clicked unit etc.
	public PlayerController pc;

	private Client mClientPlayer;

	// Get an active instance of the Client
	void Start() {
		mClientPlayer = Object.FindObjectOfType(typeof(Client)) as Client;
	}

	/**
	 * Check if the active Unit is a medic --> If so, call CheckMedic()
	 * Also: Check whether it's the current's users turn. If that's the
	 * case activate the endTurnButton and disable the waiting Button.
	 * If not: Deactive the endTurnButton and enable the waiting Button.
	 */
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

	/**
	 * If the active unit is a medic, the attack Button is
	 * supposed to be switched with the healButton.
	 * The Functionality stays the same, only the appearance
	 * is supposed to change.
	 */
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
