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

	private Transform lookAtButtonPanel;
	private Transform talkToButtonPanel;
	private Transform pickUpButtonPanel;
	private Button useButton;

	private Transform currentSelected;

	private PlayerCommandBuilder uiCommandBuilder;

	void Awake() {
		this.uiCommandBuilder = new PlayerCommandBuilder();
		this.uiCommandBuilder.uiType = UIType.VerbsPanel;

		this.ResetButtons();

		this.lookAtButtonPanel = this.transform.Find("Look At");
		this.talkToButtonPanel = this.transform.Find("Talk To");
		this.pickUpButtonPanel = this.transform.Find("Pick Up");
		this.useButton = this.transform.Find("Use").GetComponentInChildren<Button>();
	}

	public void CreateLookAtCommand() {
		Button lookAtButton = this.lookAtButtonPanel.GetComponentInChildren<Button>();
		if (this.currentSelected == lookAtButton) {
			this.ResetButtons();
			return;
		}

		this.SetButtonAsSelected(this.lookAtButtonPanel, true);
		this.uiCommandBuilder.CreateLookAtCommand();
	}

	public void CreateTalkToCommand() {
		Button talkToButton = this.talkToButtonPanel.GetComponentInChildren<Button>();
		if (this.currentSelected == talkToButton) {
			this.ResetButtons();
			return;
		}

		this.SetButtonAsSelected(this.talkToButtonPanel, true);
		this.uiCommandBuilder.CreateTalkToCommand();
	}

	public void CreatePickUpCommand() {
		Button pickUp = this.pickUpButtonPanel.GetComponentInChildren<Button>();
		if (this.currentSelected == pickUp) {
			this.ResetButtons();
			return;
		}

		this.SetButtonAsSelected(this.pickUpButtonPanel, true);
		this.uiCommandBuilder.CreatePickUpCommand();
	}

	public void CreateUseCommand() {
		//????
	}

	public void ResetButtons() {
		this.currentSelected = null;

		for (int i = 0; i <  this.gameObject.transform.childCount; i++) {
			Transform childTransform = this.gameObject.transform.GetChild(i);
			//Button button = childTransform.GetComponentInChildren<Button>();
			Text buttonText = childTransform.GetComponentInChildren<Text>();
			buttonText.color = this.UnselectedColor;
			buttonText.fontStyle = FontStyle.Normal;
		}
	}

	private void SetButtonAsSelected(Transform buttonPanel, bool selected) {
		this.ResetButtons();

		Text buttonText = buttonPanel.GetComponentInChildren<Text>();
		buttonText.color = selected ? this.SelectedColor : this.UnselectedColor;
		buttonText.fontStyle = selected ? FontStyle.Italic : FontStyle.Normal;

		this.currentSelected = buttonPanel;
	}
}
