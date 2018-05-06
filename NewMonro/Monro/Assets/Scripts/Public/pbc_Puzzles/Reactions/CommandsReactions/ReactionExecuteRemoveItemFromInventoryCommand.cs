using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactionExecuteRemoveItemFromInventoryCommand : ExecuteCommandReaction {
	public RemoveItemFromInventoryCommandParameters parameters;

	void Awake() {
		this.theCommand = CommandFactory.CreateCommand(CommandType.RemoveItemFromInventoryCommandType, null, false, this.parameters);
	}
}
