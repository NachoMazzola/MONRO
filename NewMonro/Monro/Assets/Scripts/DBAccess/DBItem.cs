using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SQLite4Unity3d;

public class DBItem {
	[PrimaryKey, AutoIncrement]
	public int Id { get; set; }

	public string ItemId {get; set;}
	public string ItemName {get; set;}
	public string CombinesWithId {get; set;}
	public string Description {get; set;}
	public string CombinedItemResultId {get; set;}
	public string ItemPrefab {get; set;} //World item prefab
	public string ItemInventoryPrefab {get; set;} //Inventory Prefab

	public bool ItemHasBeenUsed;
}

//Item allready stored in the DB. This represents 
public class DBStoredItem {
	[PrimaryKey, AutoIncrement]
	public int Id { get; set; }

	public string ItemId {get; set;}
}