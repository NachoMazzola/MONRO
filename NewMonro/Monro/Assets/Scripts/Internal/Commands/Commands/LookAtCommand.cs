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
 * Triggers a caption within the TextboxDisplayer panel.
*/
public class LookAtCommand : ICommand {

	public GameObject lookable;
	public GameObject whoLooks;

	private Lookable lookableComponent;
	private TextboxDisplayer textBoxDisplayerComponent;


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
		//this.textBoxDisplayerComponent = this.whoLooks.GetComponent<TextboxDisplayer>();

		this.talkCommand = new TalkCommand("lookAt_"+this.lookableComponent.gameEntity.ID);
		this.talkCommand.Prepare();
	}

	public override void WillStart() {
		this.talkCommand.WillStart();
//		this.textBoxDisplayerComponent.lookable = this.lookableComponent;
//		if (!this.textBoxDisplayerComponent.ShowCaption()) {
//			Debug.Log("WARNING! LOOK AT ABORTED, " + this.whoLooks + " IS NOT LOOkABLE");
//			this.finished = true;
//		}
	}

	public override void UpdateCommand () {
		this.talkCommand.UpdateCommand();
		this.finished = this.talkCommand.Finished();
		//this.finished = this.textBoxDisplayerComponent.hasFinishedCaptionDisplay;
	}

	public override bool Finished() {
		return finished;
	}

	public override CommandType GetCommandType() { 
		return CommandType.LookAtCommandType; 
	}
}
