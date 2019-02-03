﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

/**
 * 
 * FIJATE QUE EL ITEM NO SE VE EN EL INVENTARIO PORQUE Z DEL SLOT Y DEL ITEM ES -5000 Y PICO.. DEBERIA SER 0
 * Y FIJATE QUE PASA QUE EL PUZZLE 3 NO FIGURA COMO ENABLED!
*/


/**
 * Handles adding/removing items within ItemContainerPanels in inventory
*/
public class InventoryPanelHandler : MonoBehaviour {

	public Transform PanelSlot;
	public int InventorySize = 10;

	//private GridLayoutGroup gridLayout;

	void Awake() {
		//this.gridLayout = this.GetComponent<GridLayoutGroup>();
		//TODO: Acomodar el tamaño de las celdas de acuerdo a la cantidad de elementos
		for (int i = 0; i < this.InventorySize; i++) {
			Transform instanciatedSlot = GameObject.Instantiate(PanelSlot);
			instanciatedSlot.SetParent(this.gameObject.transform);
			instanciatedSlot.localScale = new Vector3(1,1,1);
			instanciatedSlot.position = new Vector3(instanciatedSlot.position.x, instanciatedSlot.position.y, 1);
		}
	}

	public void AddItem(Transform item) {
		//find first unnocuppied slot
		for (int i = 0; i < this.InventorySize; i++) {
			RectTransform slotTransform = (RectTransform)this.transform.GetChild(i).transform;
			ItemContainerPanel slot = this.transform.GetChild(i).GetComponent<ItemContainerPanel>();
			if (!slot.isOccupied) {
				item.SetParent(slot.transform);

				((RectTransform)item).localScale = new Vector3(1,1,1);
				((RectTransform)item).anchoredPosition = new Vector3(0, 0, 0);
				((RectTransform)item).localPosition = new Vector3(0, 0, 0);

				Image itemImg = item.GetComponent<Image>();
				if (itemImg != null) {
					((RectTransform)item).sizeDelta = slotTransform.sizeDelta;// new Vector2(itemImg.rectTransform.rect.width, itemImg.rectTransform.rect.height) ;
				}


				DBItemLoader itemLoader = item.GetComponent<DBItemLoader>();
				slot.GetComponent<ItemContainerPanel>().itemModel = itemLoader.itemModel;

				slot.isOccupied = true;
				break;
			}
		}
	}

	public void RemoveItems(List<string> itemIdList) {
		for (int i = 0; i < this.InventorySize; i++) {
			ItemContainerPanel slot = this.transform.GetChild(i).GetComponent<ItemContainerPanel>();
			if (slot.isOccupied) {
				DBItem itemModel = slot.itemModel;
				foreach (string id in itemIdList) {
					if (id == itemModel.ItemId) {
						Transform childItem = slot.transform.GetChild(0);
						childItem.SetParent(null);
						slot.isOccupied = false;
						slot.itemModel = null;

						GameObject.Destroy(childItem.gameObject);
					}
				}
			}
		}
		this.ReorderItems();


//		List<Transform> toRemove = new List<Transform>();
//		foreach (Transform child in inventoryContent) {
//			DBItem itemModel = child.GetChild(child.childCount-1).GetComponent<DBItemLoader>().itemModel;
//			foreach (string id in itemIdList) {
//				if (itemModel.ItemId == id) {
//					toRemove.Add(child);
//					GameObject.Destroy(child.gameObject);
//					Destroy(child);
//
//					//remove children
//					foreach (Transform subChild in child) {
//						GameObject.Destroy(subChild.gameObject);
//					}
//					break;
//				}
//			}
//		}
//
//		foreach (Transform t in toRemove) {
//			t.SetParent(null);
//		}
//		this.ReorderItems();
	}

	public void ReorderItems() {
		for (int i = 0; i < this.InventorySize; i++) {
			for (int j = 0; j < this.InventorySize-1; j++) {
				ItemContainerPanel currentSlot = this.transform.GetChild(j).GetComponent<ItemContainerPanel>();	
				ItemContainerPanel nextSlot = this.transform.GetChild(j+1).GetComponent<ItemContainerPanel>();

				if (!currentSlot.isOccupied && nextSlot.isOccupied) {
					//swap
					Transform childToSwap = nextSlot.transform.GetChild(0);
					childToSwap.SetParent(null);
					childToSwap.SetParent(currentSlot.transform);

					((RectTransform)childToSwap).anchoredPosition = new Vector3();
					((RectTransform)childToSwap).localScale = new Vector3(1,1,1);

					DBItemLoader itemLoader = childToSwap.GetComponent<DBItemLoader>();
					currentSlot.GetComponent<ItemContainerPanel>().itemModel = itemLoader.itemModel;


					currentSlot.isOccupied = true;
					nextSlot.isOccupied = false;
				}
			}
		}
	}

	public void EnableScrolling(bool enable) {
		ScrollRect scRect = WorldObjectsHelper.GetUIInventoryContentScrollViewGrid().GetComponent<ScrollRect>();
		if (scRect !=  null) {
			scRect.vertical = enable;	
		} 
	}
}
