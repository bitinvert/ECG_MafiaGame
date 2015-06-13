using UnityEngine;
using System.Collections;

public class StateMachineBaseTurn : MonoBehaviour {

	public int pIntTurnCount;
	public State pStateTurnCycle;

	// Use this for initialization
	void Start () {
		this.pIntTurnCount = 0;
		//Start state;
		this.pStateTurnCycle = new State("Idle");

		//Conditions and following states
		State mStateMove = new State("Move");
		State mStateAttack = new State("Attack");

		ConditionIdleToMove mCIdleToMove = new ConditionIdleToMove(mStateMove);
		this.pStateTurnCycle.Add (mCIdleToMove);

		ConditionMoveToAttack mCMoveToAttack = new ConditionMoveToAttack(mStateAttack);
		mStateMove.Add(mCMoveToAttack);

		ConditionAttackToMove mCAttackToMove = new ConditionAttackToMove(mStateMove);
		mStateAttack.Add (mCAttackToMove);

		ConditionAttackToIdle mCAttackToIdle = new ConditionAttackToIdle(this.pStateTurnCycle);
		mStateAttack.Add (mCAttackToIdle);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
