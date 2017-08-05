using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionDropItemOverInteractiveObject : PAction {

	public Transform DropOverThis;
	public string ItemId;

	private PuzzleActionType actionTrigger = PuzzleActionType.DropItemOver;

	override public void ExecuteAction(PuzzleActionType action, Transform actionReceiver) {
		if (action == actionTrigger && actionReceiver == DropOverThis) {
			if (ExecuteAllReactions(actionReceiver)) {
				ActionFinished();
			}
		}
	}
}
