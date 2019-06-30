using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactionExecuteMoveCameraCommand : ExecuteCommandReaction {

	public MoveCameraCommandParameters parameters;

	void Awake() {
        this.theCommand = new MoveCameraCommand(this.parameters);
	}
}
