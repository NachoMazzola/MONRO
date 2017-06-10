using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


public class UIInventory : MonoBehaviour {

	private GameObject inventoryScrollViewContainer;
	private GameObject inventoryContent;
	private ScrollRect theScrollRect;
	private Button closeInventoryButton;
	private BoxCollider2D inventoryCollider;

	private List<Transform> itemList;

	public bool isOpened;

	private Vector2 lastItemPosition;


	private List<IInventoryObserver> inventoryObservers;

	void Awake() {
		isOpened = false;
		itemList = new List<Transform>();

		inventoryScrollViewContainer = GameObject.Find("InventoryScrollViewContainer");
		inventoryScrollViewContainer.SetActive(false);

		inventoryContent = inventoryScrollViewContainer.transform.Find("InventoryContentPanel").gameObject;

		closeInventoryButton = GameObject.Find("CloseInventoryButton").GetComponent<Button>();
		closeInventoryButton.gameObject.SetActive(false);

		inventoryCollider = GetComponent<BoxCollider2D>();

		theScrollRect = inventoryScrollViewContainer.GetComponent<ScrollRect>();
		theScrollRect.vertical = false;

	}

	public void OpenInventory() {
		if (isOpened == true) {
			return;
		}
		isOpened = true;

		closeInventoryButton.gameObject.SetActive(true);
		inventoryScrollViewContainer.SetActive(true);
		inventoryCollider.enabled = true;

		foreach (IInventoryObserver obs in inventoryObservers) {
			obs.OnInventoryOpened();
		}
	}

	public void CloseInventory() {
		if (isOpened == false) {
			return;
		}
		isOpened = false;

		closeInventoryButton.gameObject.SetActive(false);
		inventoryScrollViewContainer.SetActive(false);
		inventoryCollider.enabled = false;

		foreach (IInventoryObserver obs in inventoryObservers) {
			obs.OnInventoryClosed();
		}
	}

	public void LoadItems(List<DBItem> theItemList) {
		//TODO: LOAD STORED ITEMS FROM HardDrive
	}

	public void AddItemToInventory(Transform item) {
		
		Transform theInstantiatedItem = Instantiate(item, new Vector2(), Quaternion.identity) as Transform;
		//theInstantiatedItem.gameObject.AddComponent<DragHandler>();

		itemList.Add(theInstantiatedItem);

		Sprite itemImage = item.gameObject.GetComponent<SpriteRenderer>().sprite;
		Vector2 sprite_size = itemImage.rect.size;
		Vector2 local_sprite_size = sprite_size / itemImage.pixelsPerUnit;

		//first item
		if (itemList.Count == 1) {
			lastItemPosition = new Vector2();
			lastItemPosition.x = inventoryContent.transform.position.x - local_sprite_size.x/2;
			lastItemPosition.y = inventoryContent.transform.position.y;
		}
		else {
			Vector2 newPos = new Vector2(lastItemPosition.x - local_sprite_size.x, lastItemPosition.y);	
			lastItemPosition = newPos;
		}


		theInstantiatedItem.position = lastItemPosition;
		theInstantiatedItem.SetParent(inventoryContent.transform);
		theInstantiatedItem.SetSiblingIndex(0);
		//theInstantiatedItem.localScale = new Vector2(1, 1);

		lastItemPosition = theInstantiatedItem.position;

		foreach (IInventoryObserver obs in inventoryObservers) {
			obs.OnInventoryAddedItem();
		}
	}


	public void EnableScrolling(bool enable) {
		theScrollRect.horizontal = enable;
	}

	public void AddInventoryObserver(IInventoryObserver obs) {
		if (inventoryObservers == null) {
			inventoryObservers = new List<IInventoryObserver>();
		}
		inventoryObservers.Add(obs);
	}

	public void RemoveInventoryObserver(IInventoryObserver obs) {
		if (inventoryObservers == null) {
			inventoryObservers = new List<IInventoryObserver>();
		}
		inventoryObservers.Remove(obs);
	}
}
