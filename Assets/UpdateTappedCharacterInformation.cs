using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UpdateTappedCharacterInformation : MonoBehaviour {

	public PlayerController pc;
	public GameObject charInfo;
	public Slider lifebar;
	public Text life;

	public GameObject[] avatars = new GameObject[12];

	private Unit tapped = null;

	// Update is called once per frame
	void Update () {
		tapped = pc.pUnitTapped;

		/**
		 * Deactivate all Pictures
		 */
		for (int i = 0; i < 12; i++) {
			avatars[i].SetActive(false);
		}

		/**
		 * If none Character is tapped, deactivate
		 * the whole CharacterInfo.
		 */
		if (tapped == null) 
		{
			charInfo.SetActive(false);

		} else 
		{
			/**
			 * Check the Class and Fraction of the
			 * tapped Character.
			 * Activate the specific Fraction_Class
			 * Avatar.
			 */ 
		
			charInfo.SetActive(true);

			if(tapped.GetType() == typeof(Beagleboy))
			{ 
				if(tapped.pFacFaction.Equals("Mafia")) 
				{
					avatars[0].SetActive(true);
					Debug.Log (0);
				} else 
				{
					avatars[1].SetActive(true);
					Debug.Log (1);
				}

			} else if(tapped.GetType() == typeof(Bruiser))
			{ 
				if(tapped.pFacFaction.Equals("Mafia")) 
				{
					avatars[2].SetActive(true);
					Debug.Log (2);
				} else 
				{
					avatars[3].SetActive(true);
					Debug.Log (3);
				}
				
			} else if(tapped.GetType() == typeof(Medic))
			{ 
				if(tapped.pFacFaction.Equals("Mafia")) 
				{
					avatars[4].SetActive(true);

				} else 
				{
					avatars[5].SetActive(true);
				}
				
			} else if(tapped.GetType() == typeof(Gunslinger))
			{ 
				if(tapped.pFacFaction.Equals("Mafia")) 
				{
					avatars[6].SetActive(true);

				} else 
				{
					avatars[7].SetActive(true);
				}	

			} else if(tapped.GetType() == typeof(Rifleman))
			{ 
				if(tapped.pFacFaction.Equals("Mafia")) 
				{
					avatars[8].SetActive(true);

				} else 
				{
					avatars[9].SetActive(true);
				}
				
			} else if(tapped.GetType() == typeof(Slicer))
			{
				if(tapped.pFacFaction.Equals("Mafia")) 
				{
					avatars[10].SetActive(true);

				} else 
				{
					avatars[11].SetActive(true);
				}
			}

			int fullHealth = tapped.pIntFullHealth;
			int actHealth = tapped.pIntHealth;

			lifebar.value = ((1.0f / fullHealth) * actHealth);
			life.text = actHealth + " / " + fullHealth;
		}
	}
}
