 using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {
	// Use this for initialization
	public Transform pTransSeeker;

	public Grid pGridGrid;
	public List<Unit> pListCharacters;
		
	void Start () {
		pListCharacters = new List<Unit>( Object.FindObjectsOfType(typeof(Unit)) as Unit[]);
		var mTKRecCharTap = new TKTapRecognizer();
		mTKRecCharTap.gestureRecognizedEvent += (r) =>
		{
			
			
			Vector3 mVec3TapPos = new Vector3(mTKRecCharTap.touchLocation().x,
			                                  mTKRecCharTap.touchLocation().y,
			                                  0f);
			RaycastHit mRHInfo = new RaycastHit();
			if(pTransSeeker == null && Physics.Raycast(Camera.main.ScreenPointToRay(mVec3TapPos), out mRHInfo))
			{
				if(mRHInfo.collider.gameObject.tag.Equals("Playable"))
				{
					pTransSeeker = mRHInfo.collider.gameObject.transform;
				}
			}
			else if(pTransSeeker != null && Physics.Raycast(Camera.main.ScreenPointToRay(mVec3TapPos), out mRHInfo))
			{
				int mIntUnitIndex = pListCharacters.IndexOf(pTransSeeker.GetComponent<Unit>());
				if(mRHInfo.collider.tag == "Field" && 
				   pGridGrid.NodeFromWorldPosition(mRHInfo.collider.transform.position).pBoolReachable == true && 
				   pListCharacters[mIntUnitIndex].pGOTarget == null)
				{
					pListCharacters[mIntUnitIndex].pGOTarget = mRHInfo.collider.gameObject;
					
				}
				else if(mRHInfo.collider.tag == "Field" &&
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
				else if(mRHInfo.collider.gameObject.tag.Equals("Playable"))
				{
					pTransSeeker = mRHInfo.collider.gameObject.transform;
				}
			}
			
		};
		TouchKit.addGestureRecognizer(mTKRecCharTap);
	}

}
