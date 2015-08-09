using UnityEngine;
using System.Collections;

public class ExitGame : MonoBehaviour 
{

	/**
	 * Check if the player wants to exit the game.
	 * For Debugging only!
	 */
	void Update () 
	{
		if (Input.GetKey ("escape")) 
		{
			Application.Quit ();
		}
	}

	/**
	 * Exit the game if called.
	 */
	public void ExitGameOption() 
	{
		Application.Quit ();
	}
}
