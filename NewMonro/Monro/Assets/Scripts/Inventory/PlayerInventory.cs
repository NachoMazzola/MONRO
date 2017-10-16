using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


public class PlayerInventory : MonoBehaviour {

	private List<DBItem> items;

	void Awake() {
		items = new List<DBItem>();
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void LoadUpItemsFromDataBase() {
		DBAccess dataBase = DBAccess.getComponent();
		if (dataBase) {
			

		}

	}

	public void AddItem(DBItem theItem) {
		items.Add(theItem);
		DBAccess dataBase = DBAccess.getComponent();
		dataBase.storedItemsDataBase.StoreItemWithId(theItem.ItemId);
	}

	public void RemoveItem(string itemId) {
		DBAccess dataBase = DBAccess.getComponent();
		dataBase.storedItemsDataBase.RemoveItemWithId(itemId);
	}

	public void AddItemById(string itemId) {
		DBItem itm = DBAccess.getComponent().itemsDataBase.GetItemById(itemId);
		if (itm != null) {
			AddItem(itm);
		}
	}

}
