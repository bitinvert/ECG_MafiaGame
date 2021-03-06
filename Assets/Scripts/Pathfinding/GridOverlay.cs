﻿using UnityEngine;
using System.Collections;

public class GridOverlay : MonoBehaviour {

	[SerializeField]
	private GameObject pathfinding; 

	[SerializeField]
	private PlayerController playerController;

	private Grid grid;
	private FloodFill floodFill;

	// Variables to set the Color of the actionRadius for movement, attack and special
	public Color moveColor = new Color(1f, 1f, 1f);
	public Color attackColor = new Color(1f, 1f, 1f);
	public Color specialColor = new Color(1f, 1f, 1f);

	public Color selectedFieldColor = new Color (1f, 1f, 1f);

	//Array of all nodes
	private Node[,] mNodeGrid;
	private Material lineMaterial;
	private Color lineColor = new Color(0.1f, 0.1f, 0.1f, 0.3f);
	private Color walkColor = new Color(0.0f, 0.7f, 0.9f, 0.5f);

	private bool gridCreated;

	//Needed because the Camera's zero-Point is somewhere else
	public float offsetX;
	public float offsetZ;

	/**
	 * Try to get a reference to the grid and floodFill created in 
	 * different scripts.
	 * Also create the Material used for drawing the GridOverlay with
	 * a specific shader.
	 */
	void Start () {
		grid = pathfinding.GetComponent<Grid> ();
		floodFill = pathfinding.GetComponent<FloodFill> ();

		//POSITION NICHT VERÄNDERN!!!
		CreateLineMaterial ();

		gridCreated = false;

		StartCoroutine (CheckIsGridCreated ());
	}

	/**
	 * Check if the Grid in Grid.cs has been successfully created.
	 * This is needed since there is a chance we will get a NullReferenceException
	 * in OnPostRender if we don't check this because of the nodes.
	 */
	IEnumerator CheckIsGridCreated()
	{
		while (true) 
		{
			if (grid.isGridCreated) 
			{
				Debug.Log ("Griderstellung beendet - Starte Grid-Zeichnen");
				this.mNodeGrid = grid.mNodeGrid;
				gridCreated = true;

				break;
			}
			yield return null;
		}
		yield return null;
	}

	/**
	 * Create the Shader used to show the GridOverlay over everything else
	 * so the movement radius etc. isn't hidden behind objects and thus a 
	 * Unit not moveable anymore.
	 */
	void CreateLineMaterial() 
	{
		if( !lineMaterial ) {
			lineMaterial = new Material ("Shader \"Lines\" {" +
			                             "  Subshader {" +
			                             "    Pass {" +
			                             "      Blend SrcAlpha OneMinusSrcAlpha" +  
			                             "      ZTest Always" +
			                             "      ZWrite On" +
			                             "      Cull Off" +	
			                             "      BindChannels {" +
			                             "        Bind \"vertex\", " +
			                             "        vertex Bind \"color\"," +
			                             "        color" +
			                             "      }" +
			                             "    }" +
			                             "  }" +
			                             "}");
			
			lineMaterial.hideFlags = HideFlags.HideAndDontSave;
			lineMaterial.shader.hideFlags = HideFlags.HideAndDontSave;}
	}

	/**
	 * Check if the player wants to attack, move or use special.
	 * Set the walkColor accordingly.
	 */
	void Update() 
	{
		if (playerController.pBoolShowAttack) 
		{
			walkColor = attackColor;

		} else if (playerController.pBoolShowMove) 
		{
			walkColor = moveColor;

		} else if (playerController.pBoolShowSpecial) 
		{
			walkColor = specialColor;
		}
	}

	/**
	 * Draw the GridOverlay OnPostRender by checking every node in "mNodeGrid" and
	 * drawing lines for every node if it's walkable.
	 * The lower part is used to draw the filled in GridOverlay part to show the action radius
	 * of an active unit. Only if a node is reachable AND walkable, it will be filled with the
	 * corresponding color.
	 * The selected field will be displayed in a different color.
	 */
	void OnPostRender()  
	{
		if (gridCreated) 
		{
			// set the current material
			lineMaterial.SetPass (0);

			foreach(Node n in mNodeGrid) {
				Vector3 p1 = new Vector3(n.pIntX + 0.5f + offsetX, 0, n.pIntY + 0.5f + offsetZ);
				Vector3 p2 = new Vector3(n.pIntX + 0.5f + offsetX, 0, n.pIntY - 0.5f + offsetZ);
				Vector3 p3 = new Vector3(n.pIntX - 0.5f + offsetX, 0, n.pIntY - 0.5f + offsetZ);
				Vector3 p4 = new Vector3(n.pIntX - 0.5f + offsetX, 0, n.pIntY + 0.5f + offsetZ);

				if(n.pBoolWalkable) 
				{
					GL.Begin(GL.LINES);

						GL.Color(lineColor);

						GL.Vertex(p1);
						GL.Vertex(p2);

						GL.Vertex(p2);
						GL.Vertex(p3);

						GL.Vertex(p3);
						GL.Vertex(p4);

						GL.Vertex(p4);
						GL.Vertex(p1);

					GL.End();
				} 
				if(n.pBoolReachable && n.pBoolWalkable)
				{
					GL.Begin(GL.TRIANGLE_STRIP);
					
						GL.Color (walkColor);

					if(playerController.pUnitActive.pGOTarget != null) {
						Vector3 targetPosition = playerController.pUnitActive.pGOTarget.transform.position;

						if((n.pVec3WorldPos.x) == System.Math.Round(targetPosition.x, 2) && (n.pVec3WorldPos.z) == System.Math.Round(targetPosition.z, 2)) {

							GL.Color(selectedFieldColor);
						}
					}					

						GL.Vertex(p1);
						GL.Vertex(p2);
						GL.Vertex(p3);
						GL.Vertex(p3);
						GL.Vertex(p4);
						GL.Vertex(p1);

					GL.End();
				}
			}
		} 
	}
}
