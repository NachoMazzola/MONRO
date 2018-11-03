using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
using Yarn.Unity;

public class DialogueOptionsDisplayer {
	private List<Button> optionButtons;
	private float optionButtonYDisplacement;
	private int inactiveButtons;

	public DialogueOptionsDisplayer(List<Button> optionButtons) {
		this.optionButtons = optionButtons;
	}

	public void SetOriginPositions() {
		return;
//		float bHeight = this.optionButtons [0].GetComponent<RectTransform> ().rect.height;
//		this.optionButtonYDisplacement = bHeight;
//		for (int j = 0; j < optionButtons.Count; j++) {
////			int index = j - 1;
////			if (index < 0) {
////				continue;
////			}
//			Button currentButton = optionButtons [0];
//			RectTransform buttonRect = currentButton.GetComponent<RectTransform> ();
//
//			buttonRect.anchoredPosition = new Vector2 (buttonRect.anchoredPosition.x, buttonRect.anchoredPosition.y - optionButtonYDisplacement*j);
//		}
	}

	public void PositionateButtons(Yarn.Options optionsCollection) {
		// Display each option in a button, and make it visible
		int i = 0;
		foreach (var optionString in optionsCollection.options) {
			this.optionButtons [i].gameObject.SetActive (true);
			this.optionButtons [i].GetComponentInChildren<Text> ().text = optionString;
			i++;
		}

		this.inactiveButtons = this.optionButtons.Count - optionsCollection.options.Count;

		if (inactiveButtons > 0) {
			float bHeight = this.optionButtons [0].GetComponent<RectTransform> ().rect.height;
			this.optionButtonYDisplacement = 0;//bHeight * inactiveButtons;

			for (int j = 0; j < optionButtons.Count; j++) {
				Button currentButton = optionButtons [j];
				RectTransform buttonRect = currentButton.GetComponent<RectTransform> ();
				string optionStr = currentButton.GetComponentInChildren<Text>().text;
				//if the optionsCollection does not contains this button text, 
				//it means that the button is no longer active and we should move
				//up the next button by optionButtonYDisplacement value
				if (!optionsCollection.options.Contains(optionStr)) {
					this.optionButtonYDisplacement += bHeight;
					continue;
				}

				buttonRect.anchoredPosition = new Vector2 (buttonRect.anchoredPosition.x, buttonRect.anchoredPosition.y - optionButtonYDisplacement);
			}
		}
	}


}
