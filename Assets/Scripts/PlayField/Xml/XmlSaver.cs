using UnityEngine;
using System.Collections;

public class XmlSaver {
	
	public static void SaveLevel(string path, GameObject mGOLevel){
		FieldContainer mFieldContainer = new FieldContainer ();
		
		
		Transform[] mTransAllChildren = mGOLevel.GetComponentsInChildren<Transform>();
		for(int mInCount = 1; mInCount < mTransAllChildren.Length;mInCount=mInCount+1){
			string mStringName = mTransAllChildren[mInCount].name;
			Debug.Log(mStringName);
			float mIntX = mTransAllChildren[mInCount].position.x;
			float mIntZ = mTransAllChildren[mInCount].position.z;
			Field mField = mTransAllChildren[mInCount].gameObject.GetComponent<Field>();
			Debug.Log (mField);
			string mStringPrefab = mField.pStringPrefab;
			FieldItem mFieldItem = new FieldItem(mStringName,mIntX,mIntZ,mStringPrefab);
			mFieldContainer.pLstFields.Add (mFieldItem);
		}
		mFieldContainer.Save (path);
	}
}
