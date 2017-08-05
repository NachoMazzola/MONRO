using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionPuzzlesChangeState : PAction, IPuzzleReactionObserver {

	public List<Transform> puzzlesToWatch;
	public PuzzleState StateToWatch;

	private int puzzlesChecked = 0;


	override public void ExecuteAction(PuzzleActionType action, Transform actionReceiver) {}

	override public void SetPuzzleParent(Puzzle pParent) {
		base.SetPuzzleParent(pParent);
		parent.AddObserver(this);
	}

	//IPuzzleReactionObserver
	public void UpdatedState(Puzzle updatedPuzzle) {
		foreach (Transform t in puzzlesToWatch) {
			Puzzle p = t.GetComponent<Puzzle>();
			if (p != null) {
				if (p.puzzleId == updatedPuzzle.puzzleId && p.puzzleState == StateToWatch) {
					puzzlesChecked++;
				}		
			}
		}

		if (puzzlesChecked == puzzlesToWatch.Count) {
			if (ExecuteAllReactions(null)) {
				ActionFinished();
			}
		}
	}

}
