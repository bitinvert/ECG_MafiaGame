using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;

public class PrefabHolderItem {
	[XmlAttribute("name")]
	public string name;
	public string prefab;
	public float x;
	public float z;

	public PrefabHolderItem(string mStringName,float mFltX, float mFltZ,string mStringPrefab){
		name = mStringName;
		x = mFltX;
		z = mFltZ;
		prefab = mStringPrefab;
	}

	public PrefabHolderItem(){

	}
}
