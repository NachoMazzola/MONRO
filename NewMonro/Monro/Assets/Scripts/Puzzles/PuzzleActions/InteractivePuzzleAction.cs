using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractivePuzzleAction: PuzzleAction {

	public IMActionButtonType executesOnAction;

	public override void Execute() {
		if (puzzleGameObject == null) {
			return;
		}
		IMActionButton[] actions = this.GetComponents<IMActionButton>();
		bool matches = false;
		foreach (IMActionButton action in actions) {
			if (action.buttonType == executesOnAction) {
				matches = true;
				break;
			}
		}

		if (matches) {
			UpdateGPOState();
		}
	}
}
