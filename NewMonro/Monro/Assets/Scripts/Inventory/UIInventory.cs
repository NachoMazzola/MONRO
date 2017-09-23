using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


public class UIInventory : MonoBehaviour {

	private const int itemContainerW = 50;
	private const int itemContainerH = 44;
	private const int maxItems = 4;

	private GameObject inventoryScrollViewContainer;
	private Transform inventoryContent;
	private ScrollRect theScrollRect;
	private Button closeInventoryButton;
	private BoxCollider2D inventoryCollider;

	private List<Transform> itemList;

	public bool isOpened;

	private Vector2 lastItemPosition;
	private ItemContainerCreator containerCreator;

	private List<IInventoryObserver> inventoryObservers;
	private InventoryModel inventoryModel;

	void Awake() {
		isOpened = false;
		itemList = new List<Transform>();
		inventoryModel = new InventoryModel();

		Transform UI = GameObject.Find("UI-Inventory").transform;

		inventoryScrollViewContainer = UI.Find("Inventory").gameObject;
		inventoryScrollViewContainer.SetActive(false);
		inventoryCollider = inventoryScrollViewContainer.GetComponent<BoxCollider2D>();

		inventoryContent = inventoryScrollViewContainer.transform.Find("InventoryContentPanel");

		closeInventoryButton = UI.Find("CloseInventoryButton").GetComponent<Button>();
		closeInventoryButton.gameObject.SetActive(false);

		theScrollRect = inventoryScrollViewContainer.GetComponent<ScrollRect>();
		theScrollRect.vertical = false;

		containerCreator = inventoryContent.GetComponent<ItemContainerCreator>();
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

		//TODO: 5 esta hardcodeado, deberiamos poner un numero maximo para el scrolling.. por lo menos calcular
		//de acuerdo con el ancho del item una vez q tengamos wxh definidos
		//theScrollRect.horizontal = itemList.Count > 5;
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
		Transform container = containerCreator.createContainerWithItemImage(theInstantiatedItem);

		container.transform.SetParent(inventoryContent, false);
		((RectTransform)container.transform).sizeDelta = new Vector2(itemContainerW, itemContainerH);

		DBItemLoader itemLoader = item.GetComponent<DBItemLoader>();
		container.GetComponent<ItemContainerPanel>().itemModel = itemLoader.itemModel;

		inventoryModel.AddItem(itemLoader.itemModel);

		itemList.Add(container);

		//first item
		if (itemList.Count == 1) {
			lastItemPosition = new Vector2();
		}
		else {
			Vector2 newPos = new Vector2(lastItemPosition.x - ((RectTransform)container.transform).rect.size.x * container.localScale.x, 0);	
			lastItemPosition = newPos;
		}

		((RectTransform)container.transform).anchoredPosition = lastItemPosition;
		lastItemPosition = ((RectTransform)container.transform).anchoredPosition;

		if (itemList.Count == maxItems) {
			//TODO: hacer crecer el container
		}


		foreach (IInventoryObserver obs in inventoryObservers) {
			obs.OnInventoryAddedItem();
		}
	}

	public void RemoveItem(string itemId) {
		Transform itemToRemove = null;
		DBItem itemModelToRemove = null;
		foreach (Transform container in itemList) {
			DBItem itemModel = container.GetChild(container.childCount-1).GetComponent<DBItemLoader>().itemModel;
			if (itemModel.ItemId == itemId) {
				itemToRemove = container;
				itemModelToRemove = itemModel;
				break;
			}
		}

		if (itemToRemove != null) {
			foreach (Transform child in itemToRemove) {
				GameObject.Destroy(child.gameObject);
			}

			foreach (Transform child in inventoryContent) {
				if (child == itemToRemove) {
					GameObject.Destroy(child.gameObject);	
				}
			}


			//itemToRemove.transform.SetParent(null);

			itemList.Remove(itemToRemove);
			inventoryModel.removeItem(itemModelToRemove);

			Destroy(itemToRemove);
		}

		//Reorder UI list!!!
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
