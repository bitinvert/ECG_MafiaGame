using UnityEngine;
using System.Collections;

public class Safe : Field,ObjectiveInterface {

	public int pIntCounter = 5;
	public bool pBoolOpen = false;

	public 	void ShowObjectiveStatus(){
		Debug.Log (pIntCounter);
	}
	public void ExecuteEffect(Unit mExecutingUnit){
		pBoolOpen = true;
		mExecutingUnit.pBoolHasLoot = true;
	}
	public void InteractWithObjective(Unit mInteractingUnit){
		if (!pBoolOpen) {
			if (mInteractingUnit.GetType ()is Beagleboy) {
				pIntCounter = 0;
				ExecuteEffect (mInteractingUnit);
			} else {
				pIntCounter = pIntCounter - 1;
				if (pIntCounter == 0) {
					ExecuteEffect (mInteractingUnit);
				}
			}
		}
	}
}
