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
		if(pPCPlayer.pBoolShowAttack == true && SelectedChar ())
		{
			ShowAttack();
		}
		else if(pPCPlayer.pBoolShowMove == true && SelectedChar ())
		{
			ShowMove ();
		}
		else if(pPCPlayer.pBoolShowSpecial == true && SelectedChar ())
		{
			ShowSpecial ();
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
		if(pPCPlayer.pBoolEndTurn == true)
		{
			PlayerChange();
		}
	}
	/*Checking the conditions and extra features*/
	bool SelectedChar(){ 
		return(pPCPlayer.pUnitActive != null);
	}

	void ShowAttack()
	{
		pPCPlayer.pUnitActive.pFFWalkArea.pListGridSet.Clear ();
		pPCPlayer.pUnitActive.pFFWalkArea.pGridField.ResetGrid();
		pPCPlayer.pUnitActive.ShowAttackRadius();
		pPCPlayer.pUnitActive.pFFWalkArea.pGridField.SetGrid();
	}

	void ShowMove(){
		pPCPlayer.pUnitActive.pFFWalkArea.pListGridSet.Clear ();
		pPCPlayer.pUnitActive.pFFWalkArea.pGridField.ResetGrid();
		pPCPlayer.pUnitActive.ShowMovementRadius();
		pPCPlayer.pUnitActive.pFFWalkArea.pGridField.SetGrid();
	}

	void ShowSpecial(){
		pPCPlayer.pUnitActive.pFFWalkArea.pListGridSet.Clear ();
		pPCPlayer.pUnitActive.pFFWalkArea.pGridField.ResetGrid();
		pPCPlayer.pUnitActive.ShowSpecialRadius();
		pPCPlayer.pUnitActive.pFFWalkArea.pGridField.SetGrid();
	}

	bool AttackDone(){return false;}

	bool MoveDone(){return false;}

	bool SpecialDone(){return false;}

	void PlayerChange(){
		pIntTurnCount++;
		pPCPlayer.pBoolEndTurn = false;
	}
}
