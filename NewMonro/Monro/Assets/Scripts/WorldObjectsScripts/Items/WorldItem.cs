using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WorldItem : MonoBehaviour {

	private DragHandler theDragging;

	public bool IsBeingDragged;
	public bool ItemHasBeenUsed;
	public Item inventoryItemRepresentation;

	// Use this for initialization
	void Start () {
		

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonUp(0))
		{
			StopDragging();
		}

	}

	public void StopDragging() {
		IsBeingDragged = false;
		this.gameObject.SetActive(false);

		if (!ItemHasBeenUsed && inventoryItemRepresentation != null) {
			inventoryItemRepresentation.ActivateInventoryItem();

			GameObject inv = GameObject.Find("UIInventory");
			UIInventory invScp = inv.GetComponent<UIInventory>();

			invScp.EnableScrolling(true);
		}
		else {
			Destroy(this);
		}
	}

	public void StartDragging() {
		theDragging = this.GetComponent<DragHandler>();
		theDragging.SetDraggingObject(this.gameObject);

		IsBeingDragged = true;

		GameObject inv = GameObject.Find("UIInventory");
		UIInventory invScp = inv.GetComponent<UIInventory>();

		invScp.EnableScrolling(false);
	}

	public void ItemIsOverObject(Transform other) {
		HighlightableObject highlight = this.GetComponent<HighlightableObject>();
		highlight.HighlightObject();

		ItemAction theAction = this.GetComponent<ItemAction>();
		Character theChar = other.GetComponent<Character>();
		theAction.ExecuteAction(theChar);

	}

	public void ItemHasBeenReleasedOverObject(Transform other) {
		HighlightableObject highlight = this.GetComponent<HighlightableObject>();
		highlight.RemoveHighlight();

	}
}
