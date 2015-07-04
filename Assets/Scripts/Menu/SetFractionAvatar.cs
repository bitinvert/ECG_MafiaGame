using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SetFractionAvatar : MonoBehaviour {

	public GameObject[] police_avatar = new GameObject[2];
	public GameObject[] mafia_avatar = new GameObject[2];
	private GameObject client;
	private bool avatarSet;

	// Use this for initialization
	void Start () {
		client = GameObject.FindGameObjectWithTag ("Client");
		avatarSet = false;
	}
	
	// Update is called once per frame
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
