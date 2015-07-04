using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UpdateTappedCharacterInformation : MonoBehaviour {

	public PlayerController pc;
	public GameObject charInfo;
	public Slider lifebar;
	public Text life;

	private Unit tapped = null;

	// Update is called once per frame
	void Update () {
		tapped = pc.pUnitTapped;

		if (tapped == null) 
		{

			charInfo.SetActive(false);

		} else 
		{
			charInfo.SetActive(true);

			if(tapped.GetType() == typeof(Beagleboy))
			{ 

			} else if(tapped.GetType() == typeof(Bruiser))
			{ 
				
			} else if(tapped.GetType() == typeof(Medic))
			{ 
				
			} else if(tapped.GetType() == typeof(Gunslinger))
			{ 
			
			} else if(tapped.GetType() == typeof(Rifleman))
			{ 
				
			} else if(tapped.GetType() == typeof(Slicer))
			{

			}

			int fullHealth = tapped.pIntFullHealth;
			int actHealth = tapped.pIntHealth;

			lifebar.value = ((1.0f / fullHealth) * actHealth);
			life.text = actHealth + " / " + fullHealth;
		}
	}
}
