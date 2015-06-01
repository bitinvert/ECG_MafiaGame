using UnityEngine;
using System.Collections;

public class LevelController : MonoBehaviour {
	/*
	 * Creates the level in the scene out of a text file or a xml file. 
	 * Author: Sven Magnussen
	 * <para> GameObject pGOFloor = reference to prefab | GameObject pGOWall = reference to prefab | GameObject pGOCover = reference to prefab |
	 * string pStringTypeOfLevelLoader = defines which levelloader wll be used. use "xml" for XmlLoader. use "primitve" for primitveLoader</para>
	 * 
	 * */
	public GameObject pGOFloor;
	public GameObject pGOWall;
	public GameObject pGOCover;
	public string pStringTypeOfLevelLoader;
	// Use this for initialization
	void Start () {
		switch(pStringTypeOfLevelLoader){
		case "primitve"	:	PrimitveLevelLoader mLevelLoader = new PrimitveLevelLoader (pGOFloor,pGOWall,pGOCover);
							mLevelLoader.StartLevelCreation ("assets/level.txt");
							break;
		case "xml"		:	XmlLoader.LoadLevel ("assets/level.xml");
							break;
		}
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
