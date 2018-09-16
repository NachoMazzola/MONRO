using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactionAddItemDroppableTrait : IPReaction {

	public GameObject target;
	public Transform InventroyItem;

	override public bool Execute (Transform actionReceiver, Puzzle puzzle, PAction theAction) {
		if (target.AddComponent<ItemDroppable>() != null && target.GetComponent<VerbPanelHighlighter>() != null) {
			return false;
		}

		target.AddComponent<ItemDroppable>();
		target.AddComponent<VerbPanelHighlighter>();

		ItemDroppable droppable =  target.GetComponent<ItemDroppable>();
		droppable.InventroyItem = this.InventroyItem;
			
		return true;
	}
}

