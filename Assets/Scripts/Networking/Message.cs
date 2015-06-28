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

	public GameObject targetField { get { return _targetField; } }
	private GameObject _targetField;

	public Message (ActionType action, float damage, GameObject targetField) 
	{
		this._action = action;
		this._damage = damage;
		this._targetField = targetField;
		_involvedCharacters = new List<GameObject> ();
	}
}
