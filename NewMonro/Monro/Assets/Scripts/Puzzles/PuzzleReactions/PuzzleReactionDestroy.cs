using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleReactionDestroy : PuzzleReaction {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

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
		Destroy(this.gameObject);
	}

}
