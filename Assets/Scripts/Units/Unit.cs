﻿using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour {
	
	public string pStringName;
	public int pIntAttack;
	public int pIntDefense;
	public int pIntHealth;
	public int pIntWalkDistance;
	//we have no attack distance at this point, because the dis. should change with particular weapons. So we are using a getAttackDis method
	// to wrap this fact. Right now it is returning a fix number

	//for brendans pathfinding. brace yourselves changes are coming
	public bool pBoolEnemy;
	
	public float pFloatSpeed = 2;
	//inventory is missing
	
	public AStar pAStarPathfinding;
	public FloodFill pFFWalkArea;

	public GameController pGCController;
	
	private int mIntTargetIndex;
	
	public GameObject pGOTarget;
	
	public bool pBoolDoubleTap;
	private bool mBoolPathShown;

	//Magic number for offset
	public Vector3 mVec3Offset;
	
	public Unit pUnitEnemy;

	void Start () {
		mVec3Offset = new Vector3(0f,this.transform.position.y,0f);
	}

	// Update is called once per frame
	void Update () {
		if(!pBoolEnemy && pGCController.pTransSeeker != null && pGOTarget != null && pGCController.pTransSeeker.position.Equals(pGOTarget.transform.position + mVec3Offset))
		{
			pGOTarget = null;
			pGCController.pTransSeeker = null;
			pBoolDoubleTap = false;
			mBoolPathShown = false;
			mIntTargetIndex = 0;
			
			pAStarPathfinding.pListPath = new Vector3[0];
		}
		if(!pBoolEnemy && pGOTarget != null && !pBoolDoubleTap)
		{
			mBoolPathShown = ShowPath();
		}
		if(pBoolDoubleTap)
		{
			mBoolPathShown = false;
			move();

		}
		
	}

	bool ShowPath()
	{
		//Placeholder
		pAStarPathfinding.FindPath(this.transform.position, pGOTarget.transform.position);
		if(pAStarPathfinding.pListPath.Length > 0)
		{
			return true;
		}
		return false;
	}

	public void OnDrawGizmos() {
		if (pAStarPathfinding != null) {
			for (int i = mIntTargetIndex; i < pAStarPathfinding.pListPath.Length; i ++) {
				Gizmos.color = Color.black;
				Gizmos.DrawCube(pAStarPathfinding.pListPath[i], Vector3.one/3);
				
				if (i == mIntTargetIndex) {
					Gizmos.DrawLine(transform.position, pAStarPathfinding.pListPath[i]);
				}
				else {
					Gizmos.DrawLine(pAStarPathfinding.pListPath[i-1],pAStarPathfinding.pListPath[i]);
				}
			}
		}
	}
	//Svens implementationZZZzzz
	public void ShowAttackRadius(){
		Node mNodeUnit = pFFWalkArea.pGridField.NodeFromWorldPosition (this.gameObject.transform.position-mVec3Offset);
		pFFWalkArea.FindPath (mNodeUnit, 0,GetAttackDistance());
	}
	
	private int GetAttackDistance(){
		return 2;
	}
	public int Attack (Unit mUnitEnemy){
		int mIntDamage = CalculateDamage (mUnitEnemy);
		mUnitEnemy.pIntHealth = mUnitEnemy.pIntHealth - mIntDamage;
		return mIntDamage;
	}
	public void Die (){
		Destroy (this.gameObject);
	}
	
	private int CalculateDamage(Unit mUnitEnemy){
		int mIntDamage = mUnitEnemy.pIntDefense - this.pIntAttack;
		if (mIntDamage < 1) {
			mIntDamage = 1;
		}
		return mIntDamage;
	}

	//Brendans implementation
	//Movement Stuffs
	public void ShowMovementRadius(){
		Node mNodeUnit = pFFWalkArea.pGridField.NodeFromWorldPosition (this.gameObject.transform.position-mVec3Offset);
		pFFWalkArea.FindPath (mNodeUnit, 0,pIntWalkDistance);
	}

	public void move()
	{
		Vector3 mVec3Current = pAStarPathfinding.pListPath[mIntTargetIndex];
		
		while(true)
		{
			if (transform.position == mVec3Current) {
				mIntTargetIndex++;
				if (mIntTargetIndex >= pAStarPathfinding.pListPath.Length) {
					
					break;
				}
				mVec3Current = pAStarPathfinding.pListPath[mIntTargetIndex];
			}
			
			transform.position = Vector3.MoveTowards(transform.position,mVec3Current,pFloatSpeed * Time.deltaTime);
			return;
		}
	}

	//Special Stuffs
	//Returns constant because it needs to be implemented in specialised units
	private int GetSpecialDistance()
	{
		return 1;
	}

	public void ShowSpecialRadius(){
		Node mNodeUnit = pFFWalkArea.pGridField.NodeFromWorldPosition (this.gameObject.transform.position-mVec3Offset);
		pFFWalkArea.FindPath (mNodeUnit, 0, GetSpecialDistance());
	}

	public void UseSpecial(Unit mUnitOther){
		//needs to be implemented in specialized units, only debug in standard unit
		Debug.Log("Special Done!!");
	}

}
