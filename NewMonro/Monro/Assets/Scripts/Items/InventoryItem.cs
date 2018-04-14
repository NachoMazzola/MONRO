using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(GameEntity))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(DBItemLoader))]
[RequireComponent(typeof(BoxCollider2DSizeFitter))]
public class InventoryItem : MonoBehaviour {

	void Awake() {
		this.GetComponent<GameEntity>().type = GameEntity.GameEntityType.InventoryItem;
		this.GetComponent<BoxCollider2D>().isTrigger = true;
	}
}
