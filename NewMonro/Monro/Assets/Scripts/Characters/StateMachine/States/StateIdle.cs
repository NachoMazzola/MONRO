using UnityEngine;
using System.Collections;

public class StateIdle : State {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//animStateMachine.SetState (PlayerStateMachine.PlayerStates.PlayerIdle);
	}

	override public void OnAwake() {
		base.OnAwake();
	}

	override public void OnStart() {
		base.OnStart();
	}

	override public void OnUpdate() {
		base.OnUpdate();
	}


	public override void StateStart() {
		base.StateStart();
		animStateMachine.SetState(PlayerStateMachine.PlayerStates.PlayerIdle);
	}

	public override void StateUpdate() {
	}

	public override void StateEnd() {
		base.StateEnd();
	}
		
}


