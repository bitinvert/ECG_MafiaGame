using UnityEngine;
using System.Collections;

public class LevelEditor : MonoBehaviour {
	public bool pBoolLoadOnStart;
	public bool pBoolSaveOnClose;
	public LevelController pLevelController;
	// Use this for initialization
	void Start () {
		if (pBoolLoadOnStart) {
			pLevelController.LoadLevel ("assets/safedLevel"); //Load Level for fields and solve problem with xml merge mybe 2 files?
			Debug.Log ("Load");	
		}
	}
	
	// Update is called once per frame
	void OnApplicationQuit() {
		if (pBoolSaveOnClose){
			pLevelController.SaveLevel ("assets/safedLevel");
			Debug.Log("Save");
		}
	}
}
