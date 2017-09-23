using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEntity : MonoBehaviour {

	public enum GameEntityType
	{
		Player,
		NPC,
		InteractiveObject,
		InventoryItem,
		WorldItem
	}

	public GameEntityType type;
}
