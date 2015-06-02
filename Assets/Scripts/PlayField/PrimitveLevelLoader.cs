using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class PrimitveLevelLoader : MonoBehaviour {

	//private List<string> mLstStringLevel;
	private GameObject pGOFloor;
	private GameObject pGOWall;
	private GameObject pGOCover;

	public PrimitveLevelLoader(GameObject mGOFloor,GameObject mGOWall,GameObject mGOCover){
		pGOFloor=mGOFloor;
		pGOWall=mGOWall;
		pGOCover=mGOCover;
	}

	public GameObject StartLevelCreation(string mStringPath){
		List<string> mLstStringLevel = PrimitiveStringLoader.LoadString(mStringPath);
		return CreateLevel (mLstStringLevel);
	}
	/*
	 * Creates the level in the scene out of string List, containg tags for the differnet field types. 
	 * Author: Sven Magnussen
	 * <para>List &lt; string &gt;  mLstString -> a list containing the single level file lines to create the level step by step </para>
	 * 
	 * */
	private GameObject CreateLevel(List<string> mLstStringLevel){
		GameObject mGORoot = Instantiate(new GameObject());
		mGORoot.name= "Level";
		for (int mIntXAxis = 0; mIntXAxis < mLstStringLevel.Count; mIntXAxis = mIntXAxis +1) {
			CreateLine(mLstStringLevel[mIntXAxis],mIntXAxis,mGORoot);
		}
		return mGORoot;
	}

	private void CreateLine(string mStringLine, int mIntXPos,GameObject mGORoot){
		char[] mAryCharFields = mStringLine.ToCharArray ();
		for(int mIntZAxis = 0;mIntZAxis<mAryCharFields.Length;mIntZAxis= mIntZAxis+1) {
			GameObject mGOField=null;
			switch(mAryCharFields[mIntZAxis]){
			case 'F' : 
				mGOField = (GameObject)Instantiate(pGOFloor,new Vector3(mIntXPos,1,mIntZAxis),new Quaternion());
				break;
			case 'W':
				mGOField = (GameObject)Instantiate(pGOWall,new Vector3(mIntXPos,1,mIntZAxis),new Quaternion());
				break;
			case 'C':
				mGOField = (GameObject)Instantiate(pGOCover,new Vector3(mIntXPos,1,mIntZAxis),new Quaternion());
				break;

			}
			mGOField.transform.parent=mGORoot.transform;
		}

	}
	
}
