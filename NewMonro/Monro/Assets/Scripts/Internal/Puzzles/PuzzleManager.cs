using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class PuzzleManager : MonoBehaviour {

	private List<Puzzle> puzzleList;
	private PuzzleActionTracker actionsTracker;

	// Use this for initialization
	void Start () {
		actionsTracker = new PuzzleActionTracker();
		puzzleList = new List<Puzzle>();
		for (int i = 0; i < this.transform.childCount; i++) {
			Puzzle aPuzzle = this.transform.GetChild(i).GetComponent<Puzzle>();
			puzzleList.Add(aPuzzle);
		}
	}

	public void UpdatePuzzlesWithAction(PuzzleActionType action, Transform actionReceiver = null, Dictionary<string, string> extraData = null) {
		if (actionReceiver != null) {
			actionsTracker.AddAction(action, actionReceiver);
		}
		foreach (Puzzle p in puzzleList) {
			p.actionTracker = this.actionsTracker;
			p.UpdatePuzzleWithAction(action, actionReceiver, extraData);
		}
	}

	public static PuzzleActionType TransformActionButtonTypeToPuzzleActionType(IMActionButtonType buttonType) {
		switch(buttonType) {
		case IMActionButtonType.LookAt:
			return PuzzleActionType.LookAt;
		case IMActionButtonType.Pickup:
			return PuzzleActionType.PickUp;
		case IMActionButtonType.Talk:
			return PuzzleActionType.Talk;
		case IMActionButtonType.Use:
			return PuzzleActionType.Use;
			default:
			return PuzzleActionType.None;
		}
	}

	public static void UpdatePuzzleWithAction(PuzzleActionType action, Transform actionReceiver = null, Dictionary<string, string> extraData = null) {
		GameObject puzzleManager = WorldObjectsHelper.getPuzzleManagerGO();
		if (puzzleManager) {
			PuzzleManager pmgr = puzzleManager.GetComponent<PuzzleManager>();
			if (pmgr != null) {
				pmgr.UpdatePuzzlesWithAction(action, actionReceiver, extraData);	
			}
		}
	}


	[YarnCommand("DialogueAction")]
	public void DialogueAction(string action) {
		foreach (Puzzle p in puzzleList) {
			foreach (PAction pa in p.puzzleActions) {
				if (pa is ActionDialogueAction) {
					if ((pa as ActionDialogueAction).ActionName == action) {
						pa.ExecuteAction(PuzzleActionType.Dialogue);
					}
				}
			}
		}
	}
}
