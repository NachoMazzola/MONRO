using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleNotificationManager : MonoBehaviour {

	private List<PuzzleNotificationObserver> observers = new List<PuzzleNotificationObserver>();

	static public PuzzleNotificationManager getComponent () {
		return GameObject.Find ("PuzzleNotificationManager").GetComponent<PuzzleNotificationManager> ();
	}

	void Awake() {
		GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>() ;
		foreach(GameObject go in allObjects) {
			if (go.activeInHierarchy) {
				PuzzleNotificationObserver obs = go.GetComponent<PuzzleNotificationObserver>();
				if (obs != null) {
					AddPuzzleObserver(obs);
				}
			}
		}
	}

	public void AddPuzzleObserver(PuzzleNotificationObserver pObserver) {
		if (!observers.Contains(pObserver)) {
			observers.Add(pObserver);
		}
	}

	public void RemovePuzzleObserver(PuzzleNotificationObserver pObserver) {
		if (observers.Contains(pObserver)) {
			observers.Remove(pObserver);
		}
	}

	public void RemoveAll() {
		observers.RemoveRange(0, observers.Count-1);
	}


	public void NotifyPGOChangedState(PGOState newState, PGOState lastState, string pgoId) {
		foreach (PuzzleNotificationObserver obs in observers) {
			obs.ExecuteReactionForGPOStateChange(newState, lastState, pgoId);
		}
	}
}
