using UnityEngine;
using System.Collections;

public class ConditionMoveToAttack : ICondition{
	
	public State pStateNextState;

	public ConditionMoveToAttack(State next)
	{
		this.pStateNextState = next;
	}

	public bool Test()
	{
		return true;
	}
}
