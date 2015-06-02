using UnityEngine;
using System.Collections;

public class LoadSafeTest : MonoBehaviour {
	public LevelController pLevelController;
	// Use this for initialization
	void Start () {
		/*pLevelController.LoadLevel ();
		Debug.Log ("Load");
		System.Threading.Thread.Sleep(5000);*/
		pLevelController.SaveLevel ();
		Debug.Log ("Safe");
	}
	// Update is called once per frame
	void Update () {
	
	}
}
