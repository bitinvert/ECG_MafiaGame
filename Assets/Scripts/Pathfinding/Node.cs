using UnityEngine;
using System.Collections;

public class Node {

	//Determins if a tile is walable	
	public bool pBoolWalkable;
	public bool pBoolReachable;
	//x and y position in the array of all nodes
	public int pIntX;
	public int pIntY;

	//Parent node of a path
	public Node pNodeParent;

	/**
	 * Movement cost
	 * H = Movement Cost from Node to final Node
	 * G = Movement Cost from Starting node to Node
	 **/
	public int pIntHCost;
	public int pIntGCost;

	//Global position	
	public Vector3 pVec3WorldPos;

	public int pIntDistValue;

	/**
	 * Constructor to set the values
	 **/
	public Node(bool walkable, Vector3 worldPos, int x, int y)
	{
		pBoolWalkable = walkable;
		pVec3WorldPos = worldPos;
		pIntX = x;
		pIntY = y;
	}


	/**
	 * Getter for F Cost
	 * F = H + G
	 **/
	public int pIntFCost
	{
		get 
		{
			return pIntHCost + pIntGCost;
		}
	}
}
