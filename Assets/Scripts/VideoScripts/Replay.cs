using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Replay : MonoBehaviour {

	public Unit unitA;
	public Unit unitB;
	public List<Vector3> targetsA;
	public List<Vector3> targetsB;
	public int maxRounds;
	private int round = 1;
	private int moveCountA = 0;
	private int moveCountB = 0;

	// Use this for initialization
	void Start () {
//		unitA.pAStarPathfinding.FindPath (unitA.transform.position, targetsA [moveCountA], unitA.mVec3Offset);
//		unitB.pAStarPathfinding.FindPath (unitB.transform.position, targetsB [moveCountB], unitB.mVec3Offset);
	}
	
	// Update is called once per frame
	void Update () {

		if (round == 1 && moveCountA != maxRounds) {
			if (unitA.transform.position - unitA.mVec3Offset != targetsA[moveCountA]) {
				unitA.move ();
			} else {
				round = 2;
				moveCountA += 1;
			}
		}

		if (round == 2 && moveCountB != maxRounds) {
			if (unitB.transform.position - unitB.mVec3Offset != targetsB[moveCountB]) {
				unitB.move ();
			} else {
				round = 1;
				moveCountB += 1;
			}
		}
	
	}
}
