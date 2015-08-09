using UnityEngine;
using System.Collections;

public class SwitchMenu : MonoBehaviour {

	// The menu to deactivate
	private CanvasGroup pFrom;
	// The menu to activate
	public CanvasGroup pTo;

	// Leftovers from previous attemps and rejected ideas
	public string method;
	public float time;

	// Get the current SubMenu
	void Start() {
		pFrom = this.GetComponentInParent<CanvasGroup> ();
	}

	/**
	 * A leftover from previous attemps that is used partly in
	 * the actual version of the game.
	 * It was supposed to be used to call one of the transistion
	 * methods which required an IEnumerator.
	 * 
	 * For the actual version it only uses "LessDrawCallsTest",
	 * "OverlayStatisticBack" and "OverlayStatisticTo".
	 */
	public void Switch() 
	{
		switch (method)
		{
		case "FadeOut": StartCoroutine(FadeOut());
			break;
			
		case "FadeIn": StartCoroutine(FadeIn ());
			break;
			
		case "FadeGameMenuIn": StartCoroutine(FadeGameMenuIn ());
			break;
			
		case "FadeGameMenuOut": StartCoroutine(FadeGameMenuOut ());
			break;
			
		case "LessDrawCallsTest": LessDrawCallsTest();
			break;
			
		case "OverlayBack": OverlayStatisticBack();
			break;
			
		case "OverlayTo": OverlayStatisticTo();
			break;
			
		default: break;
		}
	}

	/**
	 * The main Method used to switch between two different SubMenus.
	 * It activates one and disables the other.
	 */
	void LessDrawCallsTest() {
		pFrom.gameObject.SetActive (false);
		pTo.gameObject.SetActive (true);
	}

	/**
	 * An extra method to deactivate an OverlayMenu which didn't need
	 * the Menu in the Background to be disabled.
	 */
	void OverlayStatisticBack() {
		pFrom.gameObject.SetActive (false);
	}

	/**
	 * An extra method do actiavte an OverlayMenu without deactivating
	 * the Menu in the Background.
	 */
	void OverlayStatisticTo() {
		pTo.gameObject.SetActive (true);
	}

	/**
	 * The following part is not used in the actual game.
	 * It's working but was rejected.
	 * It creates a fadeOut/fadeIn effect when switching 
	 * between the menus.
	 */


	IEnumerator FadeOut()
	{
		pFrom.interactable = false;
		pTo.alpha = 1.0f;
		yield return null;

		while (pFrom.alpha > 0)
		{
			pFrom.alpha -= Time.deltaTime * time;
			yield return null; // wait for next frame
		}

		pFrom.blocksRaycasts = false;

		pTo.interactable = true;
		pTo.blocksRaycasts = true;
		yield return null;
	}

	IEnumerator FadeIn() 
	{
		pFrom.interactable = false;
		yield return null;

		while (pTo.alpha < 1.0f) 
		{
			pTo.alpha += Time.deltaTime * time;
			yield return null;
		}

		pFrom.blocksRaycasts = false;
		pFrom.alpha = 0.0f;

		pTo.interactable = true;
		pTo.blocksRaycasts = true;
		yield return null;
	}

	IEnumerator FadeGameMenuIn() 
	{
		pFrom.interactable = false;
		yield return null;

		while (pTo.alpha < 1)
		{
			pTo.alpha += Time.deltaTime * time;
			yield return null;
		}

		pFrom.blocksRaycasts = false;

		pTo.interactable = true;
		pTo.blocksRaycasts = true;
		yield return null;
	}

	IEnumerator FadeGameMenuOut()
	{
		pFrom.interactable = false;
		yield return null;

		while (pFrom.alpha > 0)
		{
			pFrom.alpha -= Time.deltaTime * time;
			yield return null;
		}

		pFrom.blocksRaycasts = false;

		pTo.interactable = true;
		pTo.blocksRaycasts = true;
		yield return null;
	}
}
