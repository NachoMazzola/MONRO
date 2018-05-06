using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SQLite4Unity3d;


public class DBAccess : MonoBehaviour {

	[HideInInspector]
	public ItemsDataService itemsDataBase;

	static public DBAccess getComponent ()
	{
		return GameObject.Find ("DataBase").GetComponent<DBAccess> ();
	}
		
	void Awake() {
		itemsDataBase = new ItemsDataService("Items.db");
	}
}
