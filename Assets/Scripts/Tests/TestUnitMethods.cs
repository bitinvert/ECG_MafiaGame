using UnityEngine;
using System.Collections;

public class TestUnitMethods : MonoBehaviour {
	public Unit pUnitTest;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log ("StartTest");
		pUnitTest.ShowAttackRadius ();
		Debug.Log (pUnitTest.pFFWalkArea.pListGridSet.ToString());
	}
}
