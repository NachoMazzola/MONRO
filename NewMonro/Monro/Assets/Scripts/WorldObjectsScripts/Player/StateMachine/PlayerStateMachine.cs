﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerStateMachine : MonoBehaviour {

	public enum PlayerStates {
		PlayerIdle,
		PlayerWalk,
		PlayePickUp,
		PlayerTalk
	}

	private PlayerStates currentState;
	private PlayerStates lastState;
	private Animator stateMachineAnimator;
	private Dictionary<int, PlayerStates> playerStatesDict;

	private const string animParamIsWalking = "isMoving";
	private const string animParamIsPickingUp = "isPickingUp";

	void Awake() {
		currentState = PlayerStates.PlayerIdle;
		stateMachineAnimator = GetComponent<Animator>();
		playerStatesDict = new Dictionary<int, PlayerStates>();

		if (stateMachineAnimator == null) {
			Debug.LogError("State Machine Missing!");
		}
	}


	// Use this for initialization
	void Start () {
		foreach (PlayerStates state in (PlayerStates[])System.Enum.GetValues(typeof(PlayerStates)))
		{
			playerStatesDict.Add(Animator.StringToHash("Base Layer." + state.ToString()), state);
		}
	}
	
	// Update is called once per frame
	void Update () {
		


	}

	public void SetState(PlayerStates newState) {
		lastState = currentState;
		currentState = newState;

		switch(newState) {
		case PlayerStates.PlayerIdle: 
			stateMachineAnimator.SetBool(animParamIsWalking, false);
			stateMachineAnimator.SetBool(animParamIsPickingUp, false);
			break;

		case PlayerStates.PlayerWalk:
			stateMachineAnimator.SetBool(animParamIsWalking, true);
			stateMachineAnimator.SetBool(animParamIsPickingUp, false);

			break;

		case PlayerStates.PlayePickUp:
			stateMachineAnimator.SetBool(animParamIsWalking, false);
			stateMachineAnimator.SetBool(animParamIsPickingUp, true);

			break;

		}
	}

	public PlayerStates GetCurrentState() {
		return currentState;
	}
}
