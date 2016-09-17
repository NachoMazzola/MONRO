using UnityEngine;
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
		inventory = GetComponent<PlayerInventory> ();
		//playerCaption = GetComponent<PlayerCaption> ();

		currentFacingDirection = StartFacingRight ? MovingDirection.MovingRight : MovingDirection.MovingLeft;
		lastFacingDirection = currentFacingDirection;
	}

	override public void OnStart() {
		base.OnStart();

		xAxisOnlyPosition = new Vector2 ();
		yOriginalPos = transform.position.y;
	}

	override public void OnUpdate() {
		base.OnUpdate();

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



	override public void IWOTapped(Vector2 tapPos, GameObject other) {
		Debug.Log("TAP");
		if (animStateMachine.GetCurrentState () == PlayerStateMachine.PlayerStates.PlayerTalk) {
			return;
		}


		targetPosition = tapPos;

		DecideFacingDirection();

		canMove = other == null;

	}

	override public void IWOTapHold(Vector2 tapPos, GameObject other) {
		Debug.Log("HOLD TAP");
	}

	void DecideFacingDirection() {
		MovingDirection theMovingDirection = targetPosition.x > this.transform.position.x ? MovingDirection.MovingRight : MovingDirection.MovingLeft;
		if (theMovingDirection != currentFacingDirection) {

			Vector2 theScale = characterSprite.transform.localScale;
			theScale.x = characterSprite.transform.localScale.x * -1;
			characterSprite.transform.localScale = theScale;

			lastFacingDirection = currentFacingDirection;
			currentFacingDirection = theMovingDirection;
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
	
		float newX = moveToObj.position.x*moveToObj.transform.localScale.x + (theCollider.radius+0.5f)*dirChange; 

		Vector2 newPos = new Vector2(newX, moveToObj.position.y);

		targetPosition = newPos;
		DecideFacingDirection();
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
		Transform theCaption = base.GetConversationCaptionCanvas();
		Vector3 invertedScale = new Vector3(Mathf.Abs(theCaption.localScale.x), theCaption.localScale.y);
		theCaption.localScale = invertedScale;
	}
}
