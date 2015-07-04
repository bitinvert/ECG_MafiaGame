using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Bruiser : Unit {

	public void UseSpecial(Unit mUnitOther){
		if (mUnitOther.pBoolEnemy) {
			List<Node> mListNeighbours = pFFWalkArea.pGridField.GetNeighbours (pFFWalkArea.pGridField.NodeFromWorldPosition (mUnitOther.gameObject.transform.position - mUnitOther.mVec3Offset));
			foreach (Node mNodeNeighbour in mListNeighbours) {
				if (mNodeNeighbour.pBoolWalkable) {
					Vector3 mPosTargetPosition = mUnitOther.gameObject.transform.position;
					pAStarPathfinding.FindPath (this.gameObject.transform.position - this.mVec3Offset, mPosTargetPosition - mUnitOther.mVec3Offset, this.mVec3Offset);
					this.move ();
					mUnitOther.gameObject.transform.position = Vector3.MoveTowards (mUnitOther.gameObject.transform.position, (mNodeNeighbour.pVec3WorldPos + mUnitOther.mVec3Offset), mUnitOther.pFloatSpeed * Time.deltaTime);
					break;
				}
			}
		}
	}

}
