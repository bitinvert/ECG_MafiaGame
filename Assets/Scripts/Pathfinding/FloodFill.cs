using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FloodFill : MonoBehaviour {

	public GameController pGCController;
	public List<Unit> pListCharacters;

	public Grid pGridField;

	public HashSet<Node> pListGridSet;

	// Use this for initialization
	void Start () {
		pListGridSet = new HashSet<Node>();
		pListCharacters = pGCController.pListCharacters;
	}
	
	public void FindPath(Node node, int dist, int maxDist)
	{
		if(pGCController.pTransSeeker != null){
			if(dist > maxDist)
			{
				return;
			}
			if(node.pBoolWalkable == false)
			{
				return;
			}

			node.pIntDistValue = dist;
			pListGridSet.Add (node);


			if(!(node.pIntX <= 0))
			{	
				//Westen überprüfen
				FindPath(pGridField.mNodeGrid[node.pIntX-1, node.pIntY], dist+1,maxDist);
			}
			if(!(node.pIntX >= pGridField.mIntSizeX-1))
			{
				//Osten überprüfen
				FindPath(pGridField.mNodeGrid[node.pIntX+1, node.pIntY], dist+1,maxDist);
			}
			if(!(node.pIntY <= 0))
			{
				//Sueden überprüfen
				FindPath(pGridField.mNodeGrid[node.pIntX, node.pIntY-1], dist+1,maxDist);

			}
			if(!(node.pIntY >= pGridField.mIntSizeY-1))
			{
				//Norden überprüfen
				FindPath(pGridField.mNodeGrid[node.pIntX, node.pIntY+1], dist+1,maxDist);
			}
		}
	}
}
