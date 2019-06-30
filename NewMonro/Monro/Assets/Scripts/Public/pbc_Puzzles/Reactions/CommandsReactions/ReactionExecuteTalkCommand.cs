using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactionExecuteTalkCommand : ExecuteCommandReaction {
	public TalkCommandParameters parameters;

	void Awake() {
        this.theCommand = new TalkCommand(this.parameters);
	}
}
