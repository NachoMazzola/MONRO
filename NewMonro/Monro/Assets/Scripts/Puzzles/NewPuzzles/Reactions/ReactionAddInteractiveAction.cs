using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactionAddInteractiveAction : MonoBehaviour, IPReaction {

	public PuzzleActionType ActionToAdd;
	public Transform AddActionTo;

	private InteractiveMenu menu;


	public bool Execute(Transform actionReceiver, Puzzle puzzle, PAction theAction) {
		IMActionButton actionBtn = null;
		Transform actionBtnTransform = null;
		menu = AddActionTo.GetComponent<InteractiveMenu>();
		if (menu == null) {
			Debug.LogError("ERROR: WANT TO ADD ACTION INTO AN INTERACTIVE OBJECT THAT DOES NOT HAVE AN INTERACTIVE MENU -- :" ,actionReceiver);
			return false;
		}

		switch(ActionToAdd) {
		case PuzzleActionType.Talk:

			IMBTalkAction talkComponent = menu.gameObject.GetComponent<IMBTalkAction>();
			if (talkComponent == null) {
				actionBtn = menu.gameObject.AddComponent<IMBTalkAction>();	
				actionBtnTransform = (Resources.Load("IMBLookAt") as GameObject).transform;
			}

			break;

		case PuzzleActionType.LookAt:

			IMActionButton lookAtComponent = menu.gameObject.GetComponent<IMBLookAtAction>();
			if (lookAtComponent == null) {
				actionBtn = menu.gameObject.AddComponent<IMBLookAtAction>();
				actionBtnTransform = (Resources.Load("IMBLookAt") as GameObject).transform;
			}
			break;

		case PuzzleActionType.PickUp:
			IMBPickUpAction pUpComponent = menu.gameObject.GetComponent<IMBPickUpAction>();
			if (pUpComponent == null) {
				actionBtn = menu.gameObject.AddComponent<IMBPickUpAction>();
				actionBtnTransform = (Resources.Load("IMBLookAt") as GameObject).transform;
			}

			break;

		case PuzzleActionType.Use:
			IMBUseAction useComponent = menu.gameObject.GetComponent<IMBUseAction>();
			if (useComponent == null) {
				actionBtn = menu.gameObject.AddComponent<IMBUseAction>();
				actionBtnTransform = (Resources.Load("IMBLookAt") as GameObject).transform;
			}

			break;
		}

		if (actionBtn != null && actionBtnTransform != null) {
			actionBtn.ButtonPrefab = actionBtnTransform;
			menu.AddButton(actionBtn);

			return true;
		}
		else {
			Debug.Log("PREFAB NOT FOUND BITCH");
		}

		return false;
	}
}
