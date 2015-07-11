using UnityEngine;
using System.Collections;

interface ObjectiveInterface {
	void ShowObjectiveStatus();
	void ExecuteEffect(Unit mExecutingUnit);
	void InteractWithObjective(Unit mInteractingUnit);
}
