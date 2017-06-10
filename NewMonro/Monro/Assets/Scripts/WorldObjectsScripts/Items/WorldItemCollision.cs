using UnityEngine;
using System.Collections;

public class WorldItemCollision : MonoBehaviour
{

	private DraggableWorldItem draggableItem;

	void Awake() {
		draggableItem = this.transform.parent.gameObject.GetComponent<DraggableWorldItem> ();	
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other == null) {
			return;
		}

		UIInventory theInventory = other.gameObject.GetComponent<UIInventory>();
		if (theInventory != null) {
			draggableItem.IsBeingDraggedOverInventory = true;
		}
			
		draggableItem.ItemIsOverObject (other.transform);
	}

	void OnTriggerExit2D (Collider2D other)
	{
		if (other == null) {
			return;
		}

		UIInventory theInventory = other.gameObject.GetComponent<UIInventory>();
		if (theInventory != null && draggableItem.IsBeingDragged) {
			draggableItem.IsBeingDraggedOverInventory = false;
		}
			
		draggableItem.ItemHasBeenReleasedOverObject(other.transform);
	}
}
