using UnityEngine;
using System.Collections;

public class InitializeMenus : MonoBehaviour {

	// Deactivate all Sub-Menus except the Start-Menu
	void Start () {
		GameObject[] menusGO = GameObject.FindGameObjectsWithTag ("Menu");

		foreach(GameObject menuGO in menusGO) {
			CanvasGroup menu = menuGO.GetComponent<CanvasGroup>();

			if(!(menu.name.Equals("Login") || menu.name.Equals("Standard_Overlay"))) {
				//menu.interactable = false;
				//menu.blocksRaycasts = false;
				//menu.alpha = 0.0f;
				menuGO.SetActive(false);
			} 
		}
	}
}
