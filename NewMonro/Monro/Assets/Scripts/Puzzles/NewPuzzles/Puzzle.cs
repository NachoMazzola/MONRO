using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum PuzzleState {
	Disabled,
	Enabled, 
	Completed
}


public class Puzzle : MonoBehaviour {

	public PuzzleState puzzleState = PuzzleState.Disabled;
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
			pr.parent = this;
			pr.ExecuteReaction(action, actionReceiver);
		}
	}

	public void IncrementPuzzleStep() {
		currentStep++;
		if (currentStep == maxSteps) {
			puzzleState = PuzzleState.Completed;
		}
	}

}
