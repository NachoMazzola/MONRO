using UnityEngine;
using System.Collections;

public class WorldItem : MonoBehaviour {

	private DragHandler theDragging;

	public bool IsBeingDragged;

	public Item inventoryItemRepresentation;

	// Use this for initialization
	void Start () {
		

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonUp(0))
		{
			IsBeingDragged = false;
			this.gameObject.SetActive(false);

			if (inventoryItemRepresentation != null) {
				inventoryItemRepresentation.ActivateInventoryItem();
			}

		}

	}

	public void StartDragging() {
		theDragging = this.GetComponent<DragHandler>();
		theDragging.SetDraggingObject(this.gameObject);

		IsBeingDragged = true;
	}

}
