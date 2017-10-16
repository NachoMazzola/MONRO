using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactionDestroyObject : IPReaction {

	public string ItemIdToDestroy;

	override public bool Execute (Transform actionReceiver, Puzzle puzzle, PAction theAction) {
		InteractiveObject[] intsObjs = GameObject.FindObjectsOfType<InteractiveObject>();
		GameObject toDestroy = null;
		foreach (InteractiveObject iObj in intsObjs) {
			if (iObj.Id == ItemIdToDestroy) {
				toDestroy = iObj.gameObject;
				break;
			}
		}

		if (toDestroy != null) {
			GameObject.Destroy(toDestroy);
			return true;
		}

		return false;
	}
}
