using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryCollision : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other == null) {
			return;
		}

		Debug.Log("INVENTORY COLLIDION : " + other);


	}

	void OnTriggerExit2D (Collider2D other)
	{
		if (other == null) {
			return;
		}

		UIInventory inv = GameObject.Find("UIInventory").GetComponent<UIInventory>();
		inv.CloseInventory();
	}
}
