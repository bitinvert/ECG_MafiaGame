using UnityEngine;
using System.Collections;

public class CameraCollider : MonoBehaviour {
	public Gestures pGesturesCamGestures;
	public CameraMovement pCameraMovementMainCamMov;
	public Camera pCameraMainCamera;
	private float mFloatOldCamSpeed;


	void OnTriggerExit( Collider other){
		Debug.Log("Raus");
		mFloatOldCamSpeed = pGesturesCamGestures.pFloatCMSpeed;
		pGesturesCamGestures.pFloatCMSpeed = 0;
		StartCoroutine(ResetCamMovementSpeed(1));
		pCameraMainCamera.gameObject.transform.position = pCameraMainCamera.gameObject.transform.position - pCameraMovementMainCamMov.pVec3CamMovement; 

	}

	private IEnumerator ResetCamMovementSpeed (float mFloatWaitTime){
		yield return new WaitForSeconds (mFloatWaitTime);
		pGesturesCamGestures.pFloatCMSpeed = mFloatOldCamSpeed;
	}



}
