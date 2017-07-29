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

	private List<PAction> puzzleActions;
	private List<IPuzzleReactionObserver> internalObservers;

	// Use this for initialization
	void Start () {
		puzzleActions = new List<PAction>();
		for (int i = 0; i < this.transform.childCount; i++) {
			Transform child = this.transform.GetChild(i);
			PAction action = child.GetComponent<PAction>();

			puzzleActions.Add(action);
		}

		internalObservers = new List<IPuzzleReactionObserver>();
	}
		
	public void UpdatePuzzleWithAction(PuzzleActionType action, Transform actionReceiver) {
		foreach (PAction pr in puzzleActions) {
			pr.parent = this;
			pr.ExecuteAction(action, actionReceiver);
		}
	}

	public void IncrementPuzzleStep() {
		currentStep++;
		if (currentStep == maxSteps) {
			puzzleState = PuzzleState.Completed;
		}
	}

	public void AddObserver(IPuzzleReactionObserver obs) {
		internalObservers.Add(obs);
	}

	public void RemoveObserver(IPuzzleReactionObserver obs) {
		internalObservers.Remove(obs);
	}

	public bool IsObserver(IPuzzleReactionObserver obs) {
		return internalObservers.Contains(obs);
	}

	public void UpdatePuzzleState(Puzzle puzzle) {
		foreach (IPuzzleReactionObserver obs in internalObservers) {
			obs.UpdatedState(puzzle);
		}
	}
}

