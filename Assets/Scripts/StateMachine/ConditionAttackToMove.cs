using UnityEngine;
using System.Collections;

public class ConditionAttackToMove : ICondition {

	public State pStateNextState;

	public ConditionAttackToMove(State next)
	{
		this.pStateNextState = next;
	}

	public bool Test()
	{
		return true;
	}
}
