using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * La comunicacion entre Puzzles, Steps y PuzzleManager tiene que ser asi:
 * Los Steps no se tienen que conocer entre si, la forma de comunicar algo es emitir un mensaje
 * que lo recibe el PuzzleManager y lo redistribuye a todos sus Puzzles y estos lo redistribuyen a sus Steps
 * 
 * Step -mensaje-> PuzzleManager --> Puzzles --> Steps
*/



public class ReactionObservePuzzlesStatesUpdate: MonoBehaviour, IPReaction, IPuzzleReactionObserver
{

	public PuzzleState StateToWatch;

	public List<Transform> puzzlesToWatch;

	private int puzzlesChecked = 0;
	private PAction theAction;

	public bool Execute (Transform actionReceiver, Puzzle puzzle, PAction theAction) {
		this.theAction = theAction;
		puzzle.AddObserver(this);
		return false;
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
			theAction.ActionFinished();
		}
	}
}
