using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PuzzleResolver : MonoBehaviour {

	public int puzzleStep = 0;
	public string puzzleId;
	public List<string> itemIds;
	public List<string> relatedItemsIds;
	public List<string> triggerIds;

	public abstract void PuzzleResolved(DBItem item);
	public abstract void PuzzleStateChanged(int newStep);
	public abstract void PuzzleNotResolvedButItemIsRelated(DBItem item);
	public abstract void PuzzleNotResolved(DBItem item);
}
