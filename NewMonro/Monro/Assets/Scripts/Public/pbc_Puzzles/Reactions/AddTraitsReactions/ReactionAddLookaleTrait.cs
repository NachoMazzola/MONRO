﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactionAddLookaleTrait : IPReaction {

	public GameObject target;
	public string Caption;

	override public bool Execute (Transform actionReceiver, Puzzle puzzle, PAction theAction) {
		target.AddComponent<Lookable>();
		target.AddComponent<VerbPanelHighlighter>();

		return true;
	}
}
