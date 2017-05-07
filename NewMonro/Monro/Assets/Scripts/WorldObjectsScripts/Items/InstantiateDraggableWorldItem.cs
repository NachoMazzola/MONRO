using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateDraggableWorldItem : MonoBehaviour {

	public Transform ItemWorldRepTransform;
	private Transform instanciatedWorldItem;

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

			DraggableWorldItem wItem = instanciatedWorldItem.GetComponent<DraggableWorldItem>();
			wItem.StartDragging();
			wItem.itemModel = this.GetComponent<Item>();
			wItem.gameObject.SetActive(true);
		}
	}

	void OnMouseUp() {
		this.GetComponent<SpriteRenderer>().enabled = true;
	}

	public void ActivateInventoryItem() {
		this.gameObject.SetActive(true);
		this.GetComponent<SpriteRenderer>().enabled = true;

		Destroy(instanciatedWorldItem.gameObject);

		instanciatedWorldItem = null;
		//wItem = null;
	}

}
