using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class DragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

	public static GameObject itemBeingDragged;
	Vector2 startingPosition;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//IBeginDrag implementation

	public void OnBeginDrag (PointerEventData eventData) {
		gameObject.transform.SetParent(null);
		itemBeingDragged = gameObject;
		startingPosition = transform.position;
	}

	//IDragHandler implementation

	public void OnDrag(PointerEventData eventData) {
		Vector2 transformedPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		transform.position = transformedPos;
	}

	//IEndrDrag implementation

	public void OnEndDrag(PointerEventData eventData) {
		itemBeingDragged = null;
		transform.position = startingPosition;
	}


}
