using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactionExecuteChangeSpriteCommand : ExecuteCommandReaction {

	public ChangeSpriteCommandParamters parameters;

	void Awake() {
        this.theCommand = new ChangeSpriteCommand(this.parameters);
	}
}
