﻿using System.Collections;
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
        if (this.lookable == null)
        {
            return;
        }
		this.lookableComponent = this.lookable.GetComponent<Lookable>();
		if (this.lookableComponent == null) {
			return;
		}

		this.talkCommand = new TalkCommand("lookAt_" + this.lookableComponent.gameEntity.ID);
        this.talkCommand.conversationParticipants.Add(this.lookable);
        this.talkCommand.conversationParticipants.Add(this.whoLooks);

        this.talkCommand.Prepare();
	}

	public override void WillStart() {
        this.isRunning = true;
		this.talkCommand.WillStart();
	}

	public override void UpdateCommand () {
        if (this.talkCommand == null) { return; }
		this.talkCommand.UpdateCommand();
		this.isRunning = !this.talkCommand.Finished();
	}

	public override CommandType GetCommandType() { 
		return CommandType.LookAtCommandType; 
	}
}
