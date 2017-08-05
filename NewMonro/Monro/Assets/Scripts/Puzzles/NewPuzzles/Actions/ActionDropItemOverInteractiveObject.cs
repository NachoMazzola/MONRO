using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionDropItemOverInteractiveObject : PAction {

	public Transform DropOverThis;
	public string ItemId;
	private const PuzzleActionType actionTrigger = PuzzleActionType.DropItemOver;

	override public void ExecuteAction(PuzzleActionType action, Transform actionReceiver = null, Dictionary<string, object> extraData = null) {
		if (action == actionTrigger && actionReceiver == DropOverThis && extraData != null) {
			if ((extraData["itemId"] as string) == ItemId) {
				if (ExecuteAllReactions(actionReceiver)) {
					ActionFinished();
				}	
			}
		}
	}
}
