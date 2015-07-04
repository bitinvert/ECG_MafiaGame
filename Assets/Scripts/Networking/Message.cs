using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
/// <summary>
/// Message is used to pack information for a client about a player's move.
/// </summary>
public class Message{
	
	public ActionType action { get { return _action; } set { _action = value; } }
	private ActionType _action ;

	public List <string> involvedCharacters { get { return _involvedCharacters; } }
	private List <string> _involvedCharacters;

	public float damage { get { return _damage; } set { _damage = value; } }
	private float _damage;

	public Vector3 targetField { get { return _targetField; } set { _targetField = value; } }
	private Vector3 _targetField;

	public Message (ActionType action, float damage) 
	{
		this._action = action;
		this._damage = damage;
		_involvedCharacters = new List<String> ();
	}
}
