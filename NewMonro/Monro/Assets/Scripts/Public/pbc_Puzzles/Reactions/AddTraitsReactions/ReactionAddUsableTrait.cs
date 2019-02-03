using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactionAddUsableTrait : IPReaction {

	public GameObject target;

	override public bool Execute (Transform actionReceiver, Puzzle puzzle, PAction theAction) {
		if (target.AddComponent<Usable>() != null) {
			return false;
		}

		target.AddComponent<Usable>();
		if (target.GetComponent<VerbPanelHighlighter>() == null) {
			target.AddComponent<VerbPanelHighlighter>();	
		}

		return true;
	}
}

