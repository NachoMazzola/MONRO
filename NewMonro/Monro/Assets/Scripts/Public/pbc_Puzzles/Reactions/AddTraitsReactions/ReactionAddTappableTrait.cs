using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactionAddTappableTrait : IPReaction {

	public GameObject target;

	override public bool Execute (Transform actionReceiver, Puzzle puzzle, PAction theAction) {
		if (target.AddComponent<Tappable>() != null) {
			return false;
		}

		target.AddComponent<Tappable>();

		return true;
	}
}

