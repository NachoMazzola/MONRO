using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleActionTracker {

	//dicionary that holds the interactive actions triggered on the interactive objects used as keys
	private Dictionary<Transform, List<PuzzleActionType>> actionsTriggered;

	public PuzzleActionTracker () {
		actionsTriggered = new Dictionary<Transform, List<PuzzleActionType>> ();
	}

	public void AddAction (PuzzleActionType action, Transform interactiveObject) {
		if (actionsTriggered.ContainsKey (interactiveObject)) {
			List<PuzzleActionType> actionsForObj = actionsTriggered [interactiveObject];
			if (actionsForObj != null && !actionsForObj.Contains(action)) {
				actionsForObj.Add (action);
			}
		} else {
			List<PuzzleActionType> newList = new List<PuzzleActionType> ();
			newList.Add (action);
			actionsTriggered.Add (interactiveObject, newList);
		}
	}

	public List<PuzzleActionType> GetTriggeredActionsFor (Transform t)
	{
		if (actionsTriggered.ContainsKey (t)) {
			return actionsTriggered [t];
		}
		return null;
	}
}
