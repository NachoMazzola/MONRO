using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactionExecuteLookAtCommand : ExecuteCommandReaction {

	public LookAtCommandParameters parameters;

	void Awake() {
		this.theCommand = CommandFactory.CreateCommand(CommandType.LookAtCommandType, null, false, this.parameters);
	}
}
