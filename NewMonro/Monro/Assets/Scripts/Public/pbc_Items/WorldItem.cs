using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GameEntity))]
[RequireComponent(typeof(DragHandler))]
[RequireComponent(typeof(DraggableWorldItem))]
[RequireComponent(typeof(HighlightableObject))]
public class WorldItem : MonoBehaviour {

	// Use this for initialization
	void Start () {
		this.GetComponent<GameEntity>().type = GameEntity.GameEntityType.WorldItem;
	}
}
