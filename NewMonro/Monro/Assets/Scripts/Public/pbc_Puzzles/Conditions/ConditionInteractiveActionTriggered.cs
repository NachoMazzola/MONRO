using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionInteractiveActionTriggered : MonoBehaviour, IPCondition {

	public List<Transform> interactiveObjects;
	public PuzzleActionType actionToTrigger = PuzzleActionType.None;


	public bool ConditionApplies(Puzzle inPuzzle) {
		PuzzleActionTracker tracker = inPuzzle.actionTracker;
		foreach (Transform t in interactiveObjects) {
			List<PuzzleActionType> actionsTriggeredInObj = tracker.GetTriggeredActionsFor(t);
			if (actionsTriggeredInObj != null && actionsTriggeredInObj.Contains(actionToTrigger)) {
				return true;
			}
		}

		return false;
	}
}
