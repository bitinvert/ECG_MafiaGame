using UnityEngine;
using System.Collections;

public class ShowAnimation : MonoBehaviour {
	
	public Animator animator;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void showAni (int animation, int direction)
	{
		switch (animation) {
		case (0):
			animator.SetInteger("AnimationID", 0);
			break;
		case (1):
			animator.SetInteger("AnimationID", 1);
			break;
		case (2):
			animator.SetInteger("AnimationID", 2);
			break;
		case (3):
			animator.SetInteger("AnimationID", 3);
			break;
		case (4):
			animator.SetInteger("AnimationID", 4);
			break;
		}

		switch (direction) {
		case (1):
			animator.SetInteger("Direction", 1);
			break;
		case (2):
			animator.SetInteger("Direction", 2);
			break;
		case (3):
			animator.SetInteger("Direction", 3);
			break;
		case (4):
			animator.SetInteger("Direction", 4);
			break;
		}
	}

}
