using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCombinator : MonoBehaviour {

	private ItemsDataService dataBase;
	private PlayerInventory inventory;
	private UIInventory uiInventory;

	// Use this for initialization
	void Start () {
		dataBase = DBAccess.getComponent().itemsDataBase;
		uiInventory = this.gameObject.GetComponent<UIInventory>();
		inventory = this.gameObject.GetComponent<PlayerInventory>();
	}

	public bool CombineItems(DBItem rhItem, DBItem lhItem) {
		DBItem combined = dataBase.GetCombinedItem(rhItem.ItemId, lhItem.ItemId);
		if (combined != null) {
			inventory.AddItem(combined);
			GameObject pPrefab = Resources.Load(combined.ItemPrefab) as GameObject;
			uiInventory.AddItemToInventory(pPrefab.transform);

			return true;
		}

//		Destroy(rhItem.gameObject);
//		Destroy(lhItem.gameObject);
//
		return false;
	}
}
