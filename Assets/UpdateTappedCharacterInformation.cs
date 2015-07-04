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

		for (int i = 0; i < 12; i++) {
			avatars[0].SetActive(false);
		}

		if (tapped == null) 
		{

			charInfo.SetActive(false);

		} else 
		{
			charInfo.SetActive(true);

			if(tapped.GetType() == typeof(Beagleboy))
			{ 
				if(tapped.pFacFaction.Equals("Mafia")) 
				{
					avatars[0].SetActive(true);

				} else 
				{
					avatars[1].SetActive(true);
				}

			} else if(tapped.GetType() == typeof(Bruiser))
			{ 
				if(tapped.pFacFaction.Equals("Mafia")) 
				{
					avatars[2].SetActive(true);

				} else 
				{
					avatars[3].SetActive(true);
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
