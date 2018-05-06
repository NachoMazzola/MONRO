using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactionUpdatePuzzleState : IPReaction {

	public Transform PuzzleToWatch;
	public PuzzleState NewState;

	override public bool Execute (Transform actionReceiver, Puzzle puzzle, PAction theAction) {
		Puzzle wachedPuzzle = PuzzleToWatch.GetComponent<Puzzle> ();
		if (wachedPuzzle) {
			wachedPuzzle.puzzleState = NewState;

			this.transform.parent.parent.gameObject.BroadcastMessage("UpdatePuzzleState", puzzle);

			return true;
		}

		return false;
	}
}
