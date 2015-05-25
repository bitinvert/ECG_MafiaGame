using UnityEngine;
using System.Collections;

public class InitializeMenus : MonoBehaviour {

	public CanvasGroup[] pMenus;

	// Deactivate all Sub-Menus except the Start-Menu
	void Start () {
		foreach(CanvasGroup menu in pMenus) {
			if(!menu.name.Equals("Start")) {
				menu.interactable = false;
				menu.blocksRaycasts = false;
				menu.alpha = 0.0f;
			}
		}
	}
}
