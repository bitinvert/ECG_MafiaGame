using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Grid : MonoBehaviour {

	//Layermask for all unwalkable nodes
	public LayerMask pLayerUnwalkableMask;

	//X and Z size of the grid
	public Vector2 pVec3GridWorldSize;

	//Size of a square on the grid
	public float pFloatGridRadius;
	private float mFloatGridDiameter;

	//Maximum X and Y size of the Array
	public int mIntSizeX;
	public int mIntSizeY;

	//Array of all nodes
	public Node[,] mNodeGrid;

	AStar path;
	public FloodFill fill;

	public bool isGridCreated = false;

	public LevelController lc;
	// Use this for initialization
	void Start ()
	{
		mFloatGridDiameter = pFloatGridRadius * 2;
		mIntSizeX = Mathf.RoundToInt(pVec3GridWorldSize.x / mFloatGridDiameter);
		mIntSizeY = Mathf.RoundToInt(pVec3GridWorldSize.y / mFloatGridDiameter);

		CreateGrid();
		isGridCreated = true;

	}

/*	void Update()
	{
		fill.pListGridSet.Clear ();
		if(fill.pGCController.pTransSeeker != null)
		{
			fill.FindPath(NodeFromWorldPosition(fill.pGCController.pTransSeeker.gameObject.transform.position - new Vector3(0, 0.25f, 0)), 0);
			SetGrid ();
		}
		else
		{
			ResetGrid();
		}
	}*/
	
	public void SetGrid()
	{
		foreach(Node n in mNodeGrid)
		{
			if(!fill.pListGridSet.Contains(n))
			{
				n.pBoolReachable= false;
			}
			else
			{
				n.pBoolReachable= true;
			}
		}
	}
	
	public void ResetGrid()
	{
		foreach(Node n in mNodeGrid)
		{
			n.pBoolReachable = false;
		}
	}

	//Method to set the values for the grid	
	void CreateGrid()
	{
		mNodeGrid = new Node[mIntSizeX,mIntSizeY];
		//Calculate the bottom-left corner of the grid
		Vector3 mVec3BotLeft = transform.position - Vector3.right * pVec3GridWorldSize.x/2 - Vector3.forward * pVec3GridWorldSize.y/2;

		for(int x = 0; x < mIntSizeX; x++)
		{
			for(int y = 0; y < mIntSizeY; y++)
			{
				//Calculate the worldposition of a node
				Vector3 mVec3WorldPoint = mVec3BotLeft 
					+ Vector3.right * (x * mFloatGridDiameter + pFloatGridRadius) 
						+ Vector3.forward * (y * mFloatGridDiameter + pFloatGridRadius);
				//Check is a node is walkable
				// 0.01f have to be subtracted so only the right nodes are selected to be unwalkable
				bool mBoolWalkable = !(Physics.CheckSphere(mVec3WorldPoint, pFloatGridRadius - 0.02f, pLayerUnwalkableMask));
				mNodeGrid[x,y] = new Node(mBoolWalkable, mVec3WorldPoint, x, y);
			}
		}
	}

	public List<Node> GetNeighbours(Node node)
	{
		List <Node> mListNeighbour = new List<Node>();

		for(int x = -1; x <= 1; x++)
		{
			for(int y = -1; y <= 1; y++)
			{
				if(x==0 && y==0 || Mathf.Abs (x) + Mathf.Abs (y) == 2)
					continue;

				int mIntCheckX = node.pIntX + x;
				int mIntCheckY = node.pIntY + y;

				if ((mIntCheckX >= 0 && mIntCheckX < mIntSizeX) && (mIntCheckY >= 0 && mIntCheckY < mIntSizeY)) {
					mListNeighbour.Add(mNodeGrid[mIntCheckX,mIntCheckY]);
				}
			}
		}
		return mListNeighbour;
	}

	//Calculate the the node from a position on the grid
	public Node NodeFromWorldPosition(Vector3 worldPos)
	{
		float mFloatPercentX = (worldPos.x + pVec3GridWorldSize.x/2) / pVec3GridWorldSize.x;
		float mFloatPercentY = (worldPos.z + pVec3GridWorldSize.y/2) / pVec3GridWorldSize.y;

		mFloatPercentX = Mathf.Clamp01 (mFloatPercentX);
		mFloatPercentY = Mathf.Clamp01 (mFloatPercentY);

		int mIntX = Mathf.RoundToInt((mIntSizeX-1)*mFloatPercentX);
		int mIntY = Mathf.RoundToInt((mIntSizeY-1)*mFloatPercentY);

		return mNodeGrid[mIntX, mIntY];
	}


	//Visual representation of the grid
	void OnDrawGizmos()
	{
		Gizmos.DrawWireCube(transform.position, new Vector3(pVec3GridWorldSize.x, 1, pVec3GridWorldSize.y));

		foreach(Node n in mNodeGrid)
		{
			Gizmos.color = (n.pBoolWalkable)?Color.white:Color.red;
			if(fill.pListGridSet!=null)
				if(fill.pListGridSet.Contains(n))
					Gizmos.color = Color.blue;

			Gizmos.DrawCube (n.pVec3WorldPos, Vector3.one * (mFloatGridDiameter - .1f));
		}
	}

}
