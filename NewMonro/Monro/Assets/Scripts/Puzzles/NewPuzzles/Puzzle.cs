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
	public int currentStep = 0;

	private List<PAction> puzzleActions;
	private List<IPuzzleReactionObserver> internalObservers;

	// Use this for initialization
	void Start () {
		internalObservers = new List<IPuzzleReactionObserver>();
		puzzleActions = new List<PAction>();
		for (int i = 0; i < this.transform.childCount; i++) {
			Transform child = this.transform.GetChild(i);
			PAction action = child.GetComponent<PAction>();
			action.SetPuzzleParent(this);
			puzzleActions.Add(action);
		}
	}
		
	public void UpdatePuzzleWithAction(PuzzleActionType action, Transform actionReceiver = null, Dictionary<string, object> extraData = null) {
		foreach (PAction pr in puzzleActions) {
			pr.ExecuteAction(action, actionReceiver, extraData);
		}
	}

	public void IncrementPuzzleStep() {
		currentStep++;
		if (currentStep == 1) {
			puzzleState = PuzzleState.Enabled;
		}
		else if (currentStep == maxSteps) {
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

