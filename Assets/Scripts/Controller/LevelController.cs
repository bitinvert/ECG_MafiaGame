﻿using UnityEngine;
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
	private GameObject mGOLevel;
	// Use this for initialization
	public void LoadLevel () {
		switch(pStringTypeOfLevelLoader){
		case "primitve"	:	PrimitveLevelLoader mLevelLoader = new PrimitveLevelLoader (pGOFloor,pGOWall,pGOCover);
							this.mGOLevel = mLevelLoader.StartLevelCreation ("assets/level.txt");
							break;
		case "xml"		:	this.mGOLevel = XmlLoader.LoadLevel ("assets/level.xml");
							break;
		}
	
	}
	public void SaveLevel(){
		XmlSaver.SaveLevel ("assets/safedLevel.xml",mGOLevel);
	}
	// Update is called once per frame
	void Update () {
		
	}
}
