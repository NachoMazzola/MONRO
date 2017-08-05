﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionTriggerInteractiveAction : PAction {

	public Transform ActionReceiver;
	public PuzzleActionType ActionTrigger = PuzzleActionType.None;

	override public void ExecuteAction(PuzzleActionType action, Transform actionReceiver) {
		if (action == ActionTrigger && actionReceiver == ActionReceiver) {
			if (ExecuteAllReactions(actionReceiver)) {
				ActionFinished();
			}
		}
	}
}