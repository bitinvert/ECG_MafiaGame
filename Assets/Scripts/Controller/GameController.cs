using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {
	// Use this for initialization
	public Transform pTransSeeker;

	public Grid pGridGrid;
	public List<Unit> pListCharacters;
	private PlayerController mPCPlayer;
		
	void Start () {
		pListCharacters = new List<Unit>( Object.FindObjectsOfType(typeof(Unit)) as Unit[]);
		mPCPlayer = Object.FindObjectOfType (typeof(PlayerController)) as PlayerController;
		var mTKRecCharTap = new TKTapRecognizer();
		mTKRecCharTap.gestureRecognizedEvent += (r) =>
		{
			
			
			Vector3 mVec3TapPos = new Vector3(mTKRecCharTap.touchLocation().x,
			                                  mTKRecCharTap.touchLocation().y,
			                                  0f);
			RaycastHit mRHInfo = new RaycastHit();
			if(pTransSeeker == null && Physics.Raycast(Camera.main.ScreenPointToRay(mVec3TapPos), out mRHInfo))
			{
				if(mRHInfo.collider.gameObject.tag.Equals("Playable") && mRHInfo.collider.gameObject.GetComponent<Unit>().pFacFaction == mPCPlayer.pFactionFlag && 
				   mRHInfo.collider.gameObject.GetComponent<Unit>().pBoolDone == false)
				{
					pTransSeeker = mRHInfo.collider.gameObject.transform;
					mPCPlayer.pUnitTapped = mRHInfo.collider.gameObject.GetComponent<Unit>();
					if( mRHInfo.collider.gameObject.GetComponent<Unit>().pBoolMoveDone)
					{
						mPCPlayer.pBoolShowAttack = true;
					}
					else
					{
						mPCPlayer.pBoolShowMove = true;
					}
				}
			}
			else if(pTransSeeker != null && Physics.Raycast(Camera.main.ScreenPointToRay(mVec3TapPos), out mRHInfo))
			{
				int mIntUnitIndex = pListCharacters.IndexOf(pTransSeeker.GetComponent<Unit>());
				if(mRHInfo.collider.tag == "Field" && mPCPlayer.pBoolShowMove == true &&
				   pGridGrid.NodeFromWorldPosition(mRHInfo.collider.transform.position).pBoolReachable == true && 
				   pListCharacters[mIntUnitIndex].pGOTarget == null)
				{
					pListCharacters[mIntUnitIndex].pGOTarget = mRHInfo.collider.gameObject;
					
				}
				else if(mRHInfo.collider.tag == "Field" && mPCPlayer.pBoolShowMove == true &&
				        pGridGrid.NodeFromWorldPosition(mRHInfo.collider.transform.position).pBoolReachable == true)
				{
					GameObject mGOTemp =  mRHInfo.collider.gameObject;
					
					if(pListCharacters[mIntUnitIndex].pGOTarget.Equals(mGOTemp))
					{
						pListCharacters[mIntUnitIndex].pBoolDoubleTap = true;
					}
					else
					{
						pListCharacters[mIntUnitIndex].pGOTarget = mGOTemp;

					}
				}
				else if(mRHInfo.collider.gameObject.tag.Equals("Playable") && mRHInfo.collider.gameObject.GetComponent<Unit>().pFacFaction == mPCPlayer.pFactionFlag &&
				        mRHInfo.collider.gameObject.GetComponent<Unit>().pBoolDone == false)
				{
					pTransSeeker = mRHInfo.collider.gameObject.transform;
					mPCPlayer.pUnitTapped = mRHInfo.collider.gameObject.GetComponent<Unit>();
					if( mRHInfo.collider.gameObject.GetComponent<Unit>().pBoolMoveDone)
					{
						mPCPlayer.pBoolShowAttack = true;
					}
					else
					{
						mPCPlayer.pBoolShowMove = true;
					}
				}
				else if(mRHInfo.collider.gameObject.tag.Equals("Playable") && mRHInfo.collider.gameObject.GetComponent<Unit>().pFacFaction != mPCPlayer.pFactionFlag && mPCPlayer.pBoolShowAttack == true
				        && pListCharacters[mIntUnitIndex].pUnitEnemy == null)
				{
					pListCharacters[mIntUnitIndex].pUnitEnemy = mRHInfo.collider.gameObject.GetComponent<Unit>();
					mPCPlayer.pUnitTapped = mRHInfo.collider.gameObject.GetComponent<Unit>();
				}
				else if(mRHInfo.collider.gameObject.tag.Equals("Playable") && mRHInfo.collider.gameObject.GetComponent<Unit>().pFacFaction != mPCPlayer.pFactionFlag && mPCPlayer.pBoolShowAttack == true)
				{
					Unit mUnitTemp =  mRHInfo.collider.gameObject.GetComponent<Unit>();
					
					if(pListCharacters[mIntUnitIndex].pUnitEnemy.Equals(mUnitTemp))
					{
						pListCharacters[mIntUnitIndex].pBoolDoubleTap = true;
					}
					else
					{
						pListCharacters[mIntUnitIndex].pUnitEnemy = mUnitTemp;
						mPCPlayer.pUnitTapped = mRHInfo.collider.gameObject.GetComponent<Unit>();
					}
				}
			}
			
		};
		TouchKit.addGestureRecognizer(mTKRecCharTap);
	}

}
