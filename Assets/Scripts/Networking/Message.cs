using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Message{

	//string mStringTimeStamp; --
	//string mStringMap; --not needed because there's only one map
	//string mStringPlayName; --not needed because client knows its player
	//string mStringEnemyName; --not needed because client knows its opponent
	public ActionType action { get { return _action; } set { _action = value; } }
	private ActionType _action ;

	public List <GameObject> involvedCharacters { get { return _involvedCharacters; } }
	private List <GameObject> _involvedCharacters;

	public float damage { get { return _damage; } set { _damage = value; } }
	private float _damage;

	public Vector3[] movement { get { return _movement; } }
	private Vector3[] _movement;

	public Message (ActionType action, float damage) 
	{
		this._action = action;
		this._damage = damage;
		_involvedCharacters = new List<GameObject> ();
		_movement = new Vector3[3];
	}
}
