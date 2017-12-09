using UnityEngine;
using System.Collections;
using Yarn.Unity;

public class StartDialogueCommand : ICommand {

	private DialogueRunner dialogueRunner;
	private DialogueUI dialogueUI;

	public override void Prepare() {

		Player playerComp = WorldObjectsHelper.getPlayerGO().GetComponent<Player>();

		//dialogueRunner = FindObjectOfType<DialogueRunner>();
		//dialogueUI = dialogueRunner.dialogueUI as DialogueUI;

		//dialogueUI.dialogRunner.AddParticipant(playerComp.transform);
	}

	public override void UpdateCommand () {
		
	}

	public override bool Finished() {
		return finished;
	}

}
