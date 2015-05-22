using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
	public GameObject pGOFloor;
	public GameObject pGOWall;
	public GameObject pGOCover;
	// Use this for initialization
	void Start () {
		PrimitveLevelLoader mLevelLoader = new PrimitveLevelLoader (pGOFloor,pGOWall,pGOCover);
		mLevelLoader.StartLevelCreation ("assets/level.txt");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
