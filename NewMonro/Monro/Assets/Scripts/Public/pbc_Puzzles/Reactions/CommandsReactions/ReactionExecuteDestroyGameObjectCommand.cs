using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactionExecuteDestroyGameObjectCommand : ExecuteCommandReaction {

	public DestroyGameObjectCommandParameters parameters;

	void Awake() {
        this.theCommand = new DestroyGameObjectCommand(this.parameters);
	}
}
