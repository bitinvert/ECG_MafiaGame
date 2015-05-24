using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
	// Use this for initialization
	public GameObject mGOPlayableSprite;
	public GameObject mGOTileToMove;

	void Start () {
		var mTKRecCharTap = new TKTapRecognizer();
		mTKRecCharTap.gestureRecognizedEvent += (r) =>
		{


			Vector3 mVec3TapPos = new Vector3(mTKRecCharTap.touchLocation().x,
			                                  mTKRecCharTap.touchLocation().y,
			                                  0f);
			RaycastHit mRHInfo = new RaycastHit();
			if(!mGOPlayableSprite && Physics.Raycast(Camera.main.ScreenPointToRay(mVec3TapPos), out mRHInfo))
			{
				if(mRHInfo.collider.gameObject.tag == "Playable")
				{
					mGOPlayableSprite = mRHInfo.collider.gameObject;
				}
			}
			else if(mGOPlayableSprite && Physics.Raycast(Camera.main.ScreenPointToRay(mVec3TapPos), out mRHInfo))
			{
				if(mRHInfo.collider.name == "FieldFloor(Clone)" ||
				   mRHInfo.collider.name == "FieldCover(Clone)")
				{
					mGOTileToMove = mRHInfo.collider.gameObject;
				}
			}
		};
		TouchKit.addGestureRecognizer(mTKRecCharTap);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
