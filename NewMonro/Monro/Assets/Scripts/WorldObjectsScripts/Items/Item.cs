using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class Item : MonoBehaviour {

	public Transform ItemWorldRepTransform;

	protected string itemName;
	public string itemId;
	public string resolvesPuzzleId;
	protected string itemDescription;

	private Transform instanciatedWorldItem;
	private WorldItem wItem;

	public bool ItemHasBeenUsed;

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
			wItem.itemModel = this;
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
		wItem = null;
	}
		
}
