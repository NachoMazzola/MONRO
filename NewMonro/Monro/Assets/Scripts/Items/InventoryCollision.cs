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

		//Debug.Log("INVENTORY COLLITION ENTER : " + other);
	}

	void OnTriggerExit2D (Collider2D other) {
		if (other == null) {
			return;
		}

		if (other.transform.parent != null) {
			DraggableWorldItem dwItem = other.transform.parent.GetComponent<DraggableWorldItem>();
			if (dwItem !=null && dwItem.IsBeingDraggedOverInventory && !dwItem.IsBeingDragged) {
				return;
			}
		}

		UIInventory inv = GameObject.Find("UI-Inventory").GetComponent<UIInventory>();
		inv.CloseInventory();
	}
}
