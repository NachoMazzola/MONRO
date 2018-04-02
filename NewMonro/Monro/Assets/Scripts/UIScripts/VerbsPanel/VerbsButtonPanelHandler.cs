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

	void Awake() {
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
		List<CommandType> commands = new List<CommandType>();
		commands.Add(CommandType.LookAtCommandType);
		WorldInteractionController.getComponent().commandQueue = commands;
	}

	public void CreateTalkToCommand() {
		if (this.currentSelected == this.talkToButton) {
			this.ResetButtons();
			return;
		}

		this.SetButtonAsSelected(this.talkToButton, true);

		List<CommandType> commands = new List<CommandType>();
		commands.Add(CommandType.MoveGameObjectCommandType);
		commands.Add(CommandType.TalkCommandType);
		WorldInteractionController.getComponent().commandQueue = commands;
	}

	public void CreatePickUpCommand() {
		if (this.currentSelected == this.pickUpButton) {
			this.ResetButtons();
			return;
		}

		this.SetButtonAsSelected(this.pickUpButton, true);

		List<CommandType> commands = new List<CommandType>();
		commands.Add(CommandType.MoveGameObjectCommandType);
		commands.Add(CommandType.PutItemInInventoryCommandType);
		WorldInteractionController.getComponent().commandQueue = commands;
	}

	public void CreateUseCommand() {
		//????
	}

	public void ResetButtons() {
		WorldInteractionController.getComponent().commandQueue = new List<CommandType>();
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
