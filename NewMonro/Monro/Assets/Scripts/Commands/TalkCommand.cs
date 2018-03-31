using UnityEngine;
using System.Collections;
using Yarn.Unity;

public class TalkCommand : ICommand {

	private DialogueRunner dialogueRunner;
	private DialogueUI dialogueUI;

	private bool isStarted = false;

	public ArrayList conversationParticipants;
	public string startingNode;

	public TalkCommand() {
		this.conversationParticipants = new ArrayList();
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

	public override CommandType GetCommandType() { 
		return CommandType.TalkCommandType; 
	}

}
