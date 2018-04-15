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
		//TODO: UNCOMMENT THIS WHEN WE TAKE A LOOK AT SERIALIZATION!
		//this.LoadUpItemsFromDataBase();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void LoadUpItemsFromDataBase() {
		//TODO: UPDATE THIS TO USE ADD ITEM TO INVENTORY COMMAND

		GameObject inv = WorldObjectsHelper.getUIInventoryGO();
		UIInventory uiInventory = inv.gameObject.GetComponent<UIInventory>();

		DBAccess dataBase = DBAccess.getComponent();
		if (dataBase) {
			var enume = dataBase.itemsDataBase.GetStoredItems();
			foreach (DBStoredItem d in enume) {
				if (d.Used == false) {
					DBItem it = dataBase.itemsDataBase.GetItemById(d.ItemId);
					GameObject pPrefab = Resources.Load(it.ItemInventoryPrefab) as GameObject;
					uiInventory.AddItemToInventory(pPrefab.transform);
				}
			}
		}
	}

	public void AddItem(DBItem theItem) {
		items.Add(theItem);
		DBAccess dataBase = DBAccess.getComponent();
		dataBase.itemsDataBase.StoreItemWithId(theItem.ItemId);
	}

	public void MarkItemsAsUsed(List<string> itemIds) {
		DBAccess dataBase = DBAccess.getComponent();
		foreach (string id in itemIds) {
			dataBase.itemsDataBase.MarkItemAsUsed(id);	
		}
	}

	public void AddItemById(string itemId) {
		DBItem itm = DBAccess.getComponent().itemsDataBase.GetItemById(itemId);
		if (itm != null) {
			AddItem(itm);
		}
	}
}
