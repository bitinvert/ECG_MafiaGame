using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FloodFill : MonoBehaviour {

	public GameController pGCController;
	public List<PlayerController> pListCharacters;

	public Grid pGridField;

	public HashSet<Node> pHashGridSet;

	// Use this for initialization
	void Start () {
		pHashGridSet = new HashSet<Node>();
		pListCharacters = pGCController.pListCharacters;
	}
	
	public void FindPath(Node node, int dist)
	{
		if(pGCController.pTransSeeker != null){
			if(dist > pListCharacters[pListCharacters.IndexOf(pGCController.pTransSeeker.GetComponent<PlayerController>())]
			   .pIntWalkDistance)
			{
				return;
			}
			if(pHashGridSet.Contains (node))
			{
				return;
			}
			if(!node.pBoolWalkable)
			{
				return;
			}

			node.pIntDistValue = dist;
			pHashGridSet.Add (node);

			//TODO Check if out of bounds
			if(!(node.pIntX <= 0 || node.pIntX >= pGridField.mIntSizeX-1 ||
			   node.pIntY <= 0 || node.pIntY >= pGridField.mIntSizeY-1 ))
			{	
				//Westen überprüfen
				FindPath(pGridField.mNodeGrid[node.pIntX-1, node.pIntY], dist+1);
				//Osten überprüfen
				FindPath(pGridField.mNodeGrid[node.pIntX+1, node.pIntY], dist+1);
				//Norden überprüfen
				FindPath(pGridField.mNodeGrid[node.pIntX, node.pIntY+1], dist+1);
				//Sueden überprüfen
				FindPath(pGridField.mNodeGrid[node.pIntX, node.pIntY-1], dist+1);
			}
		}
	}
}
