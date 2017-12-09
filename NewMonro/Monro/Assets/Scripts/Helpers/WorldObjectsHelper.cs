using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

/**
 * Easy access for world game objects
*/
public static class WorldObjectsHelper {

	public static GameObject getPlayerGO() {
		return GameObject.Find("PlayerViking");
	}
		
	public static GameObject getDialogueRunnerGO() {
		return GameObject.Find("Dialogue");
	}

	public static GameObject getUIGO() {
		return GameObject.Find("UI");
	}

	public static GameObject getUIInventoryGO() {
		return GameObject.Find("UI-Inventory");
	}

	public static GameObject getMovementControllerGO() {
		return GameObject.Find("MovementController");
	}

	public static GameObject getPuzzleManagerGO() {
		return GameObject.Find("PuzzleManager");
	}

	public static GameObject getWorldInteractionControllerGO() {
		return GameObject.Find("WorldInteractionController");
	}

	public static GameObject getFloorGO() {
		return GameObject.Find("Floor");
	}
}
