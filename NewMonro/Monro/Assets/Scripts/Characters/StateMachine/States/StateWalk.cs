using UnityEngine;
using System.Collections;

public class StateWalk : State {

	private Vector2 targetPosition = Vector2.zero;
	private Vector2 xAxisOnlyPosition;
	private float yOriginalPos;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}
		
	public void SetupState(Vector2 walkToPosition) {
		targetPosition = walkToPosition;

	}

	public void SetupState(Vector2 walkToPosition, bool canMove, PlayerStateMachine.PlayerStates changeToStateOnEnd = PlayerStateMachine.PlayerStates.PlayerIdle) {
		targetPosition = walkToPosition;
		stateCharacterOwner.canMove = canMove;
		this.optionalStateToTransitionOnEnd = changeToStateOnEnd;

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
		animStateMachine.SetState(PlayerStateMachine.PlayerStates.PlayerWalk);
		xAxisOnlyPosition = new Vector2 ();
		yOriginalPos = stateCharacterOwner.transform.position.y;
	}

	public override void StateUpdate() {
		if (targetPosition == Vector2.zero) {
			return;
		}

		if (stateCharacterOwner.canMove && targetPosition != Vector2.zero) {

			DecideFacingDirection();

			xAxisOnlyPosition.x = targetPosition.x;
			xAxisOnlyPosition.y = yOriginalPos;
			stateCharacterOwner.transform.position = Vector2.MoveTowards (stateCharacterOwner.transform.position, xAxisOnlyPosition, Time.deltaTime * stateCharacterOwner.MovementSpeed);
		}

		if (stateCharacterOwner.canMove && stateCharacterOwner.transform.position.x == targetPosition.x) {
			StateEnd();
		}
	}

	public override void StateEnd() {
		stateCharacterOwner.canMove = false;

		StateTransitionData stData = (stateCharacterOwner as Player).stateTransitionData;
		if (stData.otherCharacter != null) {
			stData.otherCharacter.ResetState();
		}

		base.StateEnd();
	}

	void DecideFacingDirection() {
		Character.MovingDirection theMovingDirection = targetPosition.x > stateCharacterOwner.transform.position.x ? Character.MovingDirection.MovingRight : Character.MovingDirection.MovingLeft;
		if (theMovingDirection != stateCharacterOwner.currentFacingDirection) {
			SwapFacingDirectionTo(theMovingDirection);

		}
	}

	private void SwapFacingDirectionTo(Character.MovingDirection newFacingDir) {
		stateCharacterOwner.SwapFacingDirectionTo(newFacingDir);
	}

}
