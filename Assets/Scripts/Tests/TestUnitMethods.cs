using UnityEngine;
using System.Collections;

public class TestUnitMethods : MonoBehaviour {
	public GameController pGCTester;
	// Update is called once per frame
	void Update () {
		if(pGCTester.pTransSeeker != null){
			int mIntUnitIndex = pGCTester.pListCharacters.IndexOf(pGCTester.pTransSeeker.GetComponent<Unit>());

			pGCTester.pListCharacters[mIntUnitIndex].pFFWalkArea.pListGridSet.Clear ();
			//Debug.Log ("StartTest");
			if(!pGCTester.pListCharacters[mIntUnitIndex].pBoolDoubleTap){
				pGCTester.pListCharacters[mIntUnitIndex].ShowMovementRadius ();
				//Debug.Log (pUnitTest.pFFWalkArea.pListGridSet.ToString());
				pGCTester.pListCharacters[mIntUnitIndex].pFFWalkArea.pGridField.SetGrid();
		
			}
			else
			{
				pGCTester.pListCharacters[mIntUnitIndex].pFFWalkArea.pGridField.ResetGrid();
			}
		}
	}
	
}
