using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Look At basic command.
 * Triggers a caption within the TextboxDisplayer panel.
*/
public class LookAtCommand : ICommand {

	public Lookable lookable;
	public TextboxDisplayer whoLooks;

	public override void Prepare() {
		
	}

	public override void WillStart() {
		this.whoLooks.lookable = this.lookable;
		this.whoLooks.ShowCaption();
	}

	public override void UpdateCommand () {
		this.finished = this.whoLooks.hasFinishedCaptionDisplay;
	}

	public override bool Finished() {
		return finished;
	}

	public override CommandType GetCommandType() { 
		return CommandType.LookAtCommandType; 
	}
}
