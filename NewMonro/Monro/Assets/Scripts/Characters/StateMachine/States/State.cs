using UnityEngine;
using System.Collections;

public class State : MonoBehaviour, IState {

	protected PlayerStateMachine animStateMachine;
	public Character stateCharacterOwner;
	public PlayerStateMachine.PlayerStates optionalStateToTransitionOnEnd;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	virtual public void OnAwake() {
	}

	virtual public void OnStart() {
	}

	virtual public void OnUpdate() {
	}


	virtual public void StateStart() {
		Debug.Log("START STATE NAME -- " + this.GetType());

		animStateMachine = stateCharacterOwner.GetComponent<PlayerStateMachine> ();
		if (animStateMachine == null) {
			Debug.Log("No hay maquina de estado de animaciones asociada!");
		}
	}

	virtual public void StateUpdate() {
		
	}

	virtual public void StateEnd() {
		Debug.Log("END STATE NAME -- " + this.GetType());

		if (optionalStateToTransitionOnEnd != PlayerStateMachine.PlayerStates.PlayerNone) {
			stateCharacterOwner.ChangeToState(optionalStateToTransitionOnEnd);
		}
	}

	public Character GetCharacterOwner() {
		return stateCharacterOwner;
	}

	public void SetCharacterOwner(Character owner) {
		stateCharacterOwner = owner;
	}
}
