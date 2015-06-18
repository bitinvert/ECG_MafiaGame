 using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {
	// Use this for initialization
	public Transform pTransSeeker;

	public Grid pGridGrid;
	public List<Unit> pListCharacters;
		
	void Start () {
		var mTKRecCharTap = new TKTapRecognizer();
		mTKRecCharTap.gestureRecognizedEvent += (r) =>
		{
			
			
			Vector3 mVec3TapPos = new Vector3(mTKRecCharTap.touchLocation().x,
			                                  mTKRecCharTap.touchLocation().y,
			                                  0f);
			RaycastHit mRHInfo = new RaycastHit();
			if(pTransSeeker == null && Physics.Raycast(Camera.main.ScreenPointToRay(mVec3TapPos), out mRHInfo))
			{
				Debug.Log ("Bla");
				Debug.Log (mRHInfo.collider.gameObject.tag);
				if(mRHInfo.collider.gameObject.tag.Equals("Playable"))
				{
					
					pTransSeeker = mRHInfo.collider.gameObject.transform;
					Debug.Log("Blo");
				}
				
			}
			else if(pTransSeeker != null && Physics.Raycast(Camera.main.ScreenPointToRay(mVec3TapPos), out mRHInfo))
			{
				if(mRHInfo.collider.tag == "Field" && 
				   pGridGrid.NodeFromWorldPosition(mRHInfo.collider.transform.position).pBoolReachable == true && 
				   pListCharacters[pListCharacters.IndexOf(
					pTransSeeker.GetComponent<Unit>())].pGOTarget == null)
				{
					pListCharacters[pListCharacters.IndexOf(
						pTransSeeker.GetComponent<Unit>())].pGOTarget = mRHInfo.collider.gameObject;
					
				}
				else if(mRHInfo.collider.tag == "Field" &&
				        pGridGrid.NodeFromWorldPosition(mRHInfo.collider.transform.position).pBoolReachable == true)
				{
					GameObject mGOTemp =  mRHInfo.collider.gameObject;
					
					if(pListCharacters[pListCharacters.IndexOf(
						pTransSeeker.GetComponent<Unit>())].pGOTarget.Equals(mGOTemp))
					{
						pListCharacters[pListCharacters.IndexOf(
							pTransSeeker.GetComponent<Unit>())].pBoolDoubleTap = true;
					}
					else
					{
						pListCharacters[pListCharacters.IndexOf(
							pTransSeeker.GetComponent<Unit>())].pGOTarget = mGOTemp;
					}
				}
			}else
			{
				Debug.Log("Bli");
			}
			
		};
		TouchKit.addGestureRecognizer(mTKRecCharTap);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
