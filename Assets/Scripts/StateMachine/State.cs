using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class State {
	//A State has a name and a Set of condition
	//The Conditions act like a linked list, as they have the next State
	//it only changes the state, if the test return true. The classes have been
	//created but not yet implemented
	public string pStringStateName;

	public List<ICondition> pListCondition;



	public State(string name)
	{
		this.pStringStateName = name;
	}

	//Adds a new condition to the condition list
	public void Add(ICondition con)
	{
		pListCondition.Add (con);
	}
	
}
