using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactionExecuteAnimateCharacterCommand : ExecuteCommandReaction {

	public AnimateCharacterCommandParameters parameters;

	void Awake() {
		this.theCommand = CommandFactory.CreateCommand (CommandType.AnimateCharacterCommandType, null, false, parameters);
	}
}
