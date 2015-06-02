using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;

public class FieldItem {
	[XmlAttribute("name")]
	public string name;

	public float x;
	public float z;
	public string prefab;

	public FieldItem(string mStringName, float mIntX, float mIntZ, string mStringPrefab){
		name = mStringName;
		x = mIntX;
		z = mIntZ;
		prefab = mStringPrefab;
	}
	public FieldItem(){

	}
}
