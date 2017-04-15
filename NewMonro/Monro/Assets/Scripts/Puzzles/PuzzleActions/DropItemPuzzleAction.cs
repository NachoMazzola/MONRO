using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItemPuzzleAction : PuzzleAction {

	public string droppableItemId;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public override void Execute() {
		UpdateGPOState();
	}

	void OnTriggerEnter2D (Collider2D other) {
		WorldItem item = other.transform.parent.gameObject.GetComponent<WorldItem> ();
		if (item != null && item.itemModel.itemId == droppableItemId) {
			Execute();
		}
	}

	void OnTriggerExit2D (Collider2D other) {
		
	}
}
