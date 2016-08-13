using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerCaption : MonoBehaviour {

	public Transform PlayerCaptionPrefab;
	public float CaptionDurationUntilFade = 3.0f;
	public float CaptionFadeDuration = 1.5f;


	private Transform instantiatedCaption;
	private bool showingCaption;
	private Vector2 originalScale;

	private IEnumerator hideUICoroutine;
	private IEnumerator removeCaptionCoroutine;



	void Awake() {
		originalScale = new Vector2(0.02f, 0.01f);
	}

	// Use this for initialization
	void Start () {
			

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ShowCaption(string caption) {
		if (showingCaption == true) {
			Text showingText = SetCaptionText(caption);

			showingText.CrossFadeAlpha(1.0f, 0, false);
			showingText.CrossFadeColor(Color.white, 0, false, false);

			StopCoroutine(hideUICoroutine);
			StopCoroutine(removeCaptionCoroutine);


			StartCoroutine(HideTalkUI(instantiatedCaption.gameObject, CaptionDurationUntilFade, showingText));

			return;
		}


		showingCaption = true;

		//InstantiateCaption();
		instantiatedCaption = this.gameObject.transform;

		Text theText = SetCaptionText(caption);

		hideUICoroutine = HideTalkUI(instantiatedCaption.gameObject, CaptionDurationUntilFade, theText);
		StartCoroutine(hideUICoroutine);
	}

	Text SetCaptionText(string caption) {
		Text theText = instantiatedCaption.gameObject.GetComponentInChildren<Text>();
		theText.text = caption;

		return theText;
	}

	IEnumerator HideTalkUI (GameObject guiParentCanvas, float secondsToWait, Text textToFade)
	{
		yield return new WaitForSeconds (secondsToWait);

		textToFade.CrossFadeColor(Color.black, CaptionFadeDuration, false, false);
		textToFade.CrossFadeAlpha(0.0f, CaptionFadeDuration, false); 

		removeCaptionCoroutine = RemoveCaptionAfterSeconds(1.5f, guiParentCanvas);

		StartCoroutine(removeCaptionCoroutine);

	}

	IEnumerator RemoveCaptionAfterSeconds(float secondsToWait, GameObject guiParentCanvas) {
		yield return new WaitForSeconds (secondsToWait);
		guiParentCanvas.SetActive (false);

		Destroy(instantiatedCaption.gameObject);
		instantiatedCaption = null;

		showingCaption = false;
	}

	public bool CaptionIsBeingShown() {
		return showingCaption;
	}

	public void PreserveOriginalScale(float playerXScale) {
		if (instantiatedCaption != null) {
			if (playerXScale < 0) {
				Vector2 captionScale = originalScale;
				captionScale.x *= -1;
				instantiatedCaption.localScale = captionScale;
			}
			else {
				instantiatedCaption.localScale = originalScale;
			}


		}	
	}
}
