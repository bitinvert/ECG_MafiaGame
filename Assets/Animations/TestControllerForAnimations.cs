using UnityEngine;
using System.Collections;

public class TestControllerForAnimations : MonoBehaviour {
	
	public ShowAnimation showanim;
	int counter;

	// Use this for initialization
	void Start () {
		counter = 0;	
	}
	
	// Update is called once per frame
	void Update () {
		counter += 1;
		switch (counter) {
		case (1):
			showanim.showAni(0,1);
			break;
		case (50):
			showanim.showAni(0,2);
			break;
		case (80):
			showanim.showAni(0,3);
			break;
		case (100):
			showanim.showAni(0,4);
			break;
		case (130):
			showanim.showAni(4,4);
			break;
		}

	}
}
