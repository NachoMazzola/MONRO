using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct LookAtCommandParameters: ICommandParamters {
	public GameObject lookable;
	public GameObject whoLooks;

	public CommandType GetCommandType() {
		return CommandType.LookAtCommandType;
	}
}

/**
 * Look At basic command.
 * Triggers a caption
*/
public class LookAtCommand : ICommand {

	public GameObject lookable;
	public GameObject whoLooks;

	private Lookable lookableComponent;
	private TalkCommand talkCommand;


	public LookAtCommand() {
		
	}

	public LookAtCommand(ICommandParamters parameters) {
		LookAtCommandParameters l = (LookAtCommandParameters)parameters;
		this.lookable = l.lookable;
		this.whoLooks = l.whoLooks;
	}

	public LookAtCommand(GameObject lookable, GameObject whoLooks) {
		this.lookable = lookable;
		this.whoLooks = whoLooks;
	}

	public override void Prepare() {
		this.lookableComponent = this.lookable.GetComponent<Lookable>();
		if (this.lookableComponent == null) {
			return;
		}

		this.talkCommand = new TalkCommand("lookAt_"+this.lookableComponent.gameEntity.ID);
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
		return CommandType.LookAtCommandType; 
	}
}
