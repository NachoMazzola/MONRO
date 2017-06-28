using UnityEngine;
using System.Collections;

public class OpenInventoryButton : MonoBehaviour, IInventoryObserver, IWorldInteractionObserver {

	private Animator buttonAnimator;
	private UIInventory inventory;

	void Awake() {
		buttonAnimator = GetComponent<Animator>();
		inventory = GameObject.Find("UIInventory").GetComponent<UIInventory>();
		inventory.AddInventoryObserver(this);
	}

	void Start() {
		WorldInteractionController.getComponent().AddObserver(this);
	}

	void OnDestroy() {
		if (WorldInteractionController.getComponent()) {
			WorldInteractionController.getComponent().RemoveObserver(this);
		}
	}

	public void AnimEventAddingItemAnimFinished() {
		buttonAnimator.SetBool("itemAdded", false);
	}
		
	virtual public void IWOTapped(Vector2 tapPos, GameObject other) {
		if (other != this.gameObject) {
			return;
		}

		inventory.OpenInventory();
	}

	virtual public void IWOTapHold(Vector2 tapPos, GameObject other) {

	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other == null) {
			return;
		}
		inventory.OpenInventory();
	}

	public void OnInventoryOpened() {
		this.gameObject.SetActive(false);
	}

	public void OnInventoryClosed() {
		this.gameObject.SetActive(true);
	}

	public void OnInventoryAddedItem() {
		buttonAnimator.SetBool("itemAdded", true);
	}

	public void OnInventoryRemovedItem() {}
}
