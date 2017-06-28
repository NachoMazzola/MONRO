using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Text;

public class TextBox : MonoBehaviour {

	public enum DisappearMode {
		Fade,
		WaitInput
	}

	public float CaptionDurationUntilFade = 3.0f;
	public float CaptionFadeDuration = 1.5f;
	public float TextSpeed = 0.025f;
	public Color TextColor = Color.black;

	private bool showingCaption;
	private IEnumerator hideUICoroutine;
	private IEnumerator removeCaptionCoroutine;
	private bool shouldAttachToCaller;
	private Transform followTransform;

	// Update is called once per frame
	void Update () {

		//mantain the texbox "attached" to the caller
		if (shouldAttachToCaller && followTransform) {
			PositionateCaptionOverGameObject(followTransform);
		}
	}

	public IEnumerator ShowCaptionFromGameObject(string caption, GameObject fromGO, bool followCaller, DisappearMode removalMode = DisappearMode.Fade) {
		if (showingCaption == true) {
			yield return null;
		}
		shouldAttachToCaller = followCaller;
		if (shouldAttachToCaller) {
			followTransform = fromGO.transform;
		}

		showingCaption = true;

		this.gameObject.SetActive(true);

		PositionateCaptionOverGameObject(fromGO.transform);

		Text theText = this.transform.GetComponentInChildren<Text>();
		theText.color = TextColor;

		if (TextSpeed > 0.0f) {
			// Display the line one character at a time
			var stringBuilder = new StringBuilder ();

			foreach (char c in caption) {
				stringBuilder.Append (c);
				theText.text = stringBuilder.ToString ();
				yield return new WaitForSeconds (TextSpeed);
			}
		} else {
			// Display the line immediately if textSpeed == 0
			theText.text = caption;
		}

		if (removalMode == DisappearMode.Fade) {
			hideUICoroutine = HideTalkUI(this.gameObject, CaptionDurationUntilFade, theText);
			yield return StartCoroutine(hideUICoroutine);
		}
		else {
			// Wait for any user input
			while (Input.anyKeyDown == false) {
				yield return null;
			}
		}
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

	public IEnumerator HideTalkUI (GameObject guiParentCanvas, float secondsToWait, Text textToFade)
	{
		yield return new WaitForSeconds (secondsToWait);

		textToFade.CrossFadeColor(Color.black, CaptionFadeDuration, false, false);
		textToFade.CrossFadeAlpha(0.0f, CaptionFadeDuration, false); 

		removeCaptionCoroutine = RemoveCaptionAfterSeconds(1.5f, guiParentCanvas);

		StartCoroutine(removeCaptionCoroutine);
	}

	public IEnumerator RemoveCaptionAfterSeconds(float secondsToWait, GameObject guiParentCanvas) {
		yield return new WaitForSeconds (secondsToWait);
		guiParentCanvas.SetActive (false);


		this.gameObject.SetActive(false);
		showingCaption = false;
		shouldAttachToCaller = false;
		followTransform = null;
	}

	public bool CaptionIsBeingShown() {
		return showingCaption;
	}
}
