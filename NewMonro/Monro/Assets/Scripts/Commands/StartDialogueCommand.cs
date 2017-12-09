using UnityEngine;
using System.Collections;
using Yarn.Unity;

public class StartDialogueCommand : ICommand {

	private DialogueRunner dialogueRunner;
	private DialogueUI dialogueUI;

	private bool isStarted = false;

	private ArrayList conversationParticipants;
	private string startingNode;

	public StartDialogueCommand(ArrayList conversationParticipants, string startNode) {
		this.conversationParticipants = conversationParticipants;
		this.startingNode = startNode;
	}

	public override void Prepare() {
		dialogueRunner = WorldObjectsHelper.getDialogueRunnerGO().GetComponent<DialogueRunner>();
		foreach (Transform t in this.conversationParticipants) {
			this.dialogueRunner.AddParticipant(t);	
		}
	}

	public override void WillStart() {
		foreach (Transform t in this.conversationParticipants) {
			if (t.gameObject.GetComponent<GameEntity>().type == GameEntity.GameEntityType.Player) { 
				//|| t.gameObject.GetComponent<GameEntity>().type == GameEntity.GameEntityType.NPC) {
				Player pl = t.gameObject.GetComponent<Player>();
				pl.animStateMachine.SetState (PlayerStateMachine.PlayerStates.PlayerTalk);
			}
		}
	}

	public override void UpdateCommand () {
		if (!this.isStarted) {
			this.dialogueRunner.StartDialogue(this.startingNode);
			this.isStarted = true;
		}

		this.finished = !this.dialogueRunner.isDialogueRunning;
	}

	public override bool Finished() {
		if (finished) {
			foreach (Transform t in this.conversationParticipants) {
				if (t.gameObject.GetComponent<GameEntity>().type == GameEntity.GameEntityType.Player) { 
					//|| t.gameObject.GetComponent<GameEntity>().type == GameEntity.GameEntityType.NPC) {
					Player pl = t.gameObject.GetComponent<Player>();
					pl.animStateMachine.SetState (PlayerStateMachine.PlayerStates.PlayerIdle);
				}
			}
		}

		return finished;
	}

}
