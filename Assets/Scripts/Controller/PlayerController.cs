using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {
	
	public GameController pGCController;	
	
	public string pStringPlayerName;
	public List<Unit> pListUnits;
	public Faction pFactionFlag;
	public Unit pUnitActive;

	public bool pBoolShowMove;
	public bool pBoolShowAttack;
	public bool pBoolShowSpecial;
	public bool pBoolEndTurn;
	
	void Start () {
		pListUnits = new List<Unit>( Object.FindObjectsOfType(typeof(Unit)) as Unit[]);
	}


	public enum Faction
	{
		Mafia, Police
	}
	
}