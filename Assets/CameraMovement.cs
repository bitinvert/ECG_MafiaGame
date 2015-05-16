using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {

	public Vector3 pVec3CamPos;
	public Vector3 pVec3CamMovement;

	// Use this for initialization
	void Start () {
		pVec3CamPos = Vector3.zero;
	}
	
	// Update is called once per frame
	void Update () {
		pVec3CamMovement = (transform.position - pVec3CamPos) / Time.deltaTime;
		pVec3CamMovement = Quaternion.Euler (0f, 45f, 0f) * pVec3CamMovement;
		pVec3CamPos = transform.position;
	}
}
