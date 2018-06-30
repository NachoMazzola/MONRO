using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactionAddUsableTrait : IPReaction {

	public GameObject target;

	override public bool Execute (Transform actionReceiver, Puzzle puzzle, PAction theAction) {
		target.AddComponent<Usable>();
		target.AddComponent<VerbPanelHighlighter>();

		return true;
	}
}

