using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Bruiser : Unit {

	public void UseSpecial(Unit mUnitOther){
		Vector3 mPosTargetPosition = mUnitOther.gameObject.transform.position;
		List<Node> mListNeighbours = pFFWalkArea.pGridField.GetNeighbours(pFFWalkArea.pGridField.NodeFromWorldPosition(mUnitOther.gameObject.transform.position-mUnitOther.mVec3Offset));
		foreach (Node mNodeNeighbour in mListNeighbours) {
			if (mNodeNeighbour.pBoolWalkable){
				mUnitOther.gameObject.transform.position = Vector3.MoveTowards (mUnitOther.gameObject.transform.position,(mNodeNeighbour.pVec3WorldPos+mUnitOther.mVec3Offset),mUnitOther.pFloatSpeed*Time.deltaTime);

				break;
			}
		}
	}

}
