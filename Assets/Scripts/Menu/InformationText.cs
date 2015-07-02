using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InformationText : MonoBehaviour {


	public Text enemyName;
	// Use this for initialization
	void Start () {
		GameObject go = GameObject.FindWithTag ("Client");
		Client client = (Client)go.GetComponent (typeof(Client));
		enemyName.text = client.opponentName;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
