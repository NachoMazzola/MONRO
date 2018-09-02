using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
using Yarn.Unity;


public class DialogueBottomPanel : MonoBehaviour
{
	public float TextSpeed = 0.0001f;

	[HideInInspector]
	public bool hasFinishedCaptionDisplay;

	private Text displayedText;

	private DialogueOptionsDisplayer buttonsPositionHandler;
	private List<Button> optionButtons;

	private Image talkingImage;
	private Talkable whoIsTalking;
	private Transform conversationOptionsPanel;



	// Use this for initialization
	void Start () {
		this.displayedText = this.transform.GetComponentInChildren<Text> ();
		this.talkingImage = this.transform.Find("TalkablePortrait").GetComponent<Image>();
		this.conversationOptionsPanel = this.transform.Find("ConversationOptionsPanel");
		this.MustShowDialogueOptions(false);
	}

	public IEnumerator AddActionOnFinishAfterCoroutine (IEnumerator coroutineToWait)
	{
		yield return StartCoroutine (coroutineToWait);
		this.hasFinishedCaptionDisplay = true;
	}

	public IEnumerator ShowText (string caption, Talkable whoIsTalking)
	{
		this.whoIsTalking = whoIsTalking;
		this.talkingImage.sprite = this.whoIsTalking.talkableImage;

		this.displayedText.font = this.whoIsTalking.textFont;
		this.displayedText.color = this.whoIsTalking.TextColor;
		this.displayedText.fontSize = this.whoIsTalking.TextSize;

		if (TextSpeed > 0.0f) {
			// Display the line one character at a time
			var stringBuilder = new StringBuilder ();

			foreach (char c in caption) {
				stringBuilder.Append (c);
				this.displayedText.text = stringBuilder.ToString ();
				yield return new WaitForSeconds (TextSpeed);
			}
		} else {
			// Display the line immediately if textSpeed == 0
			this.displayedText.text = caption;
		}
			
		// Wait for any user input
		while (Input.anyKeyDown == false) {
			yield return null;
		}
	}

	public IEnumerator RemoveCaptionAfterSeconds (float secondsToWait)
	{
		yield return new WaitForSeconds (secondsToWait);

		StopAllCoroutines ();
	}
		
	public void startLine ()
	{
		whoIsTalking.PlayAnimation ();
	}

	public void finishedLine ()
	{
		whoIsTalking.StopAnimation ();
	}

	public void MustShowDialogueOptions(bool show) {
		this.displayedText.gameObject.SetActive(!show);
		this.conversationOptionsPanel.gameObject.SetActive (show);
	}

	public void SetupOptionButtons(List<Button> optionButtons) {
		this.optionButtons = optionButtons;

		foreach (Button optionButton in this.optionButtons) {
			optionButton.gameObject.SetActive (false);
		}

		this.buttonsPositionHandler = new DialogueOptionsDisplayer (this.optionButtons);
		this.buttonsPositionHandler.SetOriginPositions();
	}

	public void CreateButtonForOptions(Yarn.Options optionsCollection) {
		this.conversationOptionsPanel.gameObject.SetActive (true);
		this.buttonsPositionHandler.PositionateButtons (optionsCollection);
	}

	public void ResetDialogueOptionsButtons () {
		this.buttonsPositionHandler.SetOriginPositions();
	}
}
