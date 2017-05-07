using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCombinator : MonoBehaviour {

	static public ItemCombinator getComponent () {
		return GameObject.Find ("ItemCombinator").GetComponent<ItemCombinator> ();
	}


	// Use this for initialization
	void Start () {
		//levantar la base de datos de items!
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public bool CombineItems(Item rhItem, Item lhItem) {

		//chequear como comparar los items!

		InstantiateCombinationResult();

		Destroy(rhItem.gameObject);
		Destroy(lhItem.gameObject);

		return true;
	}

	public void InstantiateCombinationResult() {
		GameObject inv = GameObject.Find("UIInventory");
		UIInventory invScp = inv.GetComponent<UIInventory>();

		GameObject pPrefab = Resources.Load("UIInventoryItemCombinationTest") as GameObject;

		invScp.AddItemToInventory(pPrefab.transform);
	}
}
