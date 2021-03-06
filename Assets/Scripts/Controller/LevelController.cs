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

	public void LoadLevel (string prefabs, string fields) {
		switch(pStringTypeOfLevelLoader){
		case "xml"		:	this.mGOLevel = XmlLoader.LoadLevelFromString (prefabs, fields);
			break;
		}
		
	}
	public void LoadLevel(string mStringLoadName){
		switch(pStringTypeOfLevelLoader){
		case "primitve"	:	PrimitveLevelLoader mLevelLoader = new PrimitveLevelLoader (pGOFloor,pGOWall,pGOCover);
			//this.mGOLevel = mLevelLoader.StartLevelCreation (path);
			break;
		case "xml"		:	this.mGOLevel = XmlLoader.LoadLevel (mStringLoadName);
			break;
		}
	}

//	public bool GetLoadStatus() {
//		return (XmlLoader.prefabsLoaded && XmlLoader.fieldsLoaded);
//	}

	public void SaveLevel(string path){
		if (mGOLevel == null) {
			mGOLevel = GameObject.Find ("level");
		}
			XmlSaver.SaveLevel (path, mGOLevel);
	}
}
