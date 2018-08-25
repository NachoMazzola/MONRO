using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MovementController : MonoBehaviour
{

	public GameObject ControlledGameObject;

	[HideInInspector]
	public bool movingRight = false;
	[HideInInspector]
	public bool movingLeft = false;

	[HideInInspector]
	public float movementLimitRight;
	[HideInInspector]
	public float movementLimitLeft;
	[HideInInspector]
	public Vector2 targetDestination = Vector2.zero;
	[HideInInspector]
	public bool reachedDestination = false;


	private Moveable moveableGameObject;
	private SpriteRenderer moveableSpriteRenderer;

	private WorldInteractionController worldInteractionCtr;

	private int optionalDistanceFromTarget = 2;

	// Use this for initialization
	void Start ()
	{
		this.SetMovingProperties();
	//	this.StopMoving ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		UpdateMovement ();
	}

	public void UpdateMovement ()
	{
		bool isStopped = movingLeft == false && movingRight == false;
		Vector3 move = isStopped ? new Vector2(0, 0) : new Vector2(movingLeft ? -1 : 1, 0);
		moveableGameObject.transform.position += move * moveableGameObject.MovementSpeed * Time.deltaTime;


		int directionModifier = movingLeft ? -1 : 1;
		int limitPosition = Mathf.RoundToInt(this.targetDestination.x) - this.optionalDistanceFromTarget * directionModifier;
		if (this.targetDestination != Vector2.zero) {
			if (Mathf.RoundToInt(this.moveableGameObject.transform.position.x) == Mathf.RoundToInt(limitPosition)) {
				this.StopMoving();
				this.reachedDestination = true;
				return;
			}	
		}

		if (movingRight) {
			moveableGameObject.SwapFacingDirectionTo (Moveable.MovingDirection.MovingRight);
			if (moveableGameObject.transform.position.x + this.moveableSpriteRenderer.bounds.size.x / 2 >= movementLimitRight) {
				StopMoving ();
			}
		} else if (movingLeft) {
			moveableGameObject.SwapFacingDirectionTo (Moveable.MovingDirection.MovingLeft);
			if (moveableGameObject.transform.position.x - this.moveableSpriteRenderer.bounds.size.x / 2 <= movementLimitLeft) {
				StopMoving ();
			}
		}
	}

	public void StartMovingRight ()
	{
		CommandManager mgr = CommandManager.getComponent();
		mgr.AbortCurrentCommand();

		if (worldInteractionCtr.enableInteractions == false) {
			return;
		}
			
		movingRight = true;
		movingLeft = false;
		this.reachedDestination = false;

		this.moveableGameObject.PlayAnimation ();
	}

	public void StartMovingLeft ()
	{
		CommandManager mgr = CommandManager.getComponent();
		mgr.AbortCurrentCommand();

		if (worldInteractionCtr.enableInteractions == false) {
			return;
		}
			
		movingRight = false;
		movingLeft = true;
		this.reachedDestination = false;

		this.moveableGameObject.PlayAnimation ();
	}

	public void StopMoving ()
	{
		movingLeft = false;
		movingRight = false;

		if (this.moveableGameObject != null) {
			this.moveableGameObject.StopAnimation ();	
		}
	}

	public bool IsMoving ()
	{
		return movingLeft || movingRight;
	}

	public Moveable.MovingDirection GetMovingDirection ()
	{
		return moveableGameObject.currentFacingDirection;
	}

	public float GetPlayerPosition ()
	{
		return moveableGameObject.transform.position.x;
	}

	public float GetMovementSpeed ()
	{
		return moveableGameObject.MovementSpeed;
	}

	public void SetMovementOptions(GameObject controlledGameObject, Vector2 targetDestination) {
		if (controlledGameObject == null) {
			this.ControlledGameObject = null;
			this.targetDestination = Vector2.zero;
			this.StopMoving();
			return;
		}

		this.ControlledGameObject = controlledGameObject;
		this.targetDestination = targetDestination;

		this.SetMovingProperties();

		this.movingLeft = this.targetDestination.x < moveableGameObject.transform.position.x;
		this.movingRight = !this.movingLeft;

		if (this.targetDestination != Vector2.zero) {
			this.movementLimitLeft = -99999;
			this.movementLimitRight = 99999;
		}
	}

	private void SetMovingProperties() {
		if (this.moveableGameObject == null) {
			if (this.ControlledGameObject != null) {
				this.moveableGameObject = ControlledGameObject.GetComponent<Moveable> ();
				this.moveableSpriteRenderer = ControlledGameObject.GetComponent<SpriteRenderer> ();
			} else {
				Debug.LogError ("WARNING: CONTROLLER CAND FIND GAME OBJECT TO CONTROL!");
			}
		}

		if (this.worldInteractionCtr == null) {
			this.worldInteractionCtr = WorldInteractionController.getComponent ();	
		}
	}



}