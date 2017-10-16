using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SQLite4Unity3d;

public class StoredItemsDataService : DataService {

	public StoredItemsDataService(string database): base(database) {

	}

	public TableQuery<DBStoredItem> GetItems() {
		return _connection.Table<DBStoredItem>();
	}

	public DBStoredItem GetItemById(string itemId) {
		return _connection.Table<DBStoredItem>().Where(x => x.ItemId == itemId).FirstOrDefault();
	}

	public List<DBItem> getDBItemsFromStoredItems() {
		List<DBItem> stored = new List<DBItem>();



		return stored;
	}

	public void StoreItemWithId(string itemId) {
		_connection.Execute("INSERT into STOREDITEMS(ItemId) values ('" + itemId + "')");	
	}

	public void RemoveItemWithId(string itemId) {
		_connection.Execute("DELETE from STOREDITEMS(ItemId) values ('" + itemId + "')");	
	}
}
