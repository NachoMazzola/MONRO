using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DraggableWorldItem : MonoBehaviour {

	private DragHandler theDragging;
	private Transform gameobjectItmeIsOver;
	private UIInventory inventory;

	public bool IsBeingDragged;

	[HideInInspector]
	public DBItem itemModel;

	void Awake() {
		GameObject inv = GameObject.Find("UIInventory");
		inventory =  inv.GetComponent<UIInventory>();
	}

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonUp(0)) {
			StopDragging();
		}
	}

	public void StopDragging() {
		IsBeingDragged = false;
		this.gameObject.SetActive(false);
		theDragging.draggingMode = false;

		HandleDrop();
	}

	public void StartDragging() {
		
		theDragging = this.GetComponent<DragHandler>();
		theDragging.SetDraggingObject(this.gameObject);

		IsBeingDragged = true;

		inventory.EnableScrolling(false);
	}

	public virtual void ItemIsOverObject(Transform other) {
		gameobjectItmeIsOver = other;
//
//		HighlightableObject highlight = this.GetComponent<HighlightableObject>();
//		highlight.HighlightObject();
	}

	public virtual void ItemHasBeenReleasedOverObject(Transform other) {
		gameobjectItmeIsOver = null;
//		HighlightableObject highlight = this.GetComponent<HighlightableObject>();
//		highlight.RemoveHighlight();

	}

	private void HandleDrop() {
		if (gameobjectItmeIsOver != null) {
			DropItemPuzzleAction dropPuzzleAction = gameobjectItmeIsOver.GetComponent<DropItemPuzzleAction>();
			if (dropPuzzleAction != null) {
				dropPuzzleAction.worldItem = this;
				if (dropPuzzleAction.Execute()) {
					Destroy(this);
				}
				return;
			}
			else {
				//this is when we drop an uiinventoryitem over another uiinventoryitem ==> COMBINATION!
				DBItem it = gameobjectItmeIsOver.GetComponent<DBItemLoader>().itemModel;
				if (it != null) {
					bool combinationResult = inventory.GetComponent<ItemCombinator>().CombineItems(it, this.itemModel);
					if (!combinationResult) {
						
					}
				}
			}
		}
		else {
			GameObject.Destroy(this.gameObject);
		}
	}
}
