using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

/**
 * Easy access for world game objects
*/
public static class WorldObjectsHelper {

	public static GameObject InstantiatePrefabFromResources(string prefabName, Transform parent) {
		GameObject pPrefab = Resources.Load(prefabName) as GameObject;
		GameObject inst = GameObject.Instantiate(pPrefab);
		if (parent != null) {
			inst.transform.SetParent(parent);	
		}

		return inst;
	}

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


	public static GameObject getMainCamera() {
		return GameObject.Find("FollowCamera");
	}

	/**
	 * NEW SHIT
	**/
	public static GameObject VerbsPanelUIGO() {
		return GameObject.Find("VerbsPanel");
	}

	public static GameObject getUIInventoryPanelContentGO() {
		return GameObject.Find("InventoryContent");
	}

	public static GameObject GetUIInventoryContentScrollViewGrid() {
		return GameObject.Find("InventoryContentScrollViewGrid");
	}

	public static GameObject GetBottomPanelUIGO() {
		return GameObject.Find("UI").transform.Find("UIPanels").gameObject;
	}

	public static GameObject GetCommandManagerGO() {
		return GameObject.Find("CommandManager");
	}
}
