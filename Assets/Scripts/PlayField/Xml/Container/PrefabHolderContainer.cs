using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
[XmlRoot("xmlLevel")]
public class PrefabHolderContainer {
	[XmlArray("prefabholders")]
	[XmlArrayItem("prefabholder")]
	public List<PrefabHolderItem> pLstPrefabHolders = new List<PrefabHolderItem>();

	public void Save(string path)
	{
		var serializer = new XmlSerializer(typeof(PrefabHolderContainer));
		using(var stream = new FileStream(path, FileMode.Create))
		{
			serializer.Serialize(stream, this);
		}
	}
	public static PrefabHolderContainer Load(string path)
	{
		var serializer = new XmlSerializer(typeof(PrefabHolderContainer));
		using(var stream = new FileStream(path, FileMode.Open))
		{
			return serializer.Deserialize(stream) as PrefabHolderContainer;
		}
	}
}
