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
//		pCameraMainCamera.gameObject.transform.position = pCameraMainCamera.gameObject.transform.position - pCameraMovementMainCamMov.pVec3CamMovement; 

		// Checking the movement of the camera and then offsetting it by one, so it doesn't shoot to far away
		if(pCameraMovementMainCamMov.pVec3CamMovement.x > 0)
		{
			pCameraMainCamera.gameObject.transform.position -= new Vector3(1f, 0f, 0f);
		}
		if(pCameraMovementMainCamMov.pVec3CamMovement.x < 0)
		{
			pCameraMainCamera.gameObject.transform.position += new Vector3(1f, 0f, 0f);
		}
		if(pCameraMovementMainCamMov.pVec3CamMovement.z > 0)
		{
			pCameraMainCamera.gameObject.transform.position -= new Vector3(0f, 0f, 1f);
		}
		if(pCameraMovementMainCamMov.pVec3CamMovement.z < 0)
		{
			pCameraMainCamera.gameObject.transform.position += new Vector3(0f, 0f, 1f);
		}
	}

	private IEnumerator ResetCamMovementSpeed (float mFloatWaitTime){
		yield return new WaitForSeconds (mFloatWaitTime);
		pGesturesCamGestures.pFloatCMSpeed = mFloatOldCamSpeed;
	}



}
