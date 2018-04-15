using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovementController : MonoBehaviour {

	public GameObject ControlledGameObject;

	[HideInInspector]
	public bool movingRight;
	[HideInInspector]
	public bool movingLeft;
	[HideInInspector]
	public bool MovePlayer = true; 
	[HideInInspector]
	public float movementLimitRight;
	[HideInInspector]
	public float movementLimitLeft;

	private Moveable moveableGameObject;
	private SpriteRenderer moveableSpriteRenderer;

	private WorldInteractionController worldInteractionCtr;

	[HideInInspector]
	public Transform targetTransform;

	// Use this for initialization
	void Start () {
		this.worldInteractionCtr = WorldInteractionController.getComponent();
	
		if (this.ControlledGameObject != null) {
			this.moveableGameObject = ControlledGameObject.GetComponent<Moveable>();
			this.moveableSpriteRenderer = ControlledGameObject.GetComponent<SpriteRenderer>();
		}

		if (MovePlayer) {
			if (ControlledGameObject == null) {
				Debug.LogError("WARNING: CONTROLLER CAND FIND GAME OBJECT TO CONTROL!");
			}
		}

		this.StopMoving();
	}
	
	// Update is called once per frame
	void Update () {
		UpdateMovement();
	}

	public void UpdateMovement() {
		if (movingRight) {
			moveableGameObject.SwapFacingDirectionTo(Moveable.MovingDirection.MovingRight);
			if (MovePlayer) {
				if (moveableGameObject.transform.position.x + this.moveableSpriteRenderer.bounds.size.x/2 >= movementLimitRight) {
					StopMoving();
				}
				else {
					moveableGameObject.transform.position += new Vector3(1 * moveableGameObject.MovementSpeed * Time.deltaTime, 0, 0);		
				}

			}
		}
		else if (movingLeft) {
			moveableGameObject.SwapFacingDirectionTo(Moveable.MovingDirection.MovingLeft);
			if (MovePlayer) {
				if (moveableGameObject.transform.position.x - this.moveableSpriteRenderer.bounds.size.x/2 <= movementLimitLeft) {
					StopMoving();
				}
				else {
					moveableGameObject.transform.position -= new Vector3(1 * moveableGameObject.MovementSpeed * Time.deltaTime, 0, 0);		
				}
			}
		}
	}

	public void StartMovingRight() {
		if (worldInteractionCtr.enableInteractions == false) {
			return;
		}
			
		movingRight = true;
		movingLeft = false;

		//thePlayer.StartMoving(Character.MovingDirection.MovingRight);
	}

	public void StartMovingLeft() {
		if (worldInteractionCtr.enableInteractions == false) {
			return;
		}
			
		movingRight = false;
		movingLeft = true;

		//thePlayer.StartMoving(Character.MovingDirection.MovingLeft);
	}

	public void StopMoving() {
		movingLeft = false;
		movingRight = false;

		//thePlayer.StopMoving();
	}

	public bool IsMoving() {
		return movingLeft || movingRight;
	}

	public Moveable.MovingDirection GetMovingDirection() {
		return moveableGameObject.currentFacingDirection;
	}

	public float GetPlayerPosition() {
		return moveableGameObject.transform.position.x;
	}

	public float GetMovementSpeed() {
		return moveableGameObject.MovementSpeed;
	}
}
