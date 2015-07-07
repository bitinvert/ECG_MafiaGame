using UnityEngine;
using System.Collections;

public class RunAway : MonoBehaviour {

	public Unit movingUnitA;
	public Unit movingUnitB;
	public Vector3 targetField;

	// Use this for initialization
	void Start () {
		movingUnitA.pAStarPathfinding.FindPath (movingUnitA.transform.position, targetField, movingUnitA.mVec3Offset);
		movingUnitB.pAStarPathfinding.FindPath (movingUnitB.transform.position, targetField, movingUnitB.mVec3Offset);
	}
	
	// Update is called once per frame
	void Update () {

		if (movingUnitA != null) {
			movingUnitA.move ();
		}

		if (movingUnitB != null) {
			movingUnitB.move ();
		}
	
	}
}
