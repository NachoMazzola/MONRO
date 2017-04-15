using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleReactionChangeSprite : PuzzleReaction {

	public override void OnAwake() {
		base.OnAwake();
	}

	public override void OnStart() {
		base.OnStart();
	}

	public override void OnUpdate() {
		base.OnUpdate();
	}

	public override void ExecuteReactionForGPOStateChange(PGOState newState, PGOState lastState, string pgoId) {
		if (newState == PGOState.Initial) {
			Debug.Log("LALALALAALALALALALAAL");
		}
	}


}	
