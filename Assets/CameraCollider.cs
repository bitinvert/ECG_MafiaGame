using UnityEngine;
using System.Collections;

public class CameraCollider : MonoBehaviour {

	public GameObject pCamMainCam;

	void OnTriggerExit( Collider other){

			Debug.Log ("Test");
			pCamMainCam.gameObject.transform.position.Set (0,0,0);

	}
}
