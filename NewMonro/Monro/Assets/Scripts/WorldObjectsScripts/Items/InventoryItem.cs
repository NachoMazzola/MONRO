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
			this.gameObject.SetActive(false);

			wItem = instanciatedWorldItem.GetComponent<WorldItem>();
			wItem.StartDragging();
			wItem.inventoryItemRepresentation = this;
		}
	}

	void OnMouseUp() {
		this.gameObject.SetActive(true);
	}

	override public void ActivateInventoryItem() {
		base.ActivateInventoryItem();

		Destroy(instanciatedWorldItem.gameObject);

		instanciatedWorldItem = null;
		wItem = null;
	}
}
