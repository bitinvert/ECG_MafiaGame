using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class XmlLoader : MonoBehaviour {
	/*
	 * Creates the level in the scene using deserialized items, which were stored in a xml file. 
	 * Author: Sven Magnussen
	 * <para>string mStringPath = path to the xml file </para>
	 * 
	 * */
	public static void LoadLevel (string mStringPath){
		FieldContainer mFieldContainer = FieldContainer.Load (mStringPath);
		GameObject mGORoot = null;
		mGORoot = new GameObject ();
		mGORoot.name = "level";
		InstantiateFields (mFieldContainer.pLstFields,mGORoot);
	}
	private static void InstantiateFields(List<FieldItem> mLstFields,GameObject mGORoot){
		foreach (FieldItem mFieldItem in mLstFields) {
			Vector3 mVector3Pos = new Vector3(mFieldItem.x,1,mFieldItem.z);
			GameObject mGOField = (GameObject)Instantiate(Resources.Load (mFieldItem.prefab),mVector3Pos,new Quaternion());
			mGOField.name = mFieldItem.name;
			mGOField.transform.parent = mGORoot.transform;
		}
	}

}
