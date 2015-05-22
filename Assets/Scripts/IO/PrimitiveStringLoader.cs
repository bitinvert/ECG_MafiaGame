using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Collections.Generic;

public class PrimitiveStringLoader : MonoBehaviour {
	/*
	 * Static method to read text out of a file 
	 * Author: Sven Magnussen
	 * <para>String mStringPath The Path to the textfile </para>
	 * <returns> List &lt; string &gt Returns a list. String elements of this list contain a single line of the text </returns>
	 * */

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
