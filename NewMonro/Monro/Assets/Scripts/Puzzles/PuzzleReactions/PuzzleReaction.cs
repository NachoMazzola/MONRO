using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PuzzleReaction : MonoBehaviour, PuzzleNotificationObserver {

	public PGOState reactOnState;
	public string PGOId;

	void Awake() {
		OnAwake();
	}

	// Use this for initialization
	void Start () {
		OnStart();
	}
	
	// Update is called once per frame
	void Update () {
		OnUpdate();
	}

	public virtual void OnAwake() {
	}

	public virtual void OnStart() {
	}

	public virtual void OnUpdate() {
	}

	public virtual void ExecuteReactionForGPOStateChange(PGOState newState, PGOState lastState, string pgoId) {}

}
