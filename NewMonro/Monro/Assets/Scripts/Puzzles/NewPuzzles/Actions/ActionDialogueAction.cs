using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionDialogueAction : PAction {

	public string ActionName;

	override public void ExecuteAction(PuzzleActionType action, Transform actionReceiver = null, Dictionary<string, object> extraData = null) {
		if (ExecuteAllReactions(actionReceiver)) {
			ActionFinished();
		}
	}
}

