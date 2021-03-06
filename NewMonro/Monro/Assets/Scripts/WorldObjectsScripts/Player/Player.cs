﻿using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public struct StateTransitionData {
	public Character otherCharacter;
	public ArrayList otherCharacters; 

	public StateTransitionData(Character otherCharacter, ArrayList otherCharacters = null) {
		this.otherCharacter = otherCharacter;
		this.otherCharacters = otherCharacters;
	}
}


public class Player : Character
{
	[HideInInspector]
	public Vector2 targetPosition;
	[HideInInspector]
	public Transform itemToPickUp;

	[HideInInspector]
	public StateTransitionData stateTransitionData;


	private bool shouldPickUpItem;

	private PlayerCaption playerCaption;

	private bool willTalkToNPC;


	void Awake (){
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

	override public void OnAwake() {
		base.OnAwake();
		characterType = CharacterType.Player;
		animStateMachine = GetComponent<PlayerStateMachine> ();

		//playerCaption = GetComponent<PlayerCaption> ();

		ChangeToState(PlayerStateMachine.PlayerStates.PlayerIdle);
	}

	override public void OnStart() {
		base.OnStart();


	}

	override public void OnUpdate() {
		base.OnUpdate();

		currentState.StateUpdate();
	}
		
	override public void IWOTapped(Vector2 tapPos, GameObject other) {
		Debug.Log("TAP");
		if (animStateMachine.GetCurrentState () == PlayerStateMachine.PlayerStates.PlayerTalk) {
			return;
		}


		targetPosition = tapPos;
		canMove = other == null;

		if (other == null) {
			this.ChangeToState(PlayerStateMachine.PlayerStates.PlayerWalk);
			(this.currentState as StateWalk).optionalStateToTransitionOnEnd = PlayerStateMachine.PlayerStates.PlayerIdle;	
		}
	}

	override public void IWOTapHold(Vector2 tapPos, GameObject other) {
		Debug.Log("HOLD TAP");
	}
		
	public IEnumerator ShowCaption(string caption) {
		Transform theCaption = GetConversationCaptionCanvas();
		theCaption.gameObject.SetActive(true);

		PlayerCaption pCaption = theCaption.GetComponent<PlayerCaption>();
		return pCaption.ShowCaption(caption);
	}

	override public Transform GetConversationCaptionCanvas() {
		Transform theCaption = base.GetConversationCaptionCanvas();
		if (currentFacingDirection == MovingDirection.MovingLeft) {
			Vector3 invertedScale = new Vector3(theCaption.localScale.x*-1, theCaption.localScale.y);
			theCaption.localScale = invertedScale;
		}
		else {
			Vector3 invertedScale = new Vector3(Mathf.Abs(theCaption.localScale.x), theCaption.localScale.y);
			theCaption.localScale = invertedScale;
		}
		return theCaption;
	}

	/*
	 * This method is called when the PickUp animaiton ends. It is wired up from the Animaiton Panel in Inspector, hence the name AnimEndEvent 
	*/
	public void AnimEndEventPutItemInInventory () {
		ResetState();
	}
		
	override  public void ResetState() {
		currentState.StateEnd();
		ChangeToState(PlayerStateMachine.PlayerStates.PlayerIdle);

		Transform theCaption = base.GetConversationCaptionCanvas();
		Vector3 invertedScale = new Vector3(Mathf.Abs(theCaption.localScale.x), theCaption.localScale.y);
		theCaption.localScale = invertedScale;
	}


	override public void ChangeToState(PlayerStateMachine.PlayerStates newState) {

		if (currentState != null) {
			lastState = currentState;
		}

		switch(newState) {
		case PlayerStateMachine.PlayerStates.PlayerIdle:
			currentState = this.gameObject.AddComponent<StateIdle>();
			currentState.SetCharacterOwner(this);
			break;

		case PlayerStateMachine.PlayerStates.PlayerWalk:
			currentState = this.gameObject.AddComponent<StateWalk>();
			currentState.SetCharacterOwner(this);
			(currentState as StateWalk).SetupState(targetPosition);

			break;

		case PlayerStateMachine.PlayerStates.PlayePickUp:
			currentState = this.gameObject.AddComponent<StatePickUp>();
			currentState.SetCharacterOwner(this);

			break;

		case PlayerStateMachine.PlayerStates.PlayerTalk:
			currentState = this.gameObject.AddComponent<StateTalk>();
			currentState.SetCharacterOwner(this);

			break;
		}

		currentState.StateStart();
	}
}
