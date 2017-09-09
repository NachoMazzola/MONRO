using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactionAddInteractiveAction : IPReaction {

	public List<PuzzleActionType> ActionToAdd;
	public List<string> AddActionToIds;

	private InteractiveMenu menu;


	override public bool Execute(Transform actionReceiver, Puzzle puzzle, PAction theAction) {
		IMActionButton actionBtn = null;
		Transform actionBtnTransform = null;
		bool actionOk = true;
		foreach (string tId in AddActionToIds) {
			Transform obj = GetTransformFromId(tId);
			menu = obj.GetComponent<InteractiveMenu>();
			if (menu == null) {
				Debug.LogError("ERROR: WANT TO ADD ACTION INTO AN INTERACTIVE OBJECT THAT DOES NOT HAVE AN INTERACTIVE MENU -- :" ,actionReceiver);
				actionOk = false;
			}

			foreach (PuzzleActionType p in ActionToAdd) {
				switch(p) {
				case PuzzleActionType.Talk:

					IMBTalkAction talkComponent = menu.gameObject.GetComponent<IMBTalkAction>();
					if (talkComponent == null) {
						actionBtn = menu.gameObject.AddComponent<IMBTalkAction>();	
						actionBtnTransform = (Resources.Load("IMBTalkTo") as GameObject).transform;
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
						actionBtnTransform = (Resources.Load("IMBPickUp") as GameObject).transform;
					}

					break;

				case PuzzleActionType.Use:
					IMBUseAction useComponent = menu.gameObject.GetComponent<IMBUseAction>();
					if (useComponent == null) {
						actionBtn = menu.gameObject.AddComponent<IMBUseAction>();
						actionBtnTransform = (Resources.Load("IMBUse") as GameObject).transform;
					}

					break;
				}

				if (actionBtn != null && actionBtnTransform != null) {
					actionBtn.ButtonPrefab = actionBtnTransform;
					menu.AddButton(actionBtn);
				}
				else {
					actionOk = false;
					Debug.Log("PREFAB NOT FOUND BITCH");
				}	
			}
		}

		return actionOk;
	}
}
