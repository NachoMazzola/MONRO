using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InstantiateDraggableWorldItem : MonoBehaviour, IPointerDownHandler
{

	public Transform ItemWorldRepTransform;
	private Transform instanciatedWorldItem;

	private DBItem instanciatedItemModel;

//	void OnMouseDown ()
//	{
////		if (ItemWorldRepTransform != null) {
////			Vector3 targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
////			targetPosition.z = -0.1f;
////			instanciatedWorldItem = Instantiate(ItemWorldRepTransform, targetPosition, Quaternion.identity) as Transform;
////
////			//this.GetComponent<SpriteRenderer>().enabled = false;
////
////			DraggableWorldItem wItem = instanciatedWorldItem.GetComponent<DraggableWorldItem>();
////			wItem.StartDragging();
////			wItem.itemModel = this.GetComponent<DBItemLoader>().itemModel;
////			wItem.gameObject.SetActive(true);
////		}
//	}

	private void InstantiateItem ()
	{
		Vector3 targetPosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		targetPosition.z = -0.1f;
		instanciatedWorldItem = Instantiate (ItemWorldRepTransform, targetPosition, Quaternion.identity) as Transform;
		
		//this.GetComponent<SpriteRenderer>().enabled = false;
		
		DraggableWorldItem wItem = instanciatedWorldItem.GetComponent<DraggableWorldItem> ();
		wItem.StartDragging ();
		wItem.itemModel = instanciatedItemModel;
		wItem.gameObject.SetActive (true);
	}

	public void OnPointerDown (PointerEventData eventData)
	{
		if (this.gameObject.transform.childCount == 0) {
			Debug.Log ("No childs in container!! Doing nothing...");
			return;
		}

		//UIInventoryItem-xx- 
		RectTransform itemInContainerTransform = (RectTransform)this.gameObject.transform.GetChild (0);
		string itemId = itemInContainerTransform.GetComponent<DBItemLoader> ().itemId;

		instanciatedItemModel = DBAccess.getComponent ().itemsDataBase.GetItemById (itemId);
		//TODO: Fijate que levante bien el insanciatedItemModel.ItemPrefab asi se lo pasas por parametro en vez del
		//string hardcodeado!
		GameObject o = Resources.Load("UIWorldItemHeart") as GameObject;
		ItemWorldRepTransform = o.transform;

		InstantiateItem();
	}
}
