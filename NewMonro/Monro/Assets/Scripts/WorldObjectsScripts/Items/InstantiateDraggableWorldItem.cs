﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateDraggableWorldItem : MonoBehaviour {

	public Transform ItemWorldRepTransform;
	private Transform instanciatedWorldItem;

	void OnMouseDown() {
		if (ItemWorldRepTransform != null) {
			Vector3 targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			targetPosition.z = -0.1f;
			instanciatedWorldItem = Instantiate(ItemWorldRepTransform, targetPosition, Quaternion.identity) as Transform;

			//this.GetComponent<SpriteRenderer>().enabled = false;

			DraggableWorldItem wItem = instanciatedWorldItem.GetComponent<DraggableWorldItem>();
			wItem.StartDragging();
			wItem.itemModel = this.GetComponent<DBItemLoader>().itemModel;
			wItem.gameObject.SetActive(true);
		}
	}
}
