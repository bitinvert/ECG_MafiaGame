using UnityEngine;
using System.Collections;

public class MenuOverlayController : MonoBehaviour {

	public GameObject attackButton;
	public GameObject healButton;
	public PlayerController pc;
	
	// Update is called once per frame
	void Update () {
		if(pc.pUnitActive != null){
			CheckMedic (pc.pUnitActive);
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
