using UnityEngine;
using System.Collections;

public class CameraCollider : MonoBehaviour {

	public GameObject pCamMainCam;

	void OnTriggerExit( Collider other){

		Debug.Log("Raus");	
		pCamMainCam.gameObject.transform.position = new Vector3 (0,0,0);

	}


}
