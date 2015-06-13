using UnityEngine;
using System.Collections;

public class ConditionAttackToIdle : ICondition {

	public State pStateNextState;

	public ConditionAttackToIdle(State next)
	{
		this.pStateNextState = next;
	}

	public bool Test()
	{
		return true;
	}
}
