using System.IO;

public class FileReader{
	/*
	 * Static method to read text out of a file and creates string containing the chars
	 * Author: Sven Magnussen
	 * <para>String mStringPath The Path to the textfile </para>
	 * <returns> string mStringtext = string with all chars of the textfile </returns>
	 * */
	
	public static string FileToString(string mStringPath){
		FileInfo mFileInfoText = new FileInfo (mStringPath);
		StreamReader mStreamReader = mFileInfoText.OpenText ();
		string mStringText = "";
		string mStringLine;
		do {
			mStringLine = mStreamReader.ReadLine ();
			mStringText = mStringText + mStringLine;
		} while(mStringLine !=null);
		return mStringText;
	}
}

