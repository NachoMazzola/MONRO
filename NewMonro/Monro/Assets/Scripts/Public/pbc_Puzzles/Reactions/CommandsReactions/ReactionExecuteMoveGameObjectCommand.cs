using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactionExecuteMoveGameObjectCommand : ExecuteCommandReaction {
	public MoveGameObjectCommandParameters parameters;

	void Awake() {
        this.theCommand = new MoveGameObjectCommand(this.parameters);
	}
}
