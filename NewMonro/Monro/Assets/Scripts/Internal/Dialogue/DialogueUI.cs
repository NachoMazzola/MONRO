﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Text;
using Yarn.Unity;
using System.Collections.Generic;

public class DialogueUI : Yarn.Unity.DialogueUIBehaviour
{

	public DialogueRunner dialogRunner;

	// The buttons that let the user choose an option
	public List<Button> optionButtons;
	// A delegate (ie a function-stored-in-a-variable) that
	// we call to tell the dialogue system about what option
	// the user selected
	private Yarn.OptionChooser SetSelectedOption;

	[Tooltip ("How quickly to show the text, in seconds per character")]
	public float textSpeed = 0.025f;

	private float optionButtonYDisplacement;
	private int inactiveButtons;
	private Transform conversationOptionsPanel;

	private Talkable lastOneWhoTalked;
	private Talkable whoIsTalking;

	DialogueOptionsDisplayer buttonsPositionHandler;

	void Awake ()
	{

		dialogRunner = FindObjectOfType<DialogueRunner> ();
		conversationOptionsPanel = WorldObjectsHelper.getUIGO ().transform.Find ("ConversationOptionsPanel").transform;
		conversationOptionsPanel.gameObject.SetActive (false);


		foreach (Button optionButton in optionButtons) {
			optionButton.gameObject.SetActive (false);
		}

		this.buttonsPositionHandler = new DialogueOptionsDisplayer (this.optionButtons);
		this.buttonsPositionHandler.SetOriginPositions();
	}

	void Start ()
	{
		
	}

	public Talkable GetParticipant (string participant)
	{
		int dotIdx = participant.IndexOf (".");
		string participantCorrectName;

		//check if we are doing a Look At instead of a normal conversation
		if (dotIdx == -1) {
			dotIdx = participant.IndexOf("_");
			participantCorrectName = participant.Substring(dotIdx+1);
			/**
			 * OJO, CUANDO ES UN LOOK AT, EL NOMBRE DEL PARTICIPANTE DEBERIA SER MORNJIALL.
			 * PORQUE SINO, VA A HABLAR EL QUE SEA EL CONVERSATION NAME, Y SI POR EJEMPLO EN EL JSON
			 * DICE LOOKAT_HILDR, EL DIALOGO LO VA A DECIR HILDR.. TENEMOS QUE PENSAR UNA MANERA
			 * DE INDICAR EN EL JSON QUE ES UN LOOKAT A HILDR, PERO EL QUE HABLA ES MORNJIALL!!
			*/

			participantCorrectName = "Mornjiall";
		}
		else {
			participantCorrectName = participant.Substring (0, dotIdx);
		}

		foreach (Talkable t in dialogRunner.conversationParticipants) {
			if (t.gameEntity.ID == participantCorrectName) {
				return t;
			}
		}	

		Debug.Log ("Careful!!, there is no character named" + participantCorrectName);

		return null;
	}


	//YARN INTERFACE IMPLEMENTATION
	// Show a line of dialogue, gradually
	public override IEnumerator RunLine (Yarn.Line line)
	{
		whoIsTalking = GetParticipant (dialogRunner.dialogue.currentNode);
		//volver a pedir el conv canvas solo cuando el que habla cambio

		if (whoIsTalking != null) {
			if (lastOneWhoTalked == null) {
				lastOneWhoTalked = whoIsTalking;
			}

			if (whoIsTalking.GetComponent<GameEntity> ().type != lastOneWhoTalked.GetComponent<GameEntity> ().type) {
				TextboxDisplayer lastTbDisplayer = lastOneWhoTalked.GetComponent<TextboxDisplayer> ();
				yield return lastTbDisplayer.HideCaption (0.0f);
				lastOneWhoTalked = whoIsTalking;
			}


			TextboxDisplayer tbDisplayer = whoIsTalking.GetComponent<TextboxDisplayer> ();
			yield return tbDisplayer.ShowCaption (line.text, TextBox.DisappearMode.WaitInput);
		
			yield break;
		}
	}

	// Show a list of options, and wait for the player to make a selection.
	public override IEnumerator RunOptions (Yarn.Options optionsCollection, 
	                                        Yarn.OptionChooser optionChooser)
	{

		// Do a little bit of safety checking
		if (optionsCollection.options.Count > optionButtons.Count) {
			Debug.LogWarning ("There are more options to present than there are" +
			"buttons to present them in. This will cause problems.");
		}

		conversationOptionsPanel.gameObject.SetActive (true);

		this.buttonsPositionHandler.PositionateButtons (optionsCollection);

		// Record that we're using it
		SetSelectedOption = optionChooser;

		// Wait until the chooser has been used and then removed (see SetOption below)
		while (SetSelectedOption != null) {
			yield return null;
		}

		// Hide all the buttons
		foreach (var button in optionButtons) {
			button.gameObject.SetActive (false);
		}

		resetDialogueOptionsButtons ();
		yield break;
	}

	// Called by buttons to make a selection.
	public void SetOption (int selectedOption)
	{
		// Call the delegate to tell the dialogue system that we've
		// selected an option.
		SetSelectedOption (selectedOption);

		// Now remove the delegate so that the loop in RunOptions will exit
		SetSelectedOption = null; 
	}

	public override IEnumerator RunCommand (Yarn.Command command)
	{
		yield break;
	}

	public override IEnumerator DialogueStarted ()
	{

		Debug.Log ("Dialogue starting!");

		GameObject inventoryGO = WorldObjectsHelper.GetBottomPanelUIGO ();
		if (inventoryGO != null) {
			inventoryGO.SetActive (false);
		}

		WorldInteractionController wic = WorldInteractionController.getComponent ();
		wic.enableInteractions = false;
		wic.InterruptInteractions ();


		// Enable the dialogue controls.

		yield break;
	}

	public override IEnumerator DialogueComplete ()
	{

		conversationOptionsPanel.gameObject.SetActive (false);
		dialogRunner.DialogueComplete ();
		resetDialogueOptionsButtons ();

		lastOneWhoTalked = null;

		TextboxDisplayer lastTbDisplayer = whoIsTalking.GetComponent<TextboxDisplayer> ();
		yield return lastTbDisplayer.HideCaption (0.0f);
		whoIsTalking = null;
	
		WorldInteractionController.getComponent ().enableInteractions = true;

		GameObject inventoryGO = WorldObjectsHelper.GetBottomPanelUIGO ();
		if (inventoryGO != null) {
			inventoryGO.SetActive (true);
			WorldObjectsHelper.VerbsPanelUIGO ().GetComponent<VerbsButtonPanelHandler> ().ResetButtons ();	
		}


		yield break;
	}

	void resetDialogueOptionsButtons ()
	{
		//set the buttons as they were before displacing them!
		if (inactiveButtons > 0) {
			float bHeight = optionButtons [0].GetComponent<RectTransform> ().rect.height;
			optionButtonYDisplacement = bHeight * inactiveButtons;
			for (int j = 0; j < optionButtons.Count; j++) {
				Button currentButton = optionButtons [j];
				RectTransform buttonRect = currentButton.GetComponent<RectTransform> ();

				buttonRect.anchoredPosition = new Vector2 (buttonRect.anchoredPosition.x, buttonRect.anchoredPosition.y + optionButtonYDisplacement);
			}
		}
	}

}
