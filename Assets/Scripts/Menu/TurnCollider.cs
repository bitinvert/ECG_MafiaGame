using UnityEngine;
using System.Collections;

public class TurnCollider : MonoBehaviour {

	private Client mClientPlayer;
	public GameObject coll;
	// Use this for initialization
	void Start () {
		mClientPlayer = Object.FindObjectOfType(typeof(Client)) as Client;
	}
	
	// Update is called once per frame
	void Update () {
		if(!mClientPlayer.IsMyTurn) {
			coll.SetActive(true);
			Debug.Log ("coll acive");
		} else {
			coll.SetActive(false);
		}
	}
}
