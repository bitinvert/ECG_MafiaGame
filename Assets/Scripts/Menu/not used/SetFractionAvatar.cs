using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SetFractionAvatar : MonoBehaviour {

	// A reference to the police Avatar
	public GameObject[] police_avatar = new GameObject[2];

	// A reference to the mafia Avatar
	public GameObject[] mafia_avatar = new GameObject[2];

	private GameObject client;

	// A bool used to only set the Avatar once
	private bool avatarSet;
	
	void Start () {
		client = GameObject.FindGameObjectWithTag ("Client");
		avatarSet = false;
	}
	
	/**
	 * Enable the corresponding profileButton and Avatar
	 */
	void Update () {
		if ((client == null) && (!avatarSet)) {
			
			client = GameObject.FindGameObjectWithTag ("Client");

		} else if(!avatarSet)
		{
			Client c = client.GetComponent<Client>();

			if(c.IsMafia) 
			{
				mafia_avatar[0].SetActive(true);
				mafia_avatar[1].SetActive(true);

			} else if(c.IsPolice) 
			{
				police_avatar[0].SetActive(true);
				police_avatar[1].SetActive(true);
			}

			avatarSet = true;

		}	
	}
}
