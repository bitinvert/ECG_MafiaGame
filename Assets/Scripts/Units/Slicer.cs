using UnityEngine;
using System.Collections;

public class Slicer : Unit {
	public void UseSpecial(Unit mUnitOther){
		if (mUnitOther.pBoolEnemy) {
			mUnitOther.pBoolCaptive = true;
		}
	}
}
