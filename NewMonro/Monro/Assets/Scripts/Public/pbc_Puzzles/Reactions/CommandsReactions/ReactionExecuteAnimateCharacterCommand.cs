using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactionExecuteAnimateCharacterCommand : ExecuteCommandReaction {

	public AnimateCharacterCommandParameters parameters;

	void Awake() {
        this.theCommand = new AnimateCharacterCommand(this.parameters);
	}
}
