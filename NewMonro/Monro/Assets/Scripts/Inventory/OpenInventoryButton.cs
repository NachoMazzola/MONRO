using UnityEngine;
using System.Collections;

public class OpenInventoryButton : MonoBehaviour, IInventoryObserver {

	private Animator buttonAnimator;
	private UIInventory inventory;

	void Awake() {
		buttonAnimator = GetComponent<Animator>();
		inventory = GameObject.Find("UI").GetComponent<UIInventory>();
		inventory.AddInventoryObserver(this);
	}
		
	public void AnimEventAddingItemAnimFinished() {
		buttonAnimator.SetBool("itemAdded", false);
	}

	public void OpenInventory() {
		inventory.OpenInventory();
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
