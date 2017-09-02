using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionTriggerInteractiveAction : PAction {

	public string ActionReceiverId;
	public PuzzleActionType ActionTrigger = PuzzleActionType.None;

	override public void ExecuteAction(PuzzleActionType action, Transform actionReceiver = null, Dictionary<string, object> extraData = null) {
		Transform dOverTransform = GetTransformFromId(ActionReceiverId);
		if (action == ActionTrigger && actionReceiver == dOverTransform) {
			if (ExecuteAllReactions(actionReceiver)) {
				ActionFinished();
			}
		}
	}
}
