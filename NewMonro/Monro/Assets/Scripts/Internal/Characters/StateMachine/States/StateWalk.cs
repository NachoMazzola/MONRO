using UnityEngine;
using System.Collections;

public class StateWalk : State {

	private Transform targetTransform;
	private MovementController movementController;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	}
		
	public void SetupState(Vector2 walkToPosition) {
		
	}

	public void SetupState(Transform walkToTransform, bool canMove, PlayerStateMachine.PlayerStates changeToStateOnEnd = PlayerStateMachine.PlayerStates.PlayerIdle) {
		targetTransform = walkToTransform;
		stateCharacterOwner.canMove = canMove;
		this.optionalStateToTransitionOnEnd = changeToStateOnEnd;

		DecideFacingDirection();

		movementController = WorldObjectsHelper.getMovementControllerGO().GetComponent<MovementController>();
		movementController.targetTransform = walkToTransform;
		movementController.movingLeft = stateCharacterOwner.currentFacingDirection == Character.MovingDirection.MovingLeft;
		movementController.movingRight = stateCharacterOwner.currentFacingDirection == Character.MovingDirection.MovingRight;
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
	}

	public override void StateUpdate() {
		if (!targetTransform) {
			return;
		}

		if (movementController != null) {
			movementController.UpdateMovement();
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
		if (!targetTransform) {
			return;
		}

		Character.MovingDirection theMovingDirection = targetTransform.position.x > stateCharacterOwner.transform.position.x ? Character.MovingDirection.MovingRight : Character.MovingDirection.MovingLeft;
		if (theMovingDirection != stateCharacterOwner.currentFacingDirection) {
			SwapFacingDirectionTo(theMovingDirection);

		}
	}

	private void SwapFacingDirectionTo(Character.MovingDirection newFacingDir) {
		stateCharacterOwner.SwapFacingDirectionTo(newFacingDir);
	}

}
