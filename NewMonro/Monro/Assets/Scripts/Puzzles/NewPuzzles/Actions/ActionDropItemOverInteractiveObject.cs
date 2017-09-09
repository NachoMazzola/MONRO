using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionDropItemOverInteractiveObject : PAction {

	public string DropOverThis;
	public string ItemId;
	private const PuzzleActionType actionTrigger = PuzzleActionType.DropItemOver;

	override public void ExecuteAction(PuzzleActionType action, Transform actionReceiver = null, Dictionary<string, string> extraData = null) {
		Transform dOverTransform = GetTransformFromId(DropOverThis);
		if (action == actionTrigger && actionReceiver == dOverTransform && extraData != null) {
			string theValue = extraData["itemId"];
			if (theValue == ItemId) {
				if (ExecuteAllReactions(actionReceiver)) {
					ActionFinished();
				}	
			}
		}
	}
}
