using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuOverlayController : MonoBehaviour {

	public GameObject attackButton;
	public GameObject healButton;

	public GameObject endTurnButton;

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
			endTurnButton.GetComponent<Button>().interactable = false;
		} else {
			endTurnButton.GetComponent<Button>().interactable = true;
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
