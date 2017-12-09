using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemInventoryCaptionTrigger : MonoBehaviour, IPointerDownHandler, IPointerClickHandler {

	private string caption;
	private bool isHoldingDown;

	private RectTransform itemTransform;

	void Start() {
		itemTransform = (RectTransform)this.gameObject.transform.GetChild (0);
		string itemId = itemTransform.GetComponent<DBItemLoader> ().itemId;

		DBItem instanciatedItemModel = DBAccess.getComponent ().itemsDataBase.GetItemById (itemId);
		caption = instanciatedItemModel.Description;
	}

	public void OnPointerDown (PointerEventData eventData) {
		
	}

	public void OnPointerClick(PointerEventData eventData) {
		InstantiateDraggableWorldItem instDraggable = itemTransform.GetComponent<InstantiateDraggableWorldItem>();
		if (instDraggable) {
			isHoldingDown = instDraggable.holdDown;
		}

		if (!isHoldingDown) {
			Player pl = WorldObjectsHelper.getPlayerGO().GetComponent<Player>();
			StartCoroutine(pl.ShowCaption(caption));
			Debug.Log("InventoryCaptionTrigger SHOW CAPTION: " + caption);
		}
	}

}
