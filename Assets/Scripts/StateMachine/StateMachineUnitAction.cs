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
			mBoolMove = false;
			mBoolSpecial = false;
		}
		else if(pPCPlayer.pBoolShowMove == true && SelectedChar ())
		{
			mBoolMove = ShowMove ();
			mBoolAttack = false;
			mBoolSpecial = false;
		}
		else if(pPCPlayer.pBoolShowSpecial == true && SelectedChar ())
		{
			mBoolSpecial = ShowSpecial ();
			mBoolAttack = false;
			mBoolMove = false;
		}
		if(mBoolAttack)
		{
			if(AttackDone ())
			{
				pPCPlayer.pUnitActive.Attack(pPCPlayer.pUnitActive.pUnitEnemy);
				pPCPlayer.pUnitActive.ResetValues();
				Debug.Log ("State: Attack Done");
				mBoolAttack = false;
			}
		}
		if(mBoolMove)
		{
			if(MoveDone ())
			{
				pPCPlayer.pUnitActive.move();
				if(pPCPlayer.pUnitActive.transform.position - pPCPlayer.pUnitActive.mVec3Offset == pPCPlayer.pUnitActive.pGOTarget.transform.position)
				{
					pPCPlayer.pUnitActive.ResetValues();
					Debug.Log ("State: Move Done");
					mBoolMove = false;
				}
			}
		}
		if(mBoolSpecial)
		{
			if(SpecialDone ())
			{
				//TODO Special cases for each type of unit
				Debug.Log ("State: Special Done");
				mBoolSpecial = false;
			}
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

	bool AttackDone(){return pPCPlayer.pUnitActive.pUnitEnemy != null && pPCPlayer.pUnitActive.pBoolDoubleTap == true;}

	bool MoveDone(){
		if(pPCPlayer.pUnitActive.pGOTarget != null && !pPCPlayer.pUnitActive.pBoolDoubleTap)
		{
			pPCPlayer.pUnitActive.pAStarPathfinding.FindPath(pPCPlayer.pUnitActive.gameObject.transform.position-pPCPlayer.pUnitActive.mVec3Offset,
			                                                 pPCPlayer.pUnitActive.pGOTarget.transform.position);
			return false;
		}
		else if(pPCPlayer.pUnitActive.pGOTarget != null && pPCPlayer.pUnitActive.pBoolDoubleTap)
		{
			return true;
		}
		return false;
	}

	bool SpecialDone(){return false;}

	void PlayerChange(){
		pIntTurnCount++;
		pPCPlayer.pBoolEndTurn = false;
		//TODO Switching player Jane?
	}
}
