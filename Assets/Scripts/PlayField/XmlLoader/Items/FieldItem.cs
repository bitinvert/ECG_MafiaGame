using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;

public class FieldItem {
	[XmlAttribute("name")]
	public string name;

	public int x;
	public int z;
	public string prefab;
}
