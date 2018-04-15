using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moveable : MonoBehaviour {
	public float MovementSpeed;

	public enum MovingDirection {
		MovingRight,
		MovingLeft
	}

	public MovingDirection StartFacingDirection = MovingDirection.MovingRight;

	[HideInInspector]
	public MovingDirection lastFacingDirection;
	[HideInInspector]
	public MovingDirection currentFacingDirection;


	private MovementController movementController;

	void Awake() {
		this.movementController = WorldObjectsHelper.getMovementControllerGO().GetComponent<MovementController>();
		this.movementController.targetTransform = this.transform;
		this.movementController.movingLeft = this.StartFacingDirection == MovingDirection.MovingLeft;
		this.movementController.movingRight = this.StartFacingDirection == MovingDirection.MovingRight;

		this.currentFacingDirection = this.StartFacingDirection;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		this.movementController.UpdateMovement();
	}

	public void SwapFacingDirectionTo(MovingDirection newFacingDir) {
		if (newFacingDir == this.currentFacingDirection) {
			return;
		}

		float theScaleFacingRight = 1;
		float theScaleFacingLeft = -1;

		Vector2 theScale = this.transform.localScale;
		theScale.x = newFacingDir == MovingDirection.MovingRight ? theScaleFacingRight : theScaleFacingLeft;
		this.transform.localScale = theScale;

		lastFacingDirection = currentFacingDirection;
		currentFacingDirection = newFacingDir;
	}
}
