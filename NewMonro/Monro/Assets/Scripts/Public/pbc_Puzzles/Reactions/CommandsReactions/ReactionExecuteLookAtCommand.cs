using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactionExecuteLookAtCommand : ExecuteCommandReaction {

	public LookAtCommandParameters parameters;

	void Awake() {
        this.theCommand = new LookAtCommand(this.parameters);
	}
}
