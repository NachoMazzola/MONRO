using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactionAddLookaleTrait : IPReaction {

	public GameObject target;
	public string Caption;

	override public bool Execute (Transform actionReceiver, Puzzle puzzle, PAction theAction) {
		if (target.GetComponent<Lookable>() != null) {
			return false;
		}

		target.AddComponent<Lookable>();

		
		return true;
	}
}
