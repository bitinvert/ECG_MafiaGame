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

	public void StartLevelCreation(string mStringPath){
		List<string> mLstStringLevel = PrimitiveStringLoader.LoadString(mStringPath);
		CreateLevel (mLstStringLevel);
	}

	private void CreateLevel(List<string> mLstStringLevel){
		for (int mIntXAxis = 0; mIntXAxis < mLstStringLevel.Count; mIntXAxis = mIntXAxis +1) {
			CreateLine(mLstStringLevel[mIntXAxis],mIntXAxis);
		}
	}

	private void CreateLine(string mStringLine, int mIntXPos){
		char[] mAryCharFields = mStringLine.ToCharArray ();
		for(int mIntZAxis = 0;mIntZAxis<mAryCharFields.Length;mIntZAxis= mIntZAxis+1) {
			switch(mAryCharFields[mIntZAxis]){
			case 'F' : 
				Instantiate(pGOFloor,new Vector3(mIntXPos,1,mIntZAxis),new Quaternion());
				break;
			case 'W':
				Instantiate(pGOWall,new Vector3(mIntXPos,1,mIntZAxis),new Quaternion());
				break;
			case 'C':
				Instantiate(pGOCover,new Vector3(mIntXPos,1,mIntZAxis),new Quaternion());
				break;

			}
		}

	}
	
}
