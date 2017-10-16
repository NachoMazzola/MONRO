using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionCombineItems : PAction {

	private PuzzleActionType actionTrigger = PuzzleActionType.CombineItems;
	public string ItemId1;
	public string ItemId2;
	public bool RemoveItemsAfterCombination = false;

	//actionReceiver should be a uiinventoryItem

	override public void ExecuteAction(PuzzleActionType action, Transform actionReceiver = null, Dictionary<string, string> extraData = null) {
		if (action == actionTrigger && extraData != null) {
			DBItemLoader itemLoader = actionReceiver.GetComponent<DBItemLoader>();
			if (itemLoader != null) {
				string theValue = extraData["itemId"];
				DBItem it = itemLoader.itemModel;
				if ((it.ItemId == ItemId1 || it.ItemId == ItemId2) && (theValue == ItemId1 || theValue == ItemId2) && it.ItemId != theValue) {
					if (ExecuteAllReactions(actionReceiver)) {
						ActionFinished();

						if (RemoveItemsAfterCombination) {
							GameObject inv = GameObject.Find ("UI-Inventory");
							UIInventory uiInventory = inv.gameObject.GetComponent<UIInventory>();

							List<string> toRemoveList = new List<string>();
							toRemoveList.Add(ItemId1);
							toRemoveList.Add(ItemId2);

							uiInventory.RemoveItems(toRemoveList);

							uiInventory.GetComponent<PlayerInventory>().MarkItemsAsUsed(toRemoveList);
						}
					}
				}
				else {
					Debug.Log("WARNING: Item with Id: " + it.ItemId + " is not combinable with " + ItemId1 + " or " + ItemId2);
				}
			}
		}
	}
}