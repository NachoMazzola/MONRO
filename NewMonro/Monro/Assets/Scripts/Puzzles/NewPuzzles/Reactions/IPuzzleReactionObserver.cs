using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPuzzleReactionObserver  {
	void UpdatedState(Puzzle updatedPuzzle);
}