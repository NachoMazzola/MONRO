using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;


public class DialogueBottomPanel : MonoBehaviour
{

	public Talkable whoIsTalking;
	[HideInInspector]
	public bool hasFinishedCaptionDisplay;

	public float CaptionDurationUntilFade = 3.0f;
	public float CaptionFadeDuration = 1.5f;
	public float TextSpeed = 0.0001f;
	public Color TextColor = Color.green;
	public int TextSize = 30;
	public Font Font;


	// Use this for initialization
	void Start ()
	{
		
	}

	public IEnumerator AddActionOnFinishAfterCoroutine (IEnumerator coroutineToWait)
	{
		yield return StartCoroutine (coroutineToWait);
		this.hasFinishedCaptionDisplay = true;
	}

	public IEnumerator ShowText (string caption)
	{

		Text theText = this.transform.GetComponentInChildren<Text> ();
		theText.font = this.Font;
		theText.color = this.TextColor;
		theText.fontSize = this.TextSize;

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
}
