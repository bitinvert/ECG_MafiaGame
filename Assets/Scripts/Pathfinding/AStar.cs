using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AStar : MonoBehaviour {

	public Grid mGridField;

	public Vector3[] pListPath;

	// Use this for initialization
	void Awake () {
		mGridField = GetComponent<Grid>();
	}



	public void FindPath(Vector3 startPos, Vector3 targetPos)
	{
		Node mNodeStart = mGridField.NodeFromWorldPosition(startPos);
		Node mNodeTarget = mGridField.NodeFromWorldPosition(targetPos);

		List<Node> mListOpenSet = new List<Node>();
		HashSet<Node> mHashClosedSet = new HashSet<Node>();
		mListOpenSet.Add (mNodeStart);
		while(mListOpenSet.Count > 0)
		{
			//Current with lowest FCost
			Node mNodeCurrent = mListOpenSet[0];

			/**
			 * Check if element i from the OpenSet has a lower FCost than the current node
			 * or if it has an equal FCost and the HCost is lower than that of the current node.
			 * if so set the current node the element i of the openlist
			 **/
			for (int i = 1; i < mListOpenSet.Count; i ++) {
				if (mListOpenSet[i].pIntFCost < mNodeCurrent.pIntFCost ||
				    mListOpenSet[i].pIntFCost == mNodeCurrent.pIntFCost && 
				    mListOpenSet[i].pIntHCost < mNodeCurrent.pIntHCost) 
				{
					mNodeCurrent = mListOpenSet[i];
				}
			}

			//Remove current from OpenSet and add it to th ClosedSet
			mListOpenSet.Remove (mNodeCurrent);
			mHashClosedSet.Add (mNodeCurrent);

			//If the current node is the target node quit
			if(mNodeCurrent == mNodeTarget)
			{
				//New Version has to be added
				RetracePath(mNodeStart, mNodeTarget	);
				return;
			}

			//Check then the neighbours
			foreach(Node neighbour in mGridField.GetNeighbours(mNodeCurrent))
			{
				//if the neighbour is not traversable or in the closed set check the next
				if(!neighbour.pBoolWalkable || mHashClosedSet.Contains(neighbour))
				{
					continue;
				}
				//else if the new path is shorter OR if the neighbour is in the closed set
				//set the values of the neighbours and if it is not in the open set add it to it
				int mIntNewMoveCost = mNodeCurrent.pIntGCost + GetDistance(mNodeCurrent, neighbour);
				if(mIntNewMoveCost < neighbour.pIntGCost || !mListOpenSet.Contains(neighbour))
				{
					neighbour.pIntGCost = mIntNewMoveCost;
					neighbour.pIntHCost = GetDistance(neighbour, mNodeTarget);
					neighbour.pNodeParent = mNodeCurrent;
					
					if (!mListOpenSet.Contains(neighbour))
						mListOpenSet.Add(neighbour);
				}
			}


		}
	}

	//Retrace the path backward from the endnode to the startnode
	//go through all parents to the start node and then reverse it
	void RetracePath(Node startNode, Node endNode)
	{
		List<Vector3> mListPath = new List<Vector3>();
		Node mNodeCurrent = endNode;

		while(mNodeCurrent != startNode)
		{
			//Offset by 0.25
			mListPath.Add (mNodeCurrent.pVec3WorldPos + new Vector3(0f, .25f, 0f));
			mNodeCurrent = mNodeCurrent.pNodeParent;
		}
		mListPath.Reverse();
		pListPath = mListPath.ToArray();

	}


	//Calculate the distance from node A to node B
	// 14 and 10 are magic numbers
	int GetDistance(Node nodeA, Node nodeB) {
		int mIntDstX = Mathf.Abs(nodeA.pIntX - nodeB.pIntX);
		int mIntDstY = Mathf.Abs(nodeA.pIntY - nodeB.pIntY);
		
		if (mIntDstX > mIntDstY)
			return 14*mIntDstY + 10* (mIntDstX-mIntDstY);
		return 14*mIntDstX + 10 * (mIntDstY-mIntDstX);
	}

}
