using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class CardChoice : MonoBehaviour {

	private bool mafiaActive;
	private bool policeActive;

	public Button mafiaButton;
	public Button policeButton;

	public Animator anim;

	public SwitchMenu mafiaSwitch;
	public SwitchMenu policeSwitch;

	// Use this for initialization
	void Start () 
	{
		mafiaActive = false;
		policeActive = false;

		mafiaButton.onClick.AddListener (() => {
			if(mafiaActive == true) 
			{
				mafiaSwitch.Switch();

			}
		});

		policeButton.onClick.AddListener (() => {
			if (policeActive == true)
			{
				policeSwitch.Switch();

			}
		});
	}
	
	void setMtP() {
		mafiaActive = true;
		policeActive = false;
	}

	void setPtM() {
		policeActive = true;
		mafiaActive = false;
	}
}
