using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddInteractiveAction : PReaction {

	public IMActionButtonType ActionToAdd;
	private InteractiveMenu menu;

	override public void ExecuteReaction(IMActionButtonType action, Transform actionReceiver) {
		if (action == ActionTrigger && actionReceiver == ActionReceiver) {
			IMActionButton actionBtn = null;
			Transform actionBtnTransform = null;
			menu = actionReceiver.GetComponent<InteractiveMenu>();
			if (menu == null) {
				Debug.LogError("ERROR: WANT TO ADD ACTION INTO AN INTERACTIVE OBJECT THAT DOES NOT HAVE AN INTERACTIVE MENU -- :" ,actionReceiver);
				return;
			}

			switch(ActionToAdd) {
			case IMActionButtonType.Talk:

				IMBTalkAction talkComponent = menu.gameObject.GetComponent<IMBTalkAction>();
				if (talkComponent == null) {
					actionBtn = menu.gameObject.AddComponent<IMBTalkAction>();	
					actionBtnTransform = (Resources.Load("IMBLookAt") as GameObject).transform;
				}

				break;

			case IMActionButtonType.LookAt:

				IMActionButton lookAtComponent = menu.gameObject.GetComponent<IMBLookAtAction>();
				if (lookAtComponent == null) {
					actionBtn = menu.gameObject.AddComponent<IMBLookAtAction>();
					actionBtnTransform = (Resources.Load("IMBLookAt") as GameObject).transform;
				}
				break;

			case IMActionButtonType.Pickup:
				IMBPickUpAction pUpComponent = menu.gameObject.GetComponent<IMBPickUpAction>();
				if (pUpComponent == null) {
					actionBtn = menu.gameObject.AddComponent<IMBPickUpAction>();
					actionBtnTransform = (Resources.Load("IMBLookAt") as GameObject).transform;
				}

				break;

			case IMActionButtonType.Use:
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

				if (IncrementSteps) {
					parent.IncrementPuzzleStep();	
				}

				ActionFinished();
			}
			else {
				Debug.Log("PREFAB NOT FOUND BITCH");
			}
		}
	}

	override public void ActionFinished() {
		Executed = true;
	}

}
