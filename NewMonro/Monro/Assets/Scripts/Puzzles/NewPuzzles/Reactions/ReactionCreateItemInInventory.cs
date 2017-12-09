using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactionCreateItemInInventory : IPReaction {

	public string ItemIdToCreate;

	override public bool Execute (Transform actionReceiver, Puzzle puzzle, PAction theAction) {
		GameObject inv = WorldObjectsHelper.getUIInventoryGO();
		UIInventory uiInventory = inv.gameObject.GetComponent<UIInventory>();
		PlayerInventory inventory = inv.gameObject.GetComponent<PlayerInventory>();

		DBItem combined = DBAccess.getComponent().itemsDataBase.GetItemById(ItemIdToCreate);
		if (combined != null) {
			inventory.AddItem(combined);

			GameObject pPrefab = Resources.Load(combined.ItemInventoryPrefab) as GameObject;
			uiInventory.AddItemToInventory(pPrefab.transform);

			return true;
		}
		return false;
	}
}
