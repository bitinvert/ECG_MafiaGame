using UnityEngine;
using System.Collections;

public class RunAway : MonoBehaviour {

	public Unit movingUnitA;
	public Unit movingUnitB;
	public Vector3 targetField;

	// Use this for initialization
	void Start () {
		movingUnitA.ResetValues();
//		movingUnitA.pAStarPathfinding.FindPath (movingUnitA.transform.position-movingUnitA.mVec3Offset, targetField);
//		movingUnitB.pAStarPathfinding.FindPath (movingUnitB.transform.position-movingUnitB.mVec3Offset,targetField);
	}
	
	// Update is called once per frame
	void Update () {
		if(movingUnitA != null/* || movingUnitA.gameObject.transform.position - movingUnitA.mVec3Offset != targetField*/)
			movingUnitA.move ();
		//if(movingUnitB != null && movingUnitA == null)
		//	movingUnitB.move ();
	}
}
