using UnityEngine;
using System.Collections;

public class Gestures : MonoBehaviour {

	public float pFloatCMSpeed = 50;

	public float pFloatCZSpeed = 1;

	// Use this for initialization
	void Start () {
	
		var mTKRecPan = new TKPanRecognizer();

		mTKRecPan.gestureRecognizedEvent += (r) => 
		{
			Camera.main.transform.position -= new Vector3( mTKRecPan.deltaTranslation.x, 0, mTKRecPan.deltaTranslation.y ) / pFloatCMSpeed;
		};
		TouchKit.addGestureRecognizer(mTKRecPan);

		var mTKRecZoom = new TKPinchRecognizer();
		mTKRecZoom.gestureRecognizedEvent += ( r ) =>
		{
			Camera.main.orthographicSize += mTKRecZoom.deltaScale * pFloatCZSpeed;
		};
		TouchKit.addGestureRecognizer(mTKRecZoom);
	}
}
