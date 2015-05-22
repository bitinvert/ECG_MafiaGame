using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Collections.Generic;

public class PrimitiveStringLoader : MonoBehaviour {
	

	public static List<String> LoadString(string mStringPath){
		//Setup the file reader
		List<string> mLstStringLines = new List<string>();
		FileInfo mFileInfoText = new FileInfo (mStringPath);
		StreamReader mStreamReader = mFileInfoText.OpenText ();
		//Reading the file
		string mStringText;
		do {
			mStringText = mStreamReader.ReadLine ();
			mLstStringLines.Add (mStringText);
		} while(mStringText !=null);
		//remove last element because it is null
		mLstStringLines.RemoveAt(mLstStringLines.Count-1);
		//Return List with the lines of the text in string format
		return mLstStringLines;
	}
}
