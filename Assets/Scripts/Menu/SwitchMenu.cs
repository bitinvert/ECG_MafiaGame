using UnityEngine;
using System.Collections;

public class SwitchMenu : MonoBehaviour {

	private CanvasGroup pFrom;
	public CanvasGroup pTo;

	public string method;

	public float time;

	void Start() {
		pFrom = this.GetComponentInParent<CanvasGroup> ();
	}

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

			default: break;
		}
	}

	void LessDrawCallsTest() {
		pFrom.gameObject.SetActive (false);
		pTo.gameObject.SetActive (true);
	}

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

	/*IEnumerator Slide() {
		Debug.Log (Screen.currentResolution.width);
		Vector3 newPos = new Vector3(Screen.currentResolution.width, pFrom.transform.position.y, 0.0f);
		pFrom.transform.position = newPos;
		yield return null;
	}*/
	/*IEnumerator ScaleOut() 
	{
			pFrom.interactable = false;
		pTo.alpha = 1.0f;
		yield return null;

		while (pFrom.transform.localScale.x > 0.0f)
		{
			float curScale = pFrom.transform.localScale.x - time;

			pFrom.transform.localScale -= new Vector3(curScale, curScale, 1.0f);
			yield return null;
		}

		pFrom.blocksRaycasts = false;

		pTo.interactable = true;
		pTo.blocksRaycasts = true;
		yield return null;
	}*/
}
