using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

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
		}
	}

	public void AddItem(Transform item) {
		//find first unnocuppied slot
		for (int i = 0; i < this.InventorySize; i++) {
			ItemContainerPanel slot = this.transform.GetChild(i).GetComponent<ItemContainerPanel>();
			if (!slot.isOccupied) {
				item.SetParent(slot.transform);
				((RectTransform)item).anchoredPosition = new Vector3();
				((RectTransform)item).localScale = new Vector3(1,1,1);

				DBItemLoader itemLoader = item.GetComponent<DBItemLoader>();
				slot.GetComponent<ItemContainerPanel>().itemModel = itemLoader.itemModel;

				slot.isOccupied = true;
				break;
			}
		}
	}

	public void EnableScrolling(bool enable) {
		WorldObjectsHelper.GetUIInventoryContentScrollViewGrid().GetComponent<ScrollRect>().vertical = enable;
	}
}
