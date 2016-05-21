﻿using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;


public class Player : MonoBehaviour
{
	public enum MovingDirection
	{
		MovingRight,
		MovingLeft
	}

	public float PlayerMovementSpeed = 4.0f;
	public bool StartFacingRight = true;

	private MovingDirection currentFacingDirection;
	private MovingDirection lastFacingDirection;

	private Vector2 targetPosition;
	private Vector2 xAxisOnlyPosition;
	private bool canMove = false;
	private float yOriginalPos;
	private PlayerStateMachine animStateMachine;
	private bool shouldPickUpItem;
	private Transform itemToPickUp;
	private PlayerCaption playerCaption;


	private PlayerInventory inventory;


	void Awake ()
	{
		animStateMachine = GetComponent<PlayerStateMachine> ();
		inventory = GetComponent<PlayerInventory> ();
		playerCaption = GetComponent<PlayerCaption> ();

		currentFacingDirection = StartFacingRight ? MovingDirection.MovingRight : MovingDirection.MovingLeft;
		lastFacingDirection = currentFacingDirection;
	}

	// Use this for initialization
	void Start ()
	{
		xAxisOnlyPosition = new Vector2 ();
		yOriginalPos = transform.position.y;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetMouseButtonDown (0) || Input.GetMouseButton (0)) {
			targetPosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);

			currentFacingDirection = targetPosition.x > this.transform.position.x ? MovingDirection.MovingRight : MovingDirection.MovingLeft;
			if (currentFacingDirection != lastFacingDirection) {

				Vector2 theScale = this.transform.localScale;
				theScale.x = this.transform.localScale.x * -1;
				this.transform.localScale = theScale;

				lastFacingDirection = currentFacingDirection;

			}

			playerCaption.PreserveOriginalScale (this.transform.localScale.x);

			Collider2D hitCollider = Physics2D.OverlapPoint (targetPosition);

			//avoid moving the player if tapped in a HotSpot or a button in the HotSpot menu
			if (hitCollider != null && (hitCollider is BoxCollider2D && hitCollider != this.GetComponent<BoxCollider2D> ())) {
				canMove = hitCollider.gameObject.tag != "InteractiveObject" && EventSystem.current.currentSelectedGameObject.tag != "IMButton";
			} else {
				//avoid moving the player if tapped in any UI (like Inventory)
				if (EventSystem.current.currentSelectedGameObject != null) {
					canMove = EventSystem.current.currentSelectedGameObject.tag != "UIElement";	
				} else {
					canMove = true;
				}

			}





		}


		if (canMove) {
			
			xAxisOnlyPosition.x = targetPosition.x;
			xAxisOnlyPosition.y = yOriginalPos;
			transform.position = Vector2.MoveTowards (transform.position, xAxisOnlyPosition, Time.deltaTime * PlayerMovementSpeed);

			animStateMachine.SetState (PlayerStateMachine.PlayerStates.PlayerWalk);
		}

		if (transform.position.x == targetPosition.x && animStateMachine.GetCurrentState () != PlayerStateMachine.PlayerStates.PlayerIdle) {
			canMove = false;

			if (shouldPickUpItem) {
				animStateMachine.SetState (PlayerStateMachine.PlayerStates.PlayePickUp);

				shouldPickUpItem = false;
			} else {
				animStateMachine.SetState (PlayerStateMachine.PlayerStates.PlayerIdle);
			}

		}
	}

	public void MoveTo (Vector2 newPosition)
	{
		targetPosition = newPosition;
		canMove = true;
	}

	public void MoveToAndPickUp (Vector2 newPosition, Transform itemToPickUp)
	{
		this.shouldPickUpItem = true;
		this.itemToPickUp = itemToPickUp;

		MoveTo (newPosition);
	}

	/*
	 * This method is called when the PickUp animaiton ends. It is wired up from the Animaiton Panel in Inspector, hence the name AnimEndEvent 
	*/
	public void AnimEndEventPutItemInInventory ()
	{
		
		UIInventory theInv = GameObject.Find ("UIInventory").GetComponent<UIInventory> ();

		inventory.AddItem (itemToPickUp.GetComponent<InteractiveObject> ().GetComponent<Item> ());
		theInv.AddItemToInventory (itemToPickUp.GetComponent<InteractiveObject> ().Item);

		itemToPickUp.gameObject.SetActive (false);

		itemToPickUp = null;
	}
		
}