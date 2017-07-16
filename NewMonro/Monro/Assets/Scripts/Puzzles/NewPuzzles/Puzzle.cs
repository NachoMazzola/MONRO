using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum PuzzleState {
	Disabled,
	Enabled, 
	Completed
}


public class Puzzle : MonoBehaviour {

	public PGOState puzzleState = PGOState.Disabled;
	public string puzzleId;
	public int maxSteps;
	public int currentStep;

	private PReaction[] puzzleReactions;

	// Use this for initialization
	void Start () {
		puzzleReactions = this.GetComponents<PReaction>();
	}


	public void UpdatePuzzleWithAction(IMActionButtonType action, Transform actionReceiver) {
		foreach (PReaction pr in puzzleReactions) {
			pr.ExecuteReaction(action, actionReceiver);
		}
	}

}
