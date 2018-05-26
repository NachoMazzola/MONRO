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
		WorldItem,
		unknown
	}

	public enum SubGameEntityType {
		NPC_Hildr,
		NPC_Mimir,
		unknown
	}

	public GameEntityType type = GameEntityType.unknown;
	public SubGameEntityType subType = SubGameEntityType.unknown;

	public string ID;
}
