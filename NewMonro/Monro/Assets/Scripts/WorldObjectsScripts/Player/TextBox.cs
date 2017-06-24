using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextBox : MonoBehaviour {

	public float CaptionDurationUntilFade = 3.0f;
	public float CaptionFadeDuration = 1.5f;

	private Transform instantiatedCaption;
	private bool showingCaption;

	private IEnumerator hideUICoroutine;
	private IEnumerator removeCaptionCoroutine;

	private bool shouldAttachToCaller;
	private Transform followTransform;

	void Awake() {
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (shouldAttachToCaller && followTransform) {
			PositionateCaptionOverGameObject(followTransform);
		}
	}

	public IEnumerator ShowCaptionFromGameObject(string caption, GameObject fromGO, bool followCaller) {
		if (showingCaption == true) {
			yield return null;
		}
		shouldAttachToCaller = followCaller;
		if (shouldAttachToCaller) {
			followTransform = fromGO.transform;
		}

		showingCaption = true;

		instantiatedCaption = this.gameObject.transform;
		instantiatedCaption.gameObject.SetActive(true);

		PositionateCaptionOverGameObject(fromGO.transform);

		Text theText = SetCaptionText(caption);

		hideUICoroutine = HideTalkUI(instantiatedCaption.gameObject, CaptionDurationUntilFade, theText);
		yield return StartCoroutine(hideUICoroutine);
	}

	public void PositionateCaptionOverGameObject(Transform overGameObject) {
		Transform textBoxPosition = overGameObject.transform.Find("TextBoxPosition");
		Transform panelTransform = (RectTransform)this.gameObject.transform.GetChild (0);
		if (textBoxPosition) {
			Vector2 gameObjectPosToScreen = Camera.main.WorldToScreenPoint(textBoxPosition.position);
			panelTransform.position = gameObjectPosToScreen;
		}
		else {
			Debug.Log("WARNING: Caption Caller does not have sprite! - Positioning textbox at 0,0");
			Vector2 gameObjectPosToScreen = Camera.main.WorldToScreenPoint(overGameObject.position);
			panelTransform.position = gameObjectPosToScreen;
		}
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


		instantiatedCaption.gameObject.SetActive(false);
		showingCaption = false;
		shouldAttachToCaller = false;
		followTransform = null;
	}

	public bool CaptionIsBeingShown() {
		return showingCaption;
	}
}
