using UnityEngine;
using System.Collections;

public class Gestures : MonoBehaviour {

	public int pIntCMSpeed = 50;

	// Use this for initialization
	void Start () {
	
		var mTKRecPan = new TKPanRecognizer();

		mTKRecPan.gestureRecognizedEvent += (r) => 
		{
			Camera.main.transform.position -= new Vector3( mTKRecPan.deltaTranslation.x, 0, mTKRecPan.deltaTranslation.y ) / pIntCMSpeed;
		};
		TouchKit.addGestureRecognizer(mTKRecPan);
	}
}
