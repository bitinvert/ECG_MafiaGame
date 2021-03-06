﻿using UnityEngine;
using System.Collections;

public class Gestures : MonoBehaviour {

	public float pFloatCMSpeed = 25;

	public float pFloatCZSpeed = 5;

	// Use this for initialization
	void Start () {
	
		var mTKRecPan = new TKPanRecognizer();

		mTKRecPan.gestureRecognizedEvent += (r) => 
		{
			if (pFloatCMSpeed != 0){
				Vector3 mVec3Dir = new Vector3(mTKRecPan.deltaTranslation.x , 0, mTKRecPan.deltaTranslation.y ) / pFloatCMSpeed;
				mVec3Dir = Quaternion.Euler (0f,45f,0f) * mVec3Dir;
				Camera.main.transform.Translate(-mVec3Dir,Space.World);
			}
		};
		TouchKit.addGestureRecognizer(mTKRecPan);

		var mTKRecZoom = new TKPinchRecognizer();
		mTKRecZoom.gestureRecognizedEvent += ( r ) =>
		{
			if (Camera.main.orthographicSize >= 1f && Camera.main.orthographicSize <= 10f) {
				Camera.main.orthographicSize += mTKRecZoom.deltaScale*pFloatCZSpeed;
			} else if (Camera.main.orthographicSize < 1f){
				Camera.main.orthographicSize = 1f;
			} else if (Camera.main.orthographicSize > 10f) {
				Camera.main.orthographicSize = 10f;
			}
		};
		TouchKit.addGestureRecognizer(mTKRecZoom);

	}


}
