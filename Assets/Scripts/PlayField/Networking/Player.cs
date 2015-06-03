using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	
	public float speed = 10f;
	private Rigidbody rigidbody;
	private NetworkView networkView;

	/// <summary>
	/// Start this instance.
	/// </summary>
	void Start ()
	{
		rigidbody = GetComponent <Rigidbody> ();
		networkView = GetComponent <NetworkView> ();
	}

	/// <summary>
	/// Fixeds the update.
	/// </summary>
	void FixedUpdate ()
	{
		if (networkView.isMine) 
		{
			InputMovement ();
		}
	}

	/// <summary>
	/// Test movement via wasd-keys.
	/// </summary>
	void InputMovement ()
	{
		if (Input.GetKey (KeyCode.W)) 
		{
			rigidbody.MovePosition(rigidbody.position + Vector3.forward * speed * Time.deltaTime);
		}
		if (Input.GetKey (KeyCode.S)) 
		{
			rigidbody.MovePosition(rigidbody.position - Vector3.forward * speed * Time.deltaTime);
		}
		if (Input.GetKey (KeyCode.D)) 
		{
			rigidbody.MovePosition (rigidbody.position + Vector3.right * speed * Time.deltaTime);
		}
		if (Input.GetKey (KeyCode.A)) 
		{
			rigidbody.MovePosition (rigidbody.position + Vector3.left * speed * Time.deltaTime);
		}
	}
}
