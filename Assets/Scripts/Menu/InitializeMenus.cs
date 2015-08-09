using UnityEngine;
using System.Collections;

public class InitializeMenus : MonoBehaviour {

	public string startMenu;

	/**
	 * Deactivate all Sub-Menus except the Start-Menu.
	 */
	void Start () {
		// Get all GameObjects that are part of the MainMenu
		GameObject[] menusGO = GameObject.FindGameObjectsWithTag ("Menu");

		// Check all Menu-GameObjects and disable them except the startMenu and the Overlay (Options etc.)
		foreach(GameObject menuGO in menusGO) {
			CanvasGroup menu = menuGO.GetComponent<CanvasGroup>();

			if(!(menu.name.Equals(startMenu) || menu.name.Equals("Standard_Overlay"))) {
				menuGO.SetActive(false);
			} 
		}
	}
}
