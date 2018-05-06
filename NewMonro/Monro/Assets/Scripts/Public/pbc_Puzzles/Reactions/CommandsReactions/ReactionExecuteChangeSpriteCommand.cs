using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactionExecuteChangeSpriteCommand : ExecuteCommandReaction {

	public ChangeSpriteCommandParamters parameters;

	void Awake() {
		this.theCommand = CommandFactory.CreateCommand(CommandType.ChangeSpriteCommandType, null, false, this.parameters);
	}
}
