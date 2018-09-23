using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class CannotApplyTraitCommandCommand : ICommand {

	private TraitType traitType;
	private GameObject target;

	private TalkCommand talkCommand;

	public CannotApplyTraitCommandCommand(GameObject target, TraitType tType) {
		this.target = target;
		this.traitType = tType;
	}

	public override void Prepare() {
		DialogueRunner dialogueRunner = WorldObjectsHelper.getDialogueRunnerGO().GetComponent<DialogueRunner>();
		string targetId = this.target.GetComponent<GameEntity>().ID;
		string traitString = Trait.toString(this.traitType);
		string fullNode = traitString + "_default";
//		if (dialogueRunner.NodeExists(fullNode) == false) {
//			fullNode = traitString + "_default";
//		}
		this.talkCommand = new TalkCommand(fullNode);
		this.talkCommand.Prepare();
	}

	public override void WillStart() {
		this.talkCommand.WillStart();
	}

	public override void UpdateCommand () {
		this.talkCommand.UpdateCommand();
		this.finished = this.talkCommand.Finished();
	}

	public override bool Finished() {
		return finished;
	}

	public override CommandType GetCommandType() { 
		return CommandType.CannotApplyTraitCommandCommandType; 
	}
}
