using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdatePuzzleStateAction : PReaction {

	public PuzzleState NewState;

	/*
	 * Optional list of aditional puzzle objects to be updated when this puzzle updates its state
	 * 
	*/
	public List<Transform> puzzlesToUpdate;

	override public void ExecuteReaction(IMActionButtonType action, Transform actionReceiver) {
		if (action == ActionTrigger && actionReceiver == ActionReceiver) {
			parent.puzzleState = NewState;

			foreach (Transform t in puzzlesToUpdate) {
				Puzzle p = t.GetComponent<Puzzle>();
				if (p) {
					p.puzzleState = NewState;
				}
			}

			if (IncrementSteps) {
				parent.IncrementPuzzleStep();	
			}
		}
	}

	override public void ActionFinished() {
		Executed = true;
	}
}
