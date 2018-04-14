using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactionDestroyObject : IPReaction {

	public string ItemIdToDestroy;

	override public bool Execute (Transform actionReceiver, Puzzle puzzle, PAction theAction) {
		Transform toDestroy = GetTransformFromId(ItemIdToDestroy);

		if (toDestroy != null) {
			GameObject.Destroy(toDestroy.gameObject);
			return true;
		}

		return false;
	}
}
