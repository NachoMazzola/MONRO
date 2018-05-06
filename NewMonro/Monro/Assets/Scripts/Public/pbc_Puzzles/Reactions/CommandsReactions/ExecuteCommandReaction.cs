using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExecuteCommandReaction : IPReaction {

	protected ICommand theCommand;

	override public bool Execute (Transform actionReceiver, Puzzle puzzle, PAction theAction) {
		CommandManager.getComponent().QueueCommand(theCommand, true);
		return true;
	}
}
