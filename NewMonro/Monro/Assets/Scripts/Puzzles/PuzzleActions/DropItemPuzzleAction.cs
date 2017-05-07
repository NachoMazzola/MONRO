using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItemPuzzleAction : PuzzleAction {

	public string droppableItemId;

	[HideInInspector]
	public DraggableWorldItem worldItem;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public override bool Execute() {
		if (worldItem != null && worldItem.itemModel.itemId == droppableItemId) {
			UpdateGPOState();

			return true;
		}

		return false;
	}

}
