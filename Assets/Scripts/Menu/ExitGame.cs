using UnityEngine;
using System.Collections;

public class ExitGame : MonoBehaviour 
{

	void Update () 
	{
		if (Input.GetKey ("escape")) 
		{
			Application.Quit ();
		}
	}

	public void ExitGameOption() 
	{
		Application.Quit ();
	}
}
