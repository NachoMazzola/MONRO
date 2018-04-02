using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Yarn.Unity;

public struct TalkCommandParameters: ICommandParamters {
	public List<GameObject> conversationParticipants;
	public string startingNode;

	public int participantsCount;

	public CommandType GetCommandType() {
		return CommandType.TalkCommandType;
	}
}


public class TalkCommand : ICommand {

	public List<GameObject> conversationParticipants;
	public string startingNode;

	private DialogueRunner dialogueRunner;
	private DialogueUI dialogueUI;

	private bool isStarted = false;

	private MoveGameObjectCommand moveToCommand;

	public TalkCommand() {
		this.conversationParticipants = new List<GameObject>();
	}

	public TalkCommand(ICommandParamters parameters) {
		TalkCommandParameters t = (TalkCommandParameters)parameters;
		this.conversationParticipants = t.conversationParticipants;
		this.startingNode = t.startingNode;
	}

	public TalkCommand(List<GameObject> conversationParticipants, string startingNode) {
		this.conversationParticipants = conversationParticipants;
		this.startingNode = startingNode;
	}

	public override void Prepare() {
		dialogueRunner = WorldObjectsHelper.getDialogueRunnerGO().GetComponent<DialogueRunner>();
		foreach (GameObject t in this.conversationParticipants) {
			this.dialogueRunner.AddParticipant(t.transform);	
		}
	}

	public override void WillStart() {
		foreach (GameObject t in this.conversationParticipants) {
			if (t.GetComponent<GameEntity>().type == GameEntity.GameEntityType.Player) { 
				//|| t.gameObject.GetComponent<GameEntity>().type == GameEntity.GameEntityType.NPC) {
				Player pl = t.GetComponent<Player>();
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
			foreach (GameObject t in this.conversationParticipants) {
				if (t.GetComponent<GameEntity>().type == GameEntity.GameEntityType.Player) { 
					//|| t.gameObject.GetComponent<GameEntity>().type == GameEntity.GameEntityType.NPC) {
					Player pl = t.GetComponent<Player>();
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
