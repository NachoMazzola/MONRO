using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactionCreateItemInInventory : IPReaction {

	public string ItemIdToCreate;

	override public bool Execute (Transform actionReceiver, Puzzle puzzle, PAction theAction) {
		GameObject inv = GameObject.Find ("UI-Inventory");
		UIInventory uiInventory = inv.gameObject.GetComponent<UIInventory>();
		PlayerInventory inventory = inv.gameObject.GetComponent<PlayerInventory>();

		DBItem combined = DBAccess.getComponent().itemsDataBase.GetItemById(ItemIdToCreate);
		if (combined != null) {
			inventory.AddItem(combined);

			GameObject pPrefab = Resources.Load(combined.ItemInventoryPrefab) as GameObject;
			uiInventory.AddItemToInventory(pPrefab.transform);

			return true;
		}



//
//		DBItemLoader itemLoader = actionReceiver.GetComponent<DBItemLoader>();
//		DBItem it = itemLoader.itemModel;
//		DBItem itm = DBAccess.getComponent().itemsDataBase.GetItemById(theValue);
//		
//		if (it != null && itm != null) {
//			GameObject inv = GameObject.Find ("UI-Inventory");
//			UIInventory inventory = inv.GetComponent<UIInventory> ();
//			bool combinationResult = inventory.GetComponent<ItemCombinator> ().CombineItems (it, itm);
//			if (!combinationResult) {
//					
//			}
//		}
//
		return false;
	}
}
