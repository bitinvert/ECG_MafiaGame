using UnityEngine;
using System.Collections;

public class Slicer : Unit {


	public void UseSpecial(Unit mUnitOther){
		if (mUnitOther.pBoolEnemy) {
			mUnitOther.pShackStunned.isSheckled= true;
			mUnitOther.pShackStunned.shackleTime = 2;
		}
	}
}
