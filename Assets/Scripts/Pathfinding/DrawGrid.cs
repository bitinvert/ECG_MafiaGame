using UnityEngine;
using System.Collections;

// Quelle: http://answers.unity3d.com/questions/482128/draw-grid-lines-in-game-view.html#

public class DrawGrid : MonoBehaviour {
	
	//public GameObject plane;
	
	private bool showMain = true;
	
	public int gridSizeX;
	public int gridSizeY;
	public int gridSizeZ;

	public float step;
	
	public float startX;
	public float startY;
	public float startZ;
	
	private float offsetY = 0f;
	//private float scrollRate = 0.1f;
	//private float lastScroll = 0f;
	
	private Material lineMaterial;
	
	private Color mainColor = new Color(0f,1f,0f,0.5f);


	void Start () 
	{

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
		CreateLineMaterial();
		// set the current material
		lineMaterial.SetPass( 0 );

		GL.Begin( GL.LINES );
	
		if(showMain)
		{
			GL.Color(mainColor);
			
			//Layers
			for(float j = 0; j <= gridSizeY; j += step)
			{
				//X axis lines
				for(float i = 0; i <= gridSizeZ; i += step)
				{
					GL.Vertex3( startX, j + offsetY, startZ + i);
					GL.Vertex3( startX + gridSizeX, j + offsetY, startZ + i);
				}
				
				//Z axis lines
				for(float i = 0; i <= gridSizeX; i += step)
				{
					GL.Vertex3( startX + i, j + offsetY, startZ);
					GL.Vertex3( startX + i, j + offsetY, startZ + gridSizeZ);
				}
			}
			
			//Y axis lines
			for(float i = 0; i <= gridSizeZ; i += step)
			{
				for(float k = 0; k <= gridSizeX; k += step)
				{
					GL.Vertex3( startX + k, startY + offsetY, startZ + i);
					GL.Vertex3( startX + k, startY + gridSizeY + offsetY, startZ + i);
					
				}
			}
		}

		GL.End();


		GL.Begin(GL.TRIANGLE_STRIP);

			GL.Color(new Color(1f, 0f, 1f, 0.5f));
			GL.Vertex3(0f + startX, 0f, 0f + startZ);
			GL.Vertex3(0f + startX, 0f, 1f + startZ);
			GL.Vertex3(1f + startX, 0f, 1f + startZ);
			GL.Vertex3(1f + startX, 0f, 0f + startZ);
			
		GL.End();
	}
}
