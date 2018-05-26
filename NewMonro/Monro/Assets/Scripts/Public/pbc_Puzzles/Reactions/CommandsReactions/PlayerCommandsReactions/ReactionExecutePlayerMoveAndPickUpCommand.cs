using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactionExecutePlayerMoveAndPickUpCommand : ExecuteCommandReaction {
	public GameObject ItemDroppable;

	void Awake() {
		this.theCommand = CommandFactory.CreateCommand(CommandType.PlayerMoveAndPickUpCommandType, this.ItemDroppable, false, null);
	}
}
