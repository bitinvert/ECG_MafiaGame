using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FloodFill : MonoBehaviour {

	public GameController pGCController;
	public List<PlayerController> pListCharacters;

	public Grid pGridField;

	public List<Node> pListGridSet;

	// Use this for initialization
	void Start () {
		pListGridSet = new List<Node>();
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
			if(pListGridSet.Contains (node))
			{
				return;
			}
			if(!node.pBoolWalkable)
			{
				return;
			}

			node.pIntDistValue = dist;
			pListGridSet.Add (node);

			//TODO Check if out of bounds
			if(!(node.pIntX <= 0))
			{	
				//Westen überprüfen
				FindPath(pGridField.mNodeGrid[node.pIntX-1, node.pIntY], dist+1);


			}
			if(!(node.pIntX >= pGridField.mIntSizeX-1))
			{
				//Osten überprüfen
				FindPath(pGridField.mNodeGrid[node.pIntX+1, node.pIntY], dist+1);
			}
			if(!(node.pIntY <= 0))
			{
				//Sueden überprüfen
				FindPath(pGridField.mNodeGrid[node.pIntX, node.pIntY-1], dist+1);


			}
			if(!(node.pIntY >= pGridField.mIntSizeY-1))
			{
				//Norden überprüfen
				FindPath(pGridField.mNodeGrid[node.pIntX, node.pIntY+1], dist+1);
			}
		}
	}
}
