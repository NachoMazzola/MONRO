using UnityEngine;
using System.Collections;

public class InventoryItem : Item {

	private Transform instanciatedWorldItem;
	private WorldItem wItem;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
	}

	void OnMouseDown() {
		if (ItemWorldRepTransform != null) {
			Vector3 targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			targetPosition.z = -0.1f;
			instanciatedWorldItem = Instantiate(ItemWorldRepTransform, targetPosition, Quaternion.identity) as Transform;
		
			this.GetComponent<SpriteRenderer>().enabled = false;


			wItem = instanciatedWorldItem.GetComponent<WorldItem>();
			wItem.StartDragging();
			wItem.inventoryItemRepresentation = this;
			wItem.gameObject.SetActive(true);
		}
	}

	void OnMouseUp() {
		this.GetComponent<SpriteRenderer>().enabled = true;
	}

	override public void ActivateInventoryItem() {
		base.ActivateInventoryItem();
		this.GetComponent<SpriteRenderer>().enabled = true;

		Destroy(instanciatedWorldItem.gameObject);

		instanciatedWorldItem = null;
		wItem = null;
	}
}
