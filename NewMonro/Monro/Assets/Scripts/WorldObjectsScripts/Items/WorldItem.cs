using UnityEngine;
using System.Collections;
using UnityEngine.UI;

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

				GameObject inv = GameObject.Find("UIInventory");
				UIInventory invScp = inv.GetComponent<UIInventory>();

				invScp.EnableScrolling(true);
			}
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

	void OnTriggerEnter2D(Collider2D other) {
		if (other && other.gameObject.tag == "Player") {
			Debug.Log("ENTRE EN LA COLISION PAPI!");
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if (other && other.gameObject.tag == "Player") {
			Debug.Log("ME SALI DE LA COLISION PAPI!");
		}
	}


}
