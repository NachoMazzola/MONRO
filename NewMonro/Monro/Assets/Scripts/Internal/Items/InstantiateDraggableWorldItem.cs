using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/**
 * Handles instanciating a Draggable World Item
*/
public class InstantiateDraggableWorldItem : MonoBehaviour, IPointerDownHandler, IPointerClickHandler
{
	[HideInInspector]
	public Transform ItemWorldRepTransform;
	[HideInInspector]
	public bool holdDown = false;

	public float TimeToHoldToInstantiate = 0.5f;

	private Transform instanciatedWorldItem;
	private DBItem instanciatedItemModel;
	private float holdDelta;


	void Update() {
		if (holdDown) {
			holdDelta += Time.deltaTime;
		}

		if (holdDown && holdDelta >= TimeToHoldToInstantiate) {
			InstantiateDraggable();
			holdDown = false;
			holdDelta = 0;
		}
	}

	private void InstantiateItem ()
	{
		Vector3 targetPosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		targetPosition.z = 1;
		instanciatedWorldItem = Instantiate (ItemWorldRepTransform, targetPosition, Quaternion.identity) as Transform;


		DraggableWorldItem wItem = instanciatedWorldItem.GetComponent<DraggableWorldItem> ();
		wItem.StartDragging ();
		wItem.itemModel = instanciatedItemModel;
		wItem.gameObject.SetActive (true);
	}

	private void InstantiateDraggable() {
		if (this.gameObject.transform.childCount == 0) {
			Debug.Log ("No childs in container!! Doing nothing...");
			return;
		}

		//UIInventoryItem-xx- 
		RectTransform itemInContainerTransform = (RectTransform)this.gameObject.transform.GetChild (0);
		string itemId = itemInContainerTransform.GetComponent<DBItemLoader> ().itemId;

		instanciatedItemModel = DBAccess.getComponent ().itemsDataBase.GetItemById (itemId);
		GameObject o = Resources.Load(instanciatedItemModel.ItemPrefab) as GameObject;
		if (o == null) {
			Debug.Log("ERROR -- Cannot load dbitem prefab");
			return;
		}
		ItemWorldRepTransform = o.transform;
	
		InstantiateItem();
	}

	public void OnPointerDown (PointerEventData eventData) {
		holdDown = true;
	}

	public void OnPointerClick(PointerEventData eventData) {
		holdDown = false;
	}
}
