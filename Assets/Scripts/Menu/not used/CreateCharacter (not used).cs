using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CreateCharacter : MonoBehaviour {

	public void Continue() {
		InputField nameField = GameObject.FindGameObjectWithTag ("CharacterName").GetComponent<InputField> ();
		string name = nameField.text;

		if(CheckNameAvaibility(name)) {
			Debug.Log ("name ok");
		} else {
			GameObject.FindGameObjectWithTag("NameUsedText").GetComponent<CanvasGroup>().alpha = 1.0f;
		}
	}
	
	// Idea: Ask the Server if the name is already taken,
	// return true if name is available, else false
	private bool CheckNameAvaibility(string name) {
		if (name.Equals (""))
		{
			return false;
		}
		if (name.Equals ("test"))
		{
			return false;
		}
		return true;
	}
}
