﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {
	// Use this for initialization
	public Transform pTransSeeker;


	public List<PlayerController> pListCharacters;
		
	void Start () {
		var mTKRecCharTap = new TKTapRecognizer();
		mTKRecCharTap.gestureRecognizedEvent += (r) =>
		{


			Vector3 mVec3TapPos = new Vector3(mTKRecCharTap.touchLocation().x,
			                                  mTKRecCharTap.touchLocation().y,
			                                  0f);
			RaycastHit mRHInfo = new RaycastHit();
			if(!pTransSeeker && Physics.Raycast(Camera.main.ScreenPointToRay(mVec3TapPos), out mRHInfo))
			{
				if(mRHInfo.collider.gameObject.tag == "Playable")
				{

					pTransSeeker = mRHInfo.collider.gameObject.transform;
				
				}
			}
			else if(pTransSeeker && Physics.Raycast(Camera.main.ScreenPointToRay(mVec3TapPos), out mRHInfo))
			{
				if(mRHInfo.collider.tag == "Field" && 
				   pListCharacters[pListCharacters.IndexOf(
					pTransSeeker.GetComponent<PlayerController>())].pGOTarget == null)
				{
					pListCharacters[pListCharacters.IndexOf(
						pTransSeeker.GetComponent<PlayerController>())].pGOTarget = mRHInfo.collider.gameObject;

				}
				else if(mRHInfo.collider.tag == "Field")
				{
					GameObject mGOTemp =  mRHInfo.collider.gameObject;

					if(pListCharacters[pListCharacters.IndexOf(
						pTransSeeker.GetComponent<PlayerController>())].pGOTarget.Equals(mGOTemp))
					{
						pListCharacters[pListCharacters.IndexOf(
							pTransSeeker.GetComponent<PlayerController>())].pBoolDoubleTap = true;
					}
					else
					{
						pListCharacters[pListCharacters.IndexOf(
							pTransSeeker.GetComponent<PlayerController>())].pGOTarget = mGOTemp;
					}
				}
			}
		};
		TouchKit.addGestureRecognizer(mTKRecCharTap);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
