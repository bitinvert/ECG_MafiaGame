using UnityEngine;
using System.Collections;

public class Medic : Unit {
	public void UseSpecial(Unit mUnitOther){
		if (mUnitOther.pBoolEnemy) {
			mUnitOther.pIntHealth = mUnitOther.pIntHealth + 4;
			if (mUnitOther.pIntHealth>12){
				mUnitOther.pIntHealth = 12;
			}
		}
	}
}
