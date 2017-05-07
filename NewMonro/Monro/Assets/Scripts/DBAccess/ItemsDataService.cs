using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SQLite4Unity3d;

public class ItemsDataService : DataService {

	public ItemsDataService(string database): base(database) {
		
	}

	public TableQuery<DBItem> GetItems() {
		return _connection.Table<DBItem>();
	}

	public DBItem GetItemById(string itemId) {
		return _connection.Table<DBItem>().Where(x => x.ItemId == itemId).FirstOrDefault();
	}

	public DBItem GetCombinedItem(string rhItem, string lhItem) {
		IEnumerable<DBItem> combination = _connection.Table<DBItem>().Where(x => x.ItemId == rhItem && x.CombinesWithId == lhItem);
		DBItem theItem = combination as DBItem;
		if (theItem != null) {
			return GetItemById(theItem.CombinedItemResultId);
		}
//
//		Item right = GetItemById(rhItem) as Item;
//		Item left = GetItemById(lhItem) as Item;
//
//		if (right != null && left != null) {
//			
//		}

		return null;
	}
}
