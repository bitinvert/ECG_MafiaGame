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
	public static GameObject LoadLevel (string mStringPath){
		PrefabHolderContainer mPrefabHolderContainer = PrefabHolderContainer.Load (mStringPath + "_prefabHolder.xml");
		FieldContainer mFieldContainer = FieldContainer.Load (mStringPath + "_singleFields.xml");
		GameObject mGORoot = new GameObject ();
		mGORoot.name = "level";
		InstantiatePrefabHolder (mPrefabHolderContainer.pLstPrefabHolders,mGORoot);
		InstantiateFields (mFieldContainer.pLstFields,mGORoot);
		return mGORoot;
	}
	private static void InstantiatePrefabHolder(List<PrefabHolderItem> mLstFields,GameObject mGORoot){
		foreach (PrefabHolderItem mFieldItem in mLstFields) {
			Vector3 mVector3Pos = new Vector3(mFieldItem.x,0,mFieldItem.z);
			GameObject mGOField = (GameObject)Instantiate(Resources.Load (mFieldItem.prefab),mVector3Pos,new Quaternion());
			mGOField.name = mFieldItem.name;
			mGOField.transform.parent = mGORoot.transform;
		}
	}
	private static void InstantiateFields(List<FieldItem> mLstFields,GameObject mGORoot){
		GameObject mGOSingleFields = new GameObject ();
		mGOSingleFields.name = "singleFields";
		foreach (FieldItem mFieldItem in mLstFields) {
			Vector3 mVector3Pos = new Vector3(mFieldItem.x,0,mFieldItem.z);
			GameObject mGOField = (GameObject)Instantiate(Resources.Load (mFieldItem.prefab),mVector3Pos,new Quaternion());
			mGOField.name = mFieldItem.name;
			mGOField.transform.parent = mGOSingleFields.transform;
		}
		mGOSingleFields.transform.parent = mGORoot.transform;
	}


}
