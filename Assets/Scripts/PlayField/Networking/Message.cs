using UnityEngine;
using System.Collections;
using System;

public class Message{

	string mStringTimeStamp;
	string mStringMap;
	string mStringPlayName;
	string mStringEnemyName;
	ActionType mATActionPerformed;
	GameObject[] mGOCharactersInvolved;
	float[] mFloatDamadge;
	Vector3[] mVec3Movement;

	public string pack()
	{
		return "NULL";
	}

	public void unpack(string message)
	{

	}

	enum ActionType
	{
		Movement,
		Attack,
		Other //More are coming
	}
}
