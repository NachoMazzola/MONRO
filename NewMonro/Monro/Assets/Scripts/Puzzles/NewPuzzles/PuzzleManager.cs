using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour {

	private List<Puzzle> puzzleList;

	// Use this for initialization
	void Start () {
		puzzleList = new List<Puzzle>();
		for (int i = 0; i < this.transform.childCount; i++) {
			Puzzle aPuzzle = this.transform.GetChild(i).GetComponent<Puzzle>();
			puzzleList.Add(aPuzzle);
		}
	}

	public void UpdatePuzzlesWithAction(IMActionButtonType action, Transform actionReceiver) {
		foreach (Puzzle p in puzzleList) {
			p.UpdatePuzzleWithAction(action, actionReceiver);
		}
	}
}
