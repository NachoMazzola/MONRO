using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactionCreateItemInInventory : IPReaction {

	public string ItemIdToCreate;

	override public bool Execute (Transform actionReceiver, Puzzle puzzle, PAction theAction) {
		PutItemInInventoryCommand putInInventory = new PutItemInInventoryCommand(ItemIdToCreate);
		putInInventory.WillStart();

		return putInInventory.Finished();
	}
}
