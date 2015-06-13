﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {
	
	public GameController pGCController;	
	
	public string pStringPlayerName;
	public List<Unit> pListUnits;
	public Faction pFactionFlag;
	public Unit pUnitActive; 
	
	
	public enum Faction
	{
		Mafia, Police
	}
	
}