using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface PuzzleNotificationObserver {
	void ExecuteReactionForGPOStateChange(PGOState newState, PGOState lastState, string pgoId);
	//PGOReaction GetPuzzleReactionComponent()
}
