using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class DraggableWorldItem : MonoBehaviour {

	private DragHandler theDragging;
	private Transform gameobjectItmeIsOver;
	private UIInventory inventory;

	public bool IsBeingDragged;
	public bool IsBeingDraggedOverInventory;

	[HideInInspector]
	public DBItem itemModel;

	void Awake() {
		GameObject inv = GameObject.Find("UI");
		inventory =  inv.GetComponent<UIInventory>();

		IsBeingDraggedOverInventory = true; //always is instanciated being dragged from inventory
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
		if (theDragging) {
			theDragging.draggingMode = false;
		}

		inventory.EnableScrolling(true);

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

			Dictionary<string, object> extraData = new Dictionary<string, object>();
			extraData.Add("itemId", this.itemModel.Id);
			PuzzleManager.UpdatePuzzleWithAction(PuzzleActionType.DropItemOver, gameobjectItmeIsOver, extraData);


/*			DropItemPuzzleAction dropPuzzleAction = gameobjectItmeIsOver.GetComponent<DropItemPuzzleAction>();
			if (dropPuzzleAction != null) {
				dropPuzzleAction.worldItem = this;
				if (dropPuzzleAction.Execute()) {
				}

				Destroy(this.gameObject);
			}
			else {
				//this is when we drop an uiinventoryitem over another uiinventoryitem ==> COMBINATION!
				DBItemLoader itemLoader = gameobjectItmeIsOver.GetComponent<DBItemLoader>();
				if (itemLoader != null) {
					DBItem it = itemLoader.itemModel;
					if (it != null) {
						bool combinationResult = inventory.GetComponent<ItemCombinator>().CombineItems(it, this.itemModel);
						if (!combinationResult) {
							Destroy(this.gameObject);
						}
					}
				}
				else {
					Destroy(this.gameObject);
				}
			}
		}
		else {
			Destroy(this.gameObject);
		}
		*/
		}
	}

}
