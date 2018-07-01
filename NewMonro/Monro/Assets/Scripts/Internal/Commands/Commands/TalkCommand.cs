using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Yarn.Unity;

[System.Serializable]
public struct TalkCommandParameters: ICommandParamters {
	//public List<GameObject> conversationParticipants;
	public string startingNode;

	public int participantsCount;

	public CommandType GetCommandType() {
		return CommandType.TalkCommandType;
	}
}


public class TalkCommand : ICommand {

	public string startingNode;

	private List<GameObject> conversationParticipants;
	private DialogueRunner dialogueRunner;
	private DialogueUI dialogueUI;

	private bool isStarted = false;

	private MoveGameObjectCommand moveToCommand;

	public TalkCommand() {
		this.conversationParticipants = new List<GameObject>();
	}

	public TalkCommand(ICommandParamters parameters) {
		TalkCommandParameters t = (TalkCommandParameters)parameters;
		this.conversationParticipants = new List<GameObject>();
		this.startingNode = t.startingNode;
	}

	public TalkCommand(string startingNode) {
		this.startingNode = startingNode;
		this.conversationParticipants = new List<GameObject>();
	}

	public override void Prepare() {
		dialogueRunner = WorldObjectsHelper.getDialogueRunnerGO().GetComponent<DialogueRunner>();
		Talkable[] participants = GameObject.FindObjectsOfType<Talkable>();
		if (participants != null && participants.Length > 0) {
			foreach (Talkable t in participants) {
				this.dialogueRunner.AddParticipant(t);	
			}	
		}
	}

	public override void WillStart() {}

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
				Talkable talkable = t.GetComponent<Talkable>();
				if (talkable != null) {
					talkable.StopAnimation();
				}
			}
		}

		return finished;
	}

	public override CommandType GetCommandType() { 
		return CommandType.TalkCommandType; 
	}

}
