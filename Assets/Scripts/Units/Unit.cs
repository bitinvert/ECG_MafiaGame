﻿using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour {
	
	public string pStringName;
	public int pIntAttack;
	public int pIntDefense;
	public int pIntHealth;
	public int pIntWalkDistance;
	
	public bool pBoolEnemy;
	
	public float pFloatSpeed = 2;
	//inventory is missing
	
	public AStar pAStarPathfinding;
	public FloodFill pFFWalkArea;
	
	public GameController pGCController;
	
	int mIntTargetIndex;
	
	public GameObject pGOTarget;
	
	public bool pBoolDoubleTap;
	private bool mBoolPathShown;
	
	// Update is called once per frame
	void Update () {
		if(!pBoolEnemy && pGCController.pTransSeeker != null && pGOTarget != null && pGCController.pTransSeeker.position.Equals(pGOTarget.transform.position + new Vector3(0f,.25f,0f)))
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
			FollowPath();
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
	
	void FollowPath()
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
	
}
