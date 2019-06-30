using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

/**
 * Easy access for world game objects
*/
public static class WorldObjectsHelper {

    private static List<GameObject> allTalkables = new List<GameObject>();

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

    public static List<GameObject> GetTalkables()
    {
        if (allTalkables.Count > 0)
        {
            return allTalkables;
        }
        
        GameObject[] all = (GameObject[])GameObject.FindObjectsOfType(typeof(GameObject));
        foreach (GameObject go in all) {
            if (go.GetComponent<Talkable>() != null)
            {
                allTalkables.Add(go);
            }
        }

        return allTalkables;
    }

    public static void AddTalkable(GameObject newTalkable)
    {
        allTalkables.Add(newTalkable);
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

	public static GameObject GetDialoguePannel() {
		GameObject bottomPanel = WorldObjectsHelper.getUIGO();
		return bottomPanel.transform.Find("DialoguePanel").gameObject;
	}
}
