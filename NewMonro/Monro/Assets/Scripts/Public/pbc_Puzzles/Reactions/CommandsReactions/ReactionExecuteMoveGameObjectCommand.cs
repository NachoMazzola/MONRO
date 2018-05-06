using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactionExecuteMoveGameObjectCommand : ExecuteCommandReaction {
	public MoveGameObjectCommandParameters parameters;

	void Awake() {
		this.theCommand = CommandFactory.CreateCommand(CommandType.MoveGameObjectCommandType, null, false, this.parameters);
	}
}
