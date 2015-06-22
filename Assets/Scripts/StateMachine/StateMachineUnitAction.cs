using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*Simple State Machine realised through methods no outer resources used*/
public class StateMachineUnitAction : MonoBehaviour {

	public PlayerController pPCPlayer;
	public int pIntTurnCount;


	// Use this for initialization
	void Start () {
		pPCPlayer = Object.FindObjectOfType(typeof(PlayerController)) as PlayerController;
		pIntTurnCount = 0;
	}
	
	// Update is called once per frame
	void Update () {
		/*The States themselves.
		  Here the certain values will be set*/
		if(SelectedChar ())
		{
			Debug.Log ("State: Character Selected");
		}
		if(ShowAttack ())
		{
			Debug.Log ("State: Show Attack");
		}
		if(ShowMove ())
		{
			Debug.Log ("State: Show Move");
		}
		if(ShowSpecial ())
		{
			Debug.Log ("State: Show Special");
		}
		if(AttackDone ())
		{
			Debug.Log ("State: Attack Done");
		}
		if(MoveDone ())
		{
			Debug.Log ("State: Move Done");
		}
		if(SpecialDone ())
		{
			Debug.Log ("State: Special Done");
		}
		if(PlayerChange ())
		{
			Debug.Log ("State Player Change");
		}
	}
	/*Checking the conditions and extra features*/
	bool SelectedChar(){return false;}

	bool ShowAttack(){return false;}

	bool ShowMove(){return false;}

	bool ShowSpecial(){return false;}

	bool AttackDone(){return false;}

	bool MoveDone(){return false;}

	bool SpecialDone(){return false;}

	bool PlayerChange(){return false;}
}
