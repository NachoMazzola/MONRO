using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

/**
 * Creates commands based on what verb button was tapped
*/
public class VerbsButtonPanelHandler : MonoBehaviour {

	public Color SelectedColor;
	public Color UnselectedColor;

	private Button lookAtButton;
	private Button talkToButton;
	private Button pickUpButton;
	private Button useButton;

	private Button currentSelected;

	private PlayerCommandBuilder uiCommandBuilder;

	void Awake() {
		this.uiCommandBuilder = new PlayerCommandBuilder();
		this.uiCommandBuilder.uiType = UIType.VerbsPanel;

		this.ResetButtons();

		this.lookAtButton = this.transform.Find("Look At").GetComponent<Button>();
		this.talkToButton = this.transform.Find("Talk To").GetComponent<Button>();
		this.pickUpButton = this.transform.Find("PickUp").GetComponent<Button>();
		this.useButton = this.transform.Find("Use").GetComponent<Button>();
	}

	public void CreateLookAtCommand() {
		if (this.currentSelected == this.lookAtButton) {
			this.ResetButtons();
			return;
		}

		this.SetButtonAsSelected(this.lookAtButton, true);
		this.uiCommandBuilder.CreateLookAtCommand();
	}

	public void CreateTalkToCommand() {
		if (this.currentSelected == this.talkToButton) {
			this.ResetButtons();
			return;
		}

		this.SetButtonAsSelected(this.talkToButton, true);
		this.uiCommandBuilder.CreateTalkToCommand();
	}

	public void CreatePickUpCommand() {
		if (this.currentSelected == this.pickUpButton) {
			this.ResetButtons();
			return;
		}

		this.SetButtonAsSelected(this.pickUpButton, true);
		this.uiCommandBuilder.CreatePickUpCommand();
	}

	public void CreateUseCommand() {
		//????
	}

	public void ResetButtons() {
		this.currentSelected = null;

		for (int i = 0; i <  this.gameObject.transform.childCount; i++) {
			Transform childTransform = this.gameObject.transform.GetChild(i);
			Button button = childTransform.GetComponent<Button>();
			Text buttonText = button.transform.GetChild(0).GetComponent<Text>();
			buttonText.color = this.UnselectedColor;
			buttonText.fontStyle = FontStyle.Normal;
		}
	}

	private void SetButtonAsSelected(Button button, bool selected) {
		this.ResetButtons();

		Text buttonText = button.transform.GetChild(0).GetComponent<Text>();
		buttonText.color = selected ? this.SelectedColor : this.UnselectedColor;
		buttonText.fontStyle = selected ? FontStyle.Italic : FontStyle.Normal;

		this.currentSelected = button;
	}
}
