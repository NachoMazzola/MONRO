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
	private Vector2 targetDestination = Vector2.zero;
	[HideInInspector]
	public bool reachedDestination = false;


	private Moveable moveableGameObject;
	private SpriteRenderer moveableSpriteRenderer;

	private WorldInteractionController worldInteractionCtr;

	private float distanceThresholdAdjustment = 0.02f;

	// Use this for initialization
	void Start ()
	{
		this.SetMovingProperties ();
		//	this.StopMoving ();
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		UpdateMovement ();
	}

	public void UpdateMovement ()
	{
		bool isStopped = movingLeft == false && movingRight == false;
		if (isStopped) {
			return;
		}

		Vector3 move = isStopped ? new Vector2 (0, 0) : new Vector2 (movingLeft ? -1 : 1, 0);
		moveableGameObject.Move(move);

	
		float minDistanceThreshold = Mathf.Abs(targetDestination.x) - this.distanceThresholdAdjustment;
		float maxDistanceThreshold = Mathf.Abs(targetDestination.x) + this.distanceThresholdAdjustment;
		float pos = moveableGameObject.transform.position.x;

		if (this.targetDestination != Vector2.zero) {
			//Debug.Log ("Target POS : " + targetDestination.x + " Monro Pos T: " + pos + " Min T: " + minDistanceThreshold + " Max T: " + maxDistanceThreshold);

			bool isInStopPositionThreshold = false;
			if (this.movingLeft) {
				isInStopPositionThreshold = pos < maxDistanceThreshold && pos > minDistanceThreshold || (pos < maxDistanceThreshold && pos < minDistanceThreshold);;
			}
			else {
				isInStopPositionThreshold = pos > minDistanceThreshold && pos < maxDistanceThreshold || (pos > maxDistanceThreshold && pos > minDistanceThreshold);
			}

			//Debug.Log ("POS : " + pos + " Min T: " + minDistanceThreshold + " Max T: " + maxDistanceThreshold);
			if (this.reachedDestination == false && isInStopPositionThreshold) {
				this.reachedDestination = true;
				StopMoving ();
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
		CommandManager mgr = CommandManager.getComponent ();
		mgr.AbortCurrentCommand ();

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
		CommandManager mgr = CommandManager.getComponent ();
		mgr.AbortCurrentCommand ();

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

		if (this.targetDestination != Vector2.zero) {
//			float movablePos = Mathf.Abs(this.moveableGameObject.transform.position.x);
//			float destinyPos = Mathf.Abs(this.targetDestination.x);

			Vector3 moveableViewportToScreen = Camera.main.WorldToScreenPoint(this.moveableGameObject.transform.position);
			Vector3 destinyToScreenCoords = Camera.main.WorldToScreenPoint(this.targetDestination); 

			bool playerIsStadingRightToDestinationObject = Mathf.RoundToInt(moveableViewportToScreen.x) > Mathf.RoundToInt(destinyToScreenCoords.x);

			Moveable.MovingDirection directionToFace = playerIsStadingRightToDestinationObject ? Moveable.MovingDirection.MovingLeft : Moveable.MovingDirection.MovingRight;
			this.moveableGameObject.SwapFacingDirectionTo(directionToFace);
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

	public void SetMovementOptions (GameObject controlledGameObject, Vector2 targetDestination)
	{
		if (controlledGameObject == null) {
			this.ControlledGameObject = null;
			this.targetDestination = Vector2.zero;
			this.StopMoving ();
			return;
		}

		/**
		 * FIJATTE QUE LA TALK POSITION ESTA EN ESPACIO COORDENADO RELATIVO AL PADRE!!
		 * HABRIA QUE VER DE CONVERTIRLA PARA HACER LOS CHEQUEOS!!
		*/

		this.ControlledGameObject = controlledGameObject;
		this.SetMovingProperties ();
		this.targetDestination = targetDestination;

		this.movingLeft = targetDestination.x < moveableGameObject.transform.position.x;
		this.movingRight = !this.movingLeft;

		if (this.targetDestination != Vector2.zero) {
			this.movementLimitLeft = -99999;
			this.movementLimitRight = 99999;
		}
	}

	private void SetMovingProperties ()
	{
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