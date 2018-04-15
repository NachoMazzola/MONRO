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
		this.textBoxDisplayerComponent = this.whoLooks.GetComponent<TextboxDisplayer>();
	}

	public override void WillStart() {
		this.textBoxDisplayerComponent.lookable = this.lookableComponent;
		this.textBoxDisplayerComponent.ShowCaption();
	}

	public override void UpdateCommand () {
		this.finished = this.textBoxDisplayerComponent.hasFinishedCaptionDisplay;
	}

	public override bool Finished() {
		return finished;
	}

	public override CommandType GetCommandType() { 
		return CommandType.LookAtCommandType; 
	}
}
