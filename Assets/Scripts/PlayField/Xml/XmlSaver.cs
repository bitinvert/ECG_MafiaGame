using UnityEngine;
using System.Collections;

public class XmlSaver {

	public static void SaveLevel(string mStringPath,GameObject mGOLevel){
		SavePrefabHolders (mStringPath, mGOLevel);
		SaveFields (mStringPath, mGOLevel);
	}
	private static void SavePrefabHolders (string mStringPath, GameObject mGOLevel){
		PrefabHolderContainer mPrefabHolderContainer = new PrefabHolderContainer ();
		PrefabHolder[] mTransAllChildren = mGOLevel.GetComponentsInChildren<PrefabHolder> ();
		Debug.Log (mTransAllChildren.Length);
		for (int mIntCount = 0; mIntCount < mTransAllChildren.Length; mIntCount=mIntCount+1) {
			//Setup Vars
			string mStringName = mTransAllChildren[mIntCount].name;
			float mFltX = mTransAllChildren[mIntCount].gameObject.transform.position.x;
			float mFltZ = mTransAllChildren[mIntCount].gameObject.transform.position.z;
			string mStringPrefab = mTransAllChildren[mIntCount].pStringPrefab;
			//Store in container
			PrefabHolderItem mPrefabHolderItem = new PrefabHolderItem(mStringName,mFltX,mFltZ,mStringPrefab);
			mPrefabHolderContainer.pLstPrefabHolders.Add (mPrefabHolderItem);
			
		}
		Debug.Log (mPrefabHolderContainer.pLstPrefabHolders.Count);
		mPrefabHolderContainer.Save (mStringPath);
	}
	private static void SaveFields (string mStringPath, GameObject mGOLevel){
		FieldContainer mFieldContainer = new FieldContainer ();
		mGOLevel = GameObject.Find ("singleFields");
		Field[] mCompAllChildren = mGOLevel.GetComponentsInChildren<Field>();
		for (int mIntCount = 0; mIntCount < mCompAllChildren.Length; mIntCount=mIntCount+1) { //this part could be refactored. DRY -> Save PrefabHolders. I am not sure if this part changes in the future
			//Setup Vars
			string mStringName = mCompAllChildren[mIntCount].name;
			float mFltX = mCompAllChildren[mIntCount].gameObject.transform.position.x;
			float mFltZ = mCompAllChildren[mIntCount].gameObject.transform.position.z;
			string mStringPrefab = mCompAllChildren[mIntCount].pStringPrefab;
			FieldItem mFieldItem = new FieldItem(mStringName,mFltX,mFltZ,mStringPrefab);
			mFieldContainer.pLstFields.Add (mFieldItem);
		}
		mFieldContainer.Save (mStringPath);
	}
}
