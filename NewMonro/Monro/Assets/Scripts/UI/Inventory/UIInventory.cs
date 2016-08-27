using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


public class UIInventory : MonoBehaviour {

	private GameObject inventoryScrollViewContainer;
	private GameObject inventoryContent;
	private Button openInventoryButton;
	private Button closeInventoryButton;

	private List<Transform> itemList;
	private bool isOpened;

	private Vector2 lastItemPosition;

	private Animator inventoryAnimator;

	void Awake() {
		isOpened = false;
		itemList = new List<Transform>();

		inventoryScrollViewContainer = GameObject.Find("InventoryScrollViewContainer");
		inventoryScrollViewContainer.SetActive(false);

		inventoryContent = inventoryScrollViewContainer.transform.FindChild("InventoryContentPanel").gameObject;

		openInventoryButton = GameObject.Find("OpenInventoryButton").GetComponent<Button>();
		closeInventoryButton = GameObject.Find("CloseInventoryButton").GetComponent<Button>();
		closeInventoryButton.gameObject.SetActive(false);
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public void OpenInventory() {
		isOpened = true;

		openInventoryButton.gameObject.SetActive(false);
		closeInventoryButton.gameObject.SetActive(true);
		inventoryScrollViewContainer.SetActive(true);
	}

	public void CloseInventory() {
		isOpened = false;

		openInventoryButton.gameObject.SetActive(true);
		closeInventoryButton.gameObject.SetActive(false);
		inventoryScrollViewContainer.SetActive(false);
	}

	public void LoadItems(List<Item> theItemList) {
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
		//theInstantiatedItem.localScale = new Vector2(1, 1);

		lastItemPosition = theInstantiatedItem.position;


		OpenInventoryButton btnComp = openInventoryButton.GetComponent<OpenInventoryButton>();
		btnComp.PlayAddingItemToInventoryAnim();

	}


	public void EnableScrolling(bool  enable) {
		ScrollRect c = this.GetComponentInChildren<ScrollRect>();
		if (c != null) {
			c.enabled = enable;
		}

	}

}
