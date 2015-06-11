using UnityEngine;
using System.Collections;

public class LoadSafeTest : MonoBehaviour {
	public LevelController pLevelController;
	// Use this for initialization
	void Start () {
		//pLevelController.LoadLevel ("assets/safedLevel"); //Load Level for fields and solve problem with xml merge mybe 2 files?
		Debug.Log ("Load");
		//System.Threading.Thread.Sleep(5000);
		//pLevelController.SaveLevel ("assets/safedLevel");
		//Debug.Log ("Safe");
	}
	// Update is called once per frame
	void Update () {
	
	}
}
