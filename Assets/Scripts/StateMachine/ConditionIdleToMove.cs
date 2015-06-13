using UnityEngine;
using System.Collections;

public class ConditionIdleToMove : ICondition {

	public State pStateNextState;

	public GameController pGCController;

	public ConditionIdleToMove(State next)
	{
		this.pStateNextState = next;
	}


	//Implementation missing
	public bool Test()
	{
		return true;
	}

}
