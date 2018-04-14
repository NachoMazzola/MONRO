using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactionExecuteDestroyGameObjectCommand : ExecuteCommandReaction {

	public DestroyGameObjectCommandParameters parameters;

	void Awake() {
		this.theCommand = CommandFactory.CreateCommand(CommandType.DestroyGameObjectCommandType, null, false, this.parameters);
	}
}
