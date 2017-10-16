using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionDropItemOverInteractiveObject : PAction {

	public string DropOverThis;
	public bool RemovesItemFromInv = false;
	public string ItemId;
	private const PuzzleActionType actionTrigger = PuzzleActionType.DropItemOver;


	override public void ExecuteAction(PuzzleActionType action, Transform actionReceiver = null, Dictionary<string, string> extraData = null) {
		Transform dOverTransform = GetTransformFromId(DropOverThis);
		if (action == actionTrigger && actionReceiver == dOverTransform && extraData != null) {
			string theValue = extraData["itemId"];
			if (theValue == ItemId) {
				if (ExecuteAllReactions(actionReceiver)) {
					if (RemovesItemFromInv) {
						//remove item from inventory!!!!
						UIInventory theInventory = GameObject.Find("UI-Inventory").GetComponent<UIInventory>();
						List<string> toRemove = new List<string>();
						toRemove.Add(ItemId);
						theInventory.RemoveItems(toRemove);
					}
					ActionFinished();
				}	
			}
		}
	}
}
