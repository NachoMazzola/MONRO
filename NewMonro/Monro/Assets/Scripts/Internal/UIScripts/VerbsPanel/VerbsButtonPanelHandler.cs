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
	public string LookAtLabel = "Look At";
	public string TalkToLabel = "Talk To";
	public string PickUpLabel = "Pick Up";
	public string UseLabel = "Use";


	private Transform lookAtButtonPanel;
	private Transform talkToButtonPanel;
	private Transform pickUpButtonPanel;
	private Transform useButtonPanel;

	private Transform currentSelected;

	private PlayerCommandBuilder uiCommandBuilder;

	void Awake() {
		this.uiCommandBuilder = new PlayerCommandBuilder();
		this.uiCommandBuilder.uiType = UIType.VerbsPanel;

		this.ResetButtons();

		this.lookAtButtonPanel = this.transform.Find("Look At");
		this.talkToButtonPanel = this.transform.Find("Talk To");
		this.pickUpButtonPanel = this.transform.Find("Pick Up");
		this.useButtonPanel = this.transform.Find("Use");

		this.SetLabels();
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


	public void HighlightVerb(TraitType type, bool highlight) {
		Transform theTransform = null;
		switch (type) {
		case TraitType.LookAt:
			theTransform = this.lookAtButtonPanel;
			break;
		case TraitType.Talk:
			theTransform = this.talkToButtonPanel;
			break;
		case TraitType.Pickup:
			theTransform = this.pickUpButtonPanel;
			break;
		case TraitType.Use:
			theTransform = this.useButtonPanel;
			break;
		default:
			break;
		}

		theTransform.GetComponentInChildren<Text>().color = highlight ? Color.yellow : Color.black;
	}

	private void SetLabels() {
		this.lookAtButtonPanel.GetComponentInChildren<Text>().text = this.LookAtLabel;
		this.talkToButtonPanel.GetComponentInChildren<Text>().text = this.TalkToLabel;
		this.pickUpButtonPanel.GetComponentInChildren<Text>().text = this.PickUpLabel;
		this.useButtonPanel.GetComponentInChildren<Text>().text = this.UseLabel;
	}

	private void SetButtonAsSelected(Transform buttonPanel, bool selected) {
		this.ResetButtons();

		Text buttonText = buttonPanel.GetComponentInChildren<Text>();
		buttonText.color = selected ? this.SelectedColor : this.UnselectedColor;
		buttonText.fontStyle = selected ? FontStyle.Italic : FontStyle.Normal;

		this.currentSelected = buttonPanel;
	}
}
