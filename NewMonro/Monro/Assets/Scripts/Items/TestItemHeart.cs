using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestItemHeart : DraggableWorldItem {
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public override void ItemIsOverObject(Transform other) {
		base.ItemIsOverObject(other);
	}

	public override void ItemIsNotOverObjectAnyMore(Transform other) {
		base.ItemIsNotOverObjectAnyMore(other);

//		if (other.gameObject.tag == "InteractiveObject") {
//			PuzzleResolver puzzleSolver = other.gameObject.GetComponent<PuzzleResolver>();
//			if (puzzleSolver != null) {
//				//do some shit
//				StopDragging();
//				itemModel.ItemHasBeenUsed = true;
//				PuzzleSolverManager.getComponent().ResolvePuzzle(puzzleSolver, itemModel, itemModel.resolvesPuzzleId);
//			}
//		}


	}
}
