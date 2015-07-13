using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*Simple State Machine realised through methods no outer resources used*/
public class StateMachineUnitAction : MonoBehaviour {
	
	public PlayerController pPCPlayer;
	public int pIntTurnCount;

	public AudioSource audio1;
	public AudioSource audio2;
	public AudioSource audio3;
	
	private Client mClientPlayer;
	private bool mBoolMove = false;
	private bool mBoolAttack = false;
	private bool mBoolSpecial = false;
	
	private List<Vector3> mVec3BackList;

	private bool trigger;
	/* used for video run away
	//-----
	private bool runAway;
	private int count = 0;
	public GameObject gangCar;
	//----
	*/
	
	GameObject sWord;
	// Use this for initialization
	void Start () {
		pPCPlayer = Object.FindObjectOfType(typeof(PlayerController)) as PlayerController;
		mClientPlayer = Object.FindObjectOfType(typeof(Client)) as Client;
		pIntTurnCount = 0;
	}
	
	// Update is called once per frame
	void Update () {
		/* used for video run away
		//------
		if (runAway) {

			gangCar.transform.position += new Vector3(1, 0, 0)* Time.deltaTime*3;
		}
		//-----
		*/

		/*The States themselves.
		  Here the certain values will be set*/
		if (pPCPlayer.pBoolShowAttack == true && SelectedChar () && !pPCPlayer.pBoolShowMove && !pPCPlayer.pBoolShowSpecial) {
			mBoolAttack = ShowAttack ();
			mBoolMove = false;
			mBoolSpecial = false;
		} else if (pPCPlayer.pBoolShowMove == true && SelectedChar () && !pPCPlayer.pBoolShowAttack && !pPCPlayer.pBoolShowSpecial) {
			mBoolMove = ShowMove ();
			mBoolAttack = false;
			mBoolSpecial = false;
		} else if (pPCPlayer.pBoolShowSpecial == true && SelectedChar () && !pPCPlayer.pBoolShowMove && !pPCPlayer.pBoolShowAttack) {
			mBoolSpecial = ShowSpecial ();
			mBoolAttack = false;
			mBoolMove = false;
		}

		if(mBoolAttack )
		{
			if(AttackDone ())
			{
				int attack = pPCPlayer.pUnitActive.Attack(pPCPlayer.pUnitActive.pUnitEnemy);
				audio1.Play ();
				pPCPlayer.pUnitActive.EnableSoundWord();
				sWord = pPCPlayer.pUnitActive.soundWord;
				StartCoroutine("DelaySound");

				Message msg = new Message(ActionType.ATTACK ,attack);
				msg.involvedCharacters.Add(pPCPlayer.pUnitActive.pStringName);
				msg.involvedCharacters.Add(pPCPlayer.pUnitActive.pUnitEnemy.pStringName);
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
				audio2.Play ();
				if(pPCPlayer.pUnitActive.transform.position - pPCPlayer.pUnitActive.mVec3Offset == test)
				{

					//----
					pPCPlayer.pBoolShowAttack = true;
					//----
					mBoolMove = false;
					mVec3BackList = new List<Vector3>();
					mVec3BackList.AddRange (pPCPlayer.pUnitActive.pAStarPathfinding.pListPath);
					pPCPlayer.pUnitActive.ResetMoveVals();
					Message msg = new Message(ActionType.MOVEMENT, 0);
					msg.involvedCharacters.Add (pPCPlayer.pUnitActive.pStringName);
					//msg.targetField = mVec3BackList[0];
					msg.targetField = pPCPlayer.pUnitActive.transform.position;
					mClientPlayer.SavePlayerMove(msg);
					
					Debug.Log ("State: Move Done");
					pPCPlayer.pBoolShowMove = false;
					pPCPlayer.pUnitActive.pBoolMoveDone = true;
					/* used for video run away
					//-----
					pPCPlayer.pUnitActive.pFFWalkArea.pListGridSet.Clear ();
					pPCPlayer.pUnitActive.pFFWalkArea.pGridField.ResetGrid();
					pPCPlayer.pListUnits.Remove(pPCPlayer.pUnitActive);

					Destroy (pPCPlayer.pUnitActive.gameObject);
					count += 1;

					if (count == 3){
						Debug.Log ("runn away");
						runAway = true;
					}
					//-----
					*/
				}
				//TODO Setting values for attacking
			}
		}
		if(mBoolSpecial)
		{
			if(SpecialDone ())
			{
				if(pPCPlayer.pUnitActive.pOIObjective != null){
					//TODO Special cases for each type of unit
					Message msg = new Message(ActionType.BREAK_SAFE, 0);
					msg.involvedCharacters.Add (pPCPlayer.pUnitActive.pStringName);
					msg.involvedCharacters.Add (pPCPlayer.pUnitActive.pOIObjective.identifier);
					mClientPlayer.SavePlayerMove(msg);
					Debug.Log ("State: Special Done");
					pPCPlayer.pUnitActive.ResetValues();
					pPCPlayer.pUnitActive.pFFWalkArea.pListGridSet.Clear ();
					pPCPlayer.pUnitActive.pFFWalkArea.pGridField.ResetGrid();
					mBoolSpecial = false;
					pPCPlayer.pBoolShowSpecial = false;
					pPCPlayer.pUnitActive.pBoolDone = true;
				}
			}
		}
		if(pPCPlayer.pBoolEndTurn == true)
		{
			PlayerChange();
		}
	}
	/*Checking the conditions and extra features*/
	bool SelectedChar(){
		return(pPCPlayer.pUnitActive != null && pPCPlayer.pUnitActive.pBoolDoubleTap == false);
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
			//mBoolMove= false;
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
				pPCPlayer.pUnitActive.bag.SetActive(true);
				pPCPlayer.pUnitActive.pOIObjective.gameObject.SetActive (false);
				return true;
			}
			return false;
		}
		
		return false;
	}
	
	void PlayerChange(){
		pIntTurnCount++;
		this.ResetStateVal();

		foreach (Unit unit in pPCPlayer.pListUnits) {
			unit.pBoolMoveDone = false;
			unit.pBoolDone = false;
			unit.pBoolDoubleTap = false;
		}
		if(pPCPlayer.pUnitActive != null) {
			pPCPlayer.pUnitActive.pFFWalkArea.pListGridSet.Clear ();
			pPCPlayer.pUnitActive.pFFWalkArea.pGridField.ResetGrid();
		}
		pPCPlayer.pUnitActive = null;
		pPCPlayer.pUnitTapped = null;

		pPCPlayer.pBoolEndTurn = false;
		mClientPlayer.playerChange();
		
	}
	
	void ResetStateVal()
	{
		pPCPlayer.pBoolShowAttack = false;
		pPCPlayer.pBoolShowMove = false;
		pPCPlayer.pBoolShowSpecial =false;
		mBoolMove = false;
		mBoolAttack = false;
		mBoolSpecial = false;
	}

	public IEnumerator DelaySound() {
		trigger = false;
		yield return new WaitForSeconds(1f); // waits 3 seconds
		sWord.SetActive(false);
		audio3.Play ();	
		trigger = true; // will make the update method pick up 
	}
}
