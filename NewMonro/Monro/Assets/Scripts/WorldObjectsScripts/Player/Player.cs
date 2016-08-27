﻿using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;


public class Player : Character
{
	public enum MovingDirection
	{
		MovingRight,
		MovingLeft
	}

	public float PlayerMovementSpeed = 4.0f;
	public bool StartFacingRight = true;

	[HideInInspector]
	public PlayerStateMachine animStateMachine;

	private MovingDirection currentFacingDirection;
	private MovingDirection lastFacingDirection;

	private Vector2 targetPosition;
	private Vector2 xAxisOnlyPosition;
	private bool canMove = false;
	private float yOriginalPos;


	private bool shouldPickUpItem;
	private Transform itemToPickUp;
	private PlayerCaption playerCaption;

	private bool willTalkToNPC;

	private PlayerInventory inventory;


	void Awake ()
	{
		animStateMachine = GetComponent<PlayerStateMachine> ();
		inventory = GetComponent<PlayerInventory> ();
		//playerCaption = GetComponent<PlayerCaption> ();

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
		if (Input.GetMouseButtonDown(0) || Input.GetMouseButton (0)) {
			if (animStateMachine.GetCurrentState () == PlayerStateMachine.PlayerStates.PlayerTalk) {
				return;
			}


			targetPosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);

			currentFacingDirection = targetPosition.x > this.transform.position.x ? MovingDirection.MovingRight : MovingDirection.MovingLeft;
			if (currentFacingDirection != lastFacingDirection) {

				Vector2 theScale = this.transform.localScale;
				theScale.x = this.transform.localScale.x * -1;
				this.transform.localScale = theScale;

				lastFacingDirection = currentFacingDirection;
			}

			//playerCaption.PreserveOriginalScale (this.transform.localScale.x);

			Collider2D[] hitColliders = Physics2D.OverlapPointAll(targetPosition); 
			Collider2D hitCollider = null;
			if (hitColliders != null && hitColliders.Length == 2) {
				hitCollider = hitColliders[0].gameObject.GetComponent<InteractiveObject>().GetTappbleCollider();

				//avoid moving the player if tapped in a HotSpot or a button in the HotSpot menu
				if (hitCollider != null && hitCollider.gameObject != this.gameObject) {
					canMove = hitCollider.gameObject.tag != "InteractiveObject" && EventSystem.current.currentSelectedGameObject.tag != "IMButton";
					canMove = hitCollider.isTrigger; //we re check here because we may have clicked in the circle collider of a HotSpot.. in this case, we must move the player there
				}
			}
			else {
				//avoid moving the player if tapped in any UI (like Inventory)
				EventSystem c = EventSystem.current;
				GameObject f = c.gameObject;
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

		if (canMove && transform.position.x == targetPosition.x && animStateMachine.GetCurrentState () != PlayerStateMachine.PlayerStates.PlayerIdle) {
			canMove = false;

			if (shouldPickUpItem) {
				animStateMachine.SetState (PlayerStateMachine.PlayerStates.PlayePickUp);

				shouldPickUpItem = false;

				Debug.Log("Player state: " + animStateMachine.GetCurrentState ());
			}
			else if (willTalkToNPC) {
				animStateMachine.SetState (PlayerStateMachine.PlayerStates.PlayerTalk);
				willTalkToNPC = false;

			}else {
				animStateMachine.SetState (PlayerStateMachine.PlayerStates.PlayerIdle);
			}

		}
	}

	public void MoveTo (Vector2 newPosition)
	{
		targetPosition = newPosition;
		canMove = true;
	}

	public void MoveToKeepDistance(Transform moveToObj) {

		CircleCollider2D theCollider = moveToObj.GetComponent<CircleCollider2D>();
		int dirChange = 1; 
		if (currentFacingDirection == MovingDirection.MovingRight) {
			dirChange = -1;
		}
	
		float newX = moveToObj.position.x + (theCollider.radius+0.5f)*dirChange; 

		Vector2 newPos = new Vector2(newX, moveToObj.position.y);

		targetPosition = newPos;
		canMove = true;
	}

	public void GoTalkToNPC(Transform NPC) {
		MoveToKeepDistance(NPC);
		willTalkToNPC = true;
	}


	public void MoveToAndPickUp (Vector2 newPosition, Transform itemToPickUp)
	{
		this.shouldPickUpItem = true;
		this.itemToPickUp = itemToPickUp;

		MoveTo (newPosition);
	}

	public void ShowCaption(string caption) {
		Transform theCaption = GetConversationCaptionCanvas();
		theCaption.gameObject.SetActive(true);

		PlayerCaption pCaption = theCaption.GetComponent<PlayerCaption>();
		pCaption.ShowCaption(caption);
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

		ResetState();
	}
		
	override  public void ResetState() {
		animStateMachine.SetState (PlayerStateMachine.PlayerStates.PlayerIdle);
	}
}
