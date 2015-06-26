using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*Simple State Machine realised through methods no outer resources used*/
public class StateMachineUnitAction : MonoBehaviour {

	public PlayerController pPCPlayer;
	public int pIntTurnCount;

	private bool mBoolMove = false;
	private bool mBoolAttack = false;
	private bool mBoolSpecial = false;

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
			mBoolAttack = ShowAttack();
		}
		else if(pPCPlayer.pBoolShowMove == true && SelectedChar ())
		{
			mBoolMove = ShowMove ();
		}
		else if(pPCPlayer.pBoolShowSpecial == true && SelectedChar ())
		{
			mBoolSpecial = ShowSpecial ();
		}
		if(AttackDone ())
		{
			Debug.Log ("State: Attack Done");
		}
		/*if(mBoolpPCPlayer.pUnitActive.pGOTarget != null && pPCPlayer.pUnitActive.pBoolDoubleTap)
		{
			//pPCPlayer.pUnitActive.
		}*/
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

	bool ShowAttack()
	{
		pPCPlayer.pUnitActive.pFFWalkArea.pListGridSet.Clear ();
		pPCPlayer.pUnitActive.pFFWalkArea.pGridField.ResetGrid();
		pPCPlayer.pUnitActive.ShowAttackRadius();
		pPCPlayer.pUnitActive.pFFWalkArea.pGridField.SetGrid();
		return true;
	}

	bool ShowMove(){
		pPCPlayer.pUnitActive.pFFWalkArea.pListGridSet.Clear ();
		pPCPlayer.pUnitActive.pFFWalkArea.pGridField.ResetGrid();
		pPCPlayer.pUnitActive.ShowMovementRadius();
		pPCPlayer.pUnitActive.pFFWalkArea.pGridField.SetGrid();
		return true;
	}

	bool ShowSpecial(){
		pPCPlayer.pUnitActive.pFFWalkArea.pListGridSet.Clear ();
		pPCPlayer.pUnitActive.pFFWalkArea.pGridField.ResetGrid();
		pPCPlayer.pUnitActive.ShowSpecialRadius();
		pPCPlayer.pUnitActive.pFFWalkArea.pGridField.SetGrid();
		return true;
	}

	bool AttackDone(){return false;}

	bool MoveDone(){return false;}

	bool SpecialDone(){return false;}

	void PlayerChange(){
		pIntTurnCount++;
		pPCPlayer.pBoolEndTurn = false;
	}
}
