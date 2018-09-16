using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactionAddTappableTrait : IPReaction {

	public GameObject target;

	override public bool Execute (Transform actionReceiver, Puzzle puzzle, PAction theAction) {
		if (target.AddComponent<Tappable>() != null && target.GetComponent<VerbPanelHighlighter>() != null) {
			return false;
		}

		target.AddComponent<Tappable>();
		target.AddComponent<VerbPanelHighlighter>();

		return true;
	}
}

