using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleSolverManager : MonoBehaviour {

	private List<PuzzleResolver> observers = new List<PuzzleResolver>();

	static public PuzzleSolverManager getComponent () {
		return GameObject.Find ("PuzzleSolverManager").GetComponent<PuzzleSolverManager> ();
	}


	public void NotifyPuzzleResolved(DBItem item, string puzzleId) {
		foreach (PuzzleResolver pR in observers) {
			pR.PuzzleResolved(item);
		}
	}

	public void NotifyTriggerTriggered(string triggerId) {
		foreach (PuzzleResolver pR in observers) {
			//pR.NotifyTriggerTriggered(triggerId);
		}
	}

	public void NotifyPuzzleStateUpdate(string puzzleId, int puzzleStep) {
		foreach (PuzzleResolver pR in observers) {
			if (pR.puzzleId == puzzleId) {
				
			}
		}
	}


	public void AddPuzzleObserver(PuzzleResolver pObserver) {
		if (!observers.Contains(pObserver)) {
			observers.Add(pObserver);
		}
	}

	public void RemovePuzzleObserver(PuzzleResolver pObserver) {
		if (observers.Contains(pObserver)) {
			observers.Remove(pObserver);
		}
	}

	public void RemoveAll() {
		observers.RemoveRange(0, observers.Count-1);
	}


	public void ResolvePuzzle(PuzzleResolver interactiveObject, DBItem item, string puzzleId) {
		//resuelve el puzzle?
		if (interactiveObject.itemIds.Contains(item.ItemId) && interactiveObject.itemIds.Count == 1) {
			//hace algo
			interactiveObject.PuzzleResolved(item);
			NotifyPuzzleResolved(item, puzzleId);
		}
		else {
			//no lo resuelve pero esta relacionado con el puzzle
			if (interactiveObject.relatedItemsIds.Contains(item.ItemId)) {
				interactiveObject.PuzzleNotResolvedButItemIsRelated(item);
			}
			//no lo resuelve
			else {
				interactiveObject.PuzzleNotResolved(item);
			}
		}
	}
}
