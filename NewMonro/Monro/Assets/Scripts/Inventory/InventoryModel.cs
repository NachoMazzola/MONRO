using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//THIS CLASS SHOULD HANDLE LOGIC RELATED TO INVENTORY MANAGMENT, LIKE SERIALIZING ITS DATA AND STUFF
//WHEN WE IMPLEMENT SERIALIZING, THIS CLASS WILL HAVE MUCH MORE SENSE
public class InventoryModel {

	private List<DBItem> items;

	public InventoryModel() {
		items = new List<DBItem>();
	}

	public void AddItem(DBItem item) {
		items.Add(item);
	}

	public void removeItem(DBItem item) {
		items.Remove(item);
	}
}
