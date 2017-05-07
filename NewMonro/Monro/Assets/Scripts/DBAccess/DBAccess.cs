using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SQLite4Unity3d;


public class DBAccess : MonoBehaviour {

	void Awake() {
//		ItemsDataService itemsDS = new ItemsDataService("Items.db");
//
//		TableQuery<Item> caca = itemsDS.GetItems();
//		Item f = caca.Where(x => x.itemId == "ITM001").First();
//
//
//		Item test = itemsDS.GetItemById("ITM001");
//
//		int a3 = 2;

		ItemsDataService itemsDS = new ItemsDataService("Items.db");
		DBItem caca = itemsDS.GetItemById("ITM001");

		int f = 2;
	}


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
