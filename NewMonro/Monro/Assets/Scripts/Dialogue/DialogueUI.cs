using UnityEngine;
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



	void Awake ()
	{

		dialogRunner = FindObjectOfType<DialogueRunner> ();
		conversationOptionsPanel = WorldObjectsHelper.getUIGO().transform.Find("ConversationOptionsPanel").transform;
		conversationOptionsPanel.gameObject.SetActive (false);


		foreach (Button optionButton in optionButtons) {
			optionButton.gameObject.SetActive (false);
		}
	}
		
	void Start() {
		
	}

	private Talkable GetParticipant (string participant)
	{
		int dotIdx = participant.IndexOf (".");
		string participantCorrectName = participant.Substring (0, dotIdx);

		foreach (Transform t in dialogRunner.conversationParticipants) {
			Talkable conversationInterface = t.GetComponent<Talkable> ();
			if (conversationInterface.ConversationName == participantCorrectName) {
				return conversationInterface;
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

			if (whoIsTalking.GetComponent<GameEntity>().type != lastOneWhoTalked.GetComponent<GameEntity>().type) {
				TextboxDisplayer lastTbDisplayer = lastOneWhoTalked.GetComponent<TextboxDisplayer>();
				yield return lastTbDisplayer.HideCaption (0.0f);

				lastOneWhoTalked = whoIsTalking;
			}
				
			TextboxDisplayer tbDisplayer = whoIsTalking.GetComponent<TextboxDisplayer>();
			yield return StartCoroutine (tbDisplayer.ShowCaption (line.text, TextBox.DisappearMode.WaitInput));

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

		// Display each option in a button, and make it visible
		int i = 0;
		foreach (var optionString in optionsCollection.options) {
			optionButtons [i].gameObject.SetActive (true);
			optionButtons [i].GetComponentInChildren<Text> ().text = optionString;
			i++;
		}

		inactiveButtons = optionButtons.Count - optionsCollection.options.Count;

		if (inactiveButtons > 0) {
			float bHeight = optionButtons [0].GetComponent<RectTransform> ().rect.height;
			optionButtonYDisplacement = bHeight * inactiveButtons;
			for (int j = 0; j < optionButtons.Count; j++) {
				Button currentButton = optionButtons [j];
				RectTransform buttonRect = currentButton.GetComponent<RectTransform> ();

				buttonRect.anchoredPosition = new Vector2 (buttonRect.anchoredPosition.x, buttonRect.anchoredPosition.y - optionButtonYDisplacement);
			}
		}

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

		GameObject inventoryGO = WorldObjectsHelper.getUIInventoryGO();
		if (inventoryGO != null) {
			UIInventory inv = inventoryGO.GetComponent<UIInventory>();
			inv.CloseInventory();
		}

		WorldInteractionController wic = WorldInteractionController.getComponent();
		wic.enableInteractions = false;
		wic.InterruptInteractions();	

		// Enable the dialogue controls.

		yield break;
	}

	public override IEnumerator DialogueComplete ()
	{

		conversationOptionsPanel.gameObject.SetActive (false);
		dialogRunner.DialogueComplete ();
		resetDialogueOptionsButtons ();

		lastOneWhoTalked = null;

		TextboxDisplayer lastTbDisplayer = whoIsTalking.GetComponent<TextboxDisplayer>();
		yield return lastTbDisplayer.HideCaption(0.0f);
		whoIsTalking = null;
	
		WorldInteractionController.getComponent().enableInteractions = true;

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
