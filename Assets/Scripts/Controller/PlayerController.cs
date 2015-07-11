using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {
	
	public GameController pGCController;	
	
	public string pStringPlayerName;
	public List<Unit> pListUnits;
	public Fraction pFactionFlag;
	public Unit pUnitActive;

	public Unit pUnitTapped;

	public bool pBoolShowMove;
	public bool pBoolShowAttack;
	public bool pBoolShowSpecial;
	public bool pBoolEndTurn;

	//Für Nadja: wenn der button zum festnehmen gedrückt wurde soll dieser boolean gesetzt werden
	public bool pBoolShackle;

	private Client mClinetFaction;

	void Start () {
		mClinetFaction = Object.FindObjectOfType(typeof(Client)) as Client;
		pListUnits = new List<Unit>( Object.FindObjectsOfType(typeof(Unit)) as Unit[]);
		pBoolShowMove = true;
		pBoolShowAttack = false;
		pBoolShowSpecial = false;
		pBoolEndTurn = false;
		pBoolShackle = false;
		pFactionFlag = mClinetFaction.user.fraction;
	}


	void Update () {
		if(pGCController.pTransSeeker != null){
			int mIntUnitIndex = pGCController.pListCharacters.IndexOf(pGCController.pTransSeeker.GetComponent<Unit>());
			pUnitActive = pGCController.pListCharacters[mIntUnitIndex];
		}
	}


	public void setShowMove(bool b) {
		this.pBoolShowMove = b;
	}

	public void setShowAttack(bool b) {
		this.pBoolShowAttack = b;
	}

	public void setShowSpecial(bool b) {
		this.pBoolShowSpecial = b;
	}

	public void setEndTurn(bool b) {
		this.pBoolEndTurn = b;
	}
}