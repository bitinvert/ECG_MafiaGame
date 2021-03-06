﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
using System.Text;

[XmlRoot("xmlLevel")]
public class FieldContainer {
	
	[XmlArray("fields")]
	[XmlArrayItem("field")]
	public List<FieldItem> pLstFields = new List<FieldItem>();
	/*
	 * Creates an container around a list, filled with deserialized items out of a xml file 
	 * Author: Sven Magnussen
	 * <para>string path = path to the xml file </para>
	 * 
	 * */
	public static FieldContainer Load(string path)
	{
		var serializer = new XmlSerializer(typeof(FieldContainer));
		using(var stream = new FileStream(path, FileMode.Open))
		{
			return serializer.Deserialize(stream) as FieldContainer;
		}
	}
	public static FieldContainer LoadFromString(string mString){
		var serializer = new XmlSerializer(typeof(FieldContainer));
		Stream mStream = new MemoryStream(Encoding.UTF8.GetBytes(mString ?? ""));
		return serializer.Deserialize (mStream) as FieldContainer;
	}
	public void Save(string path)
	{
		var serializer = new XmlSerializer(typeof(FieldContainer));
		using(var stream = new FileStream(path, FileMode.Create))
		{
			serializer.Serialize(stream, this);

		}
	}
	
}
