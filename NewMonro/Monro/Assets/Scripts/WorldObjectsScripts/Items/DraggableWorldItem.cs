using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DraggableWorldItem : MonoBehaviour {

	private DragHandler theDragging;
	private Transform gameobjectItmeIsOver;

	public bool IsBeingDragged;

	[HideInInspector]
	public Item itemModel;


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

		HandleDrop();
	}

	public void StartDragging() {
		theDragging = this.GetComponent<DragHandler>();
		theDragging.SetDraggingObject(this.gameObject);

		IsBeingDragged = true;

		GameObject inv = GameObject.Find("UIInventory");
		UIInventory invScp = inv.GetComponent<UIInventory>();

		invScp.EnableScrolling(false);
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
				Item it = gameobjectItmeIsOver.GetComponent<Item>();
				if (it != null) {
					ItemCombinator.getComponent().CombineItems(it, this.itemModel);

				}
			}

		}

	}
}
