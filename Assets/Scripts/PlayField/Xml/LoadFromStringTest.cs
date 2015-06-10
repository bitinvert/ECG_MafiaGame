using UnityEngine;
using System.Collections;
using System.IO;

public class LoadFromStringTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
		string mStringSingleFields = FileReader.FileToString ("assets/XmlLevels/TestWith3DModels_singleFields.xml");
		string mStringPrefabHolders = FileReader.FileToString ("assets/XmlLevels/TestWith3DModels_prefabHolder.xml");
		XmlLoader.LoadLevelFromString (mStringPrefabHolders,mStringSingleFields);
	}


	
	// Update is called once per frame
	void Update () {
	
	}
}
