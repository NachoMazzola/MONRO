using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPuzzleBox : PuzzleResolver {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public override void PuzzleResolved(Item item) {
		if (item.ItemHasBeenUsed == false) {
			GameObject.Find("ValkyriePrefab2 (1)").SetActive(false);
			item.ItemHasBeenUsed = true;	
		}
	}

	public override void PuzzleStateChanged(int newStep) {
		
	}

	public override void PuzzleNotResolvedButItemIsRelated(Item item) {
		
	}

	public override void PuzzleNotResolved(Item item) {
		
	}


	void OnTriggerEnter2D (Collider2D other) {
		DraggableWorldItem item = other.transform.parent.gameObject.GetComponent<DraggableWorldItem> ();
		PuzzleSolverManager.getComponent().ResolvePuzzle(this, item.itemModel, puzzleId);
	}

	void OnTriggerExit2D (Collider2D other) {
		
	}
}
