using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SQLite4Unity3d;

public class ItemsDataService : DataService {

	public ItemsDataService(string database): base(database) {
		
	}

	public IEnumerable<DBItem> GetItems() {
		return _connection.Table<DBItem>();
	}

	public DBItem GetItemById(string itemId) {
		return _connection.Table<DBItem>().Where(x => x.ItemId == itemId).FirstOrDefault();
	}

	public DBItem GetCombinedItem(string rhItem, string lhItem) {
		DBItem theItem = _connection.Table<DBItem>().Where(x => x.ItemId == rhItem && x.CombinesWithId == lhItem).FirstOrDefault();
		if (theItem != null) {
			return GetItemById(theItem.CombinedItemResultId);
		}
		return null;
	}


	// STORED ITEMS DATABASE //

	public IEnumerable<DBStoredItem> GetStoredItems() {
		return _connection.Table<DBStoredItem>();
	}

	public DBStoredItem GetStoredItemById(string itemId) {
		return _connection.Table<DBStoredItem>().Where(x => x.ItemId == itemId).FirstOrDefault();
	}

	public void StoreItemWithId(string itemId) {
		_connection.Execute("INSERT into DBSTOREDITEM(ItemId) values ('" + itemId + "')");
	}

	public void MarkItemAsUsed(string itemId) {
		DBStoredItem it = GetStoredItemById(itemId);
		if (it != null && it.Used == false) {
			int id = it.Id;
			_connection.Execute ("Update DBSTOREDITEM set Used=" + 1 + " where Id="+id);	
		}
	}

}
