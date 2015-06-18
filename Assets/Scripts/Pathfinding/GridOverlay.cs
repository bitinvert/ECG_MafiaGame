using UnityEngine;
using System.Collections;

public class GridOverlay : MonoBehaviour {

	[SerializeField]
	private GameObject pathfinding; 

	private Grid grid;
	//private AStar astar;
	private FloodFill floodFill;

	//Array of all nodes
	private Node[,] mNodeGrid;
	private Material lineMaterial;
	private Color lineColor = new Color(0.1f, 0.1f, 0.1f, 0.3f);
	private Color walkColor = new Color(0.0f, 0.7f, 0.9f, 0.5f);

	private bool gridCreated;

	//Needed because the Camera's zero-Point is somewhere else
	public float offsetX;
	public float offsetZ;

	// Use this for initialization
	void Start () {
		grid = pathfinding.GetComponent<Grid> ();
		//astar = pathfinding.GetComponent<AStar> ();
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
