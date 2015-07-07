using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*Simple State Machine realised through methods no outer resources used*/
public class StateMachineUnitAction : MonoBehaviour {

	public PlayerController pPCPlayer;
	public int pIntTurnCount;


    private Client mClientPlayer;
	private bool mBoolMove = false;
	private bool mBoolAttack = false;
	private bool mBoolSpecial = false;

	private List<Vector3> mVec3BackList;

	// Use this for initialization
	void Start () {
		pPCPlayer = Object.FindObjectOfType(typeof(PlayerController)) as PlayerController;
        mClientPlayer = Object.FindObjectOfType(typeof(Client)) as Client;
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
		if(mBoolAttack )
		{
			if(AttackDone ())
			{
				Message msg = new Message(ActionType.ATTACK ,pPCPlayer.pUnitActive.Attack(pPCPlayer.pUnitActive.pUnitEnemy));
				msg.involvedCharacters.Add(pPCPlayer.pUnitActive.pStringName);
				msg.involvedCharacters.Add(pPCPlayer.pUnitActive.pUnitEnemy.name);
				mClientPlayer.SavePlayerMove(msg);
				pPCPlayer.pUnitActive.ResetValues();
				Debug.Log ("State: Attack Done");
				mBoolAttack = false;
				pPCPlayer.pUnitActive.pBoolDone = true;
			}
		}
		if(mBoolMove)
		{
			if(MoveDone () && pPCPlayer.pUnitActive.pBoolMoveDone == false)
			{
				Vector3 test = new Vector3(pPCPlayer.pUnitActive.pGOTarget.transform.position.x, 0f, pPCPlayer.pUnitActive.pGOTarget.transform.position.z);
				
	
				pPCPlayer.pUnitActive.move();
				if(pPCPlayer.pUnitActive.transform.position - pPCPlayer.pUnitActive.mVec3Offset == test)
				{
					mBoolMove = false;
					mVec3BackList = new List<Vector3>();
					mVec3BackList.AddRange (pPCPlayer.pUnitActive.pAStarPathfinding.pListPath);
					pPCPlayer.pUnitActive.ResetValues();
					Message msg = new Message(ActionType.MOVEMENT, 0);
					msg.involvedCharacters.Add (pPCPlayer.pUnitActive.pStringName);
					msg.targetField = mVec3BackList[0];
					mClientPlayer.SavePlayerMove(msg);

					Debug.Log ("State: Move Done");

					pPCPlayer.pUnitActive.pBoolMoveDone = true;
					pPCPlayer.pBoolShowAttack = true;
					pPCPlayer.pBoolShowMove = false;
				}
				//TODO Setting values for attacking
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
			pPCPlayer.pUnitActive.pAStarPathfinding.FindPath(pPCPlayer.pUnitActive.gameObject.transform.position , pPCPlayer.pUnitActive.pGOTarget.transform.position,
			                                                 pPCPlayer.pUnitActive.mVec3Offset);
			return false;
		}
		else if(pPCPlayer.pUnitActive.pGOTarget != null && pPCPlayer.pUnitActive.pBoolDoubleTap)
		{
			mBoolMove= false;
			return true;
		}
		return false;
	}

	bool SpecialDone(){
	if(pPCPlayer.pUnitActive.pOIObjective != null && pPCPlayer.pUnitActive.pBoolDoubleTap)
	{
		Debug.Log ("Test");

		ObjectiveInterface mObjective = pPCPlayer.pUnitActive.pOIObjective;
		if(mObjective!=null){
			mObjective.InteractWithObjective(pPCPlayer.pUnitActive);
			mObjective.ShowObjectiveStatus();
			return true;
		}
		return false;
	}

	return false;
}

	void PlayerChange(){
		pIntTurnCount++;
        mClientPlayer.playerChange();
		pPCPlayer.pBoolEndTurn = false;
		//TODO Switching player Jane?
	}
}
