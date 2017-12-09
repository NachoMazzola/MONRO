using UnityEngine;
using System.Collections;
using Yarn.Unity;

public class StartDialogueCommand : ICommand {

	private DialogueRunner dialogueRunner;
	private DialogueUI dialogueUI;

	private bool isStarted = false;

	public override void Prepare() {

		Player playerComp = WorldObjectsHelper.getPlayerGO().GetComponent<Player>();

		dialogueRunner = WorldObjectsHelper.getDialogueRunnerGO().GetComponent<DialogueRunner>();
		this.dialogueRunner.AddParticipant(playerComp.transform);
	}

	public override void UpdateCommand () {
		if (!this.isStarted) {
			this.dialogueRunner.StartDialogue("Monrjiall.Start22");
			this.isStarted = true;
		}

		this.finished = !this.dialogueRunner.isDialogueRunning;
	}

	public override bool Finished() {
		return finished;
	}

}
