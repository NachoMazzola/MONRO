using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemInventoryCaptionTrigger : MonoBehaviour, IPointerDownHandler, IPointerClickHandler {

	private string caption;
	private bool isHoldingDown;

	private RectTransform itemTransform;

	void Start() {
		
	}

	public void OnPointerDown (PointerEventData eventData) {
		
	}

	public void OnPointerClick(PointerEventData eventData) {
		if (this.gameObject.transform.childCount == 0) {
			return;
		}

		itemTransform = (RectTransform)this.gameObject.transform.GetChild (0);
		string itemId = itemTransform.GetComponent<DBItemLoader> ().itemId;

		DBItem instanciatedItemModel = DBAccess.getComponent ().itemsDataBase.GetItemById (itemId);
		caption = instanciatedItemModel.Description;

		InstantiateDraggableWorldItem instDraggable = itemTransform.GetComponent<InstantiateDraggableWorldItem>();
		if (instDraggable) {
			isHoldingDown = instDraggable.holdDown;
		}

		if (!isHoldingDown) {

			GameObject playerGO = WorldObjectsHelper.getPlayerGO();

			ICommand tCommand = CommandFactory.CreateCommand(CommandType.LookAtCommandType, playerGO, true, null);
			CommandManager.getComponent().QueueCommand(tCommand, true);
		}
	}

}
