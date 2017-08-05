using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour {

	private List<Puzzle> puzzleList;

	// Use this for initialization
	void Start () {
		puzzleList = new List<Puzzle>();
		for (int i = 0; i < this.transform.childCount; i++) {
			Puzzle aPuzzle = this.transform.GetChild(i).GetComponent<Puzzle>();
			puzzleList.Add(aPuzzle);
		}
	}

	public void UpdatePuzzlesWithAction(PuzzleActionType action, Transform actionReceiver = null, Dictionary<string, object> extraData = null) {
		foreach (Puzzle p in puzzleList) {
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

	public static void UpdatePuzzleWithAction(PuzzleActionType action, Transform actionReceiver = null, Dictionary<string, object> extraData = null) {
		GameObject puzzleManager = GameObject.Find("PuzzleManager");
		if (puzzleManager) {
			PuzzleManager pmgr = puzzleManager.GetComponent<PuzzleManager>();
			if (pmgr != null) {
				pmgr.UpdatePuzzlesWithAction(action, actionReceiver, extraData);	
			}
		}
	}
}
