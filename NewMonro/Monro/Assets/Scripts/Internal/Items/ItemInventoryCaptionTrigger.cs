using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemInventoryCaptionTrigger : MonoBehaviour, IPointerDownHandler, IPointerClickHandler {

	private string caption;
	private bool isHoldingDown;

	private RectTransform itemTransform;

    private LookAtCommand captionCommand;

	void Start() {
		
	}

	public void OnPointerDown (PointerEventData eventData) {
        itemTransform = (RectTransform)this.gameObject.transform.GetChild(0);
        string itemId = itemTransform.GetComponent<DBItemLoader>().itemId;

        DBItem instanciatedItemModel = DBAccess.getComponent().itemsDataBase.GetItemById(itemId);
        caption = instanciatedItemModel.Description;

        InstantiateDraggableWorldItem instDraggable = itemTransform.GetComponent<InstantiateDraggableWorldItem>();
        if (instDraggable)
        {
            isHoldingDown = instDraggable.holdDown;
        }
    }

	public void OnPointerClick(PointerEventData eventData) {
		if (this.gameObject.transform.childCount == 0) {
			return;
		}

        captionCommand = new LookAtCommand(this.itemTransform.gameObject, WorldObjectsHelper.getPlayerGO());
        captionCommand.Prepare();
        captionCommand.WillStart();
	}

    void Update()
    {
        if (captionCommand != null)
        {
            captionCommand.UpdateCommand();
        }
    }

}
