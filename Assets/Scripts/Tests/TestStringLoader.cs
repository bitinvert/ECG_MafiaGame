using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TestStringLoader : MonoBehaviour {

	// Use this for initialization
	void Start () {
		List<string> mLstStringText =  PrimitiveStringLoader.LoadString ("assets/level.txt");	
		foreach (string mStringLine in mLstStringText)
		{
			Debug.Log(mStringLine);
		}
		Debug.Log("Alles gelesen!");
	}

}
