using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;


public class Player : MonoBehaviour {

	private Vector2 targetPosition;
	private Vector2 xAxisOnlyPosition;
	private bool canMove = false;
	private float yOriginalPos;
	private PlayerStateMachine animStateMachine;
	private bool shouldPickUpItem;
	private GameObject itemToPickUp;

	public float PlayerMovementSpeed = 4.0f;


	// Use this for initialization
	void Start () {
		xAxisOnlyPosition = new Vector2();
		yOriginalPos = transform.position.y;

		animStateMachine = GetComponent<PlayerStateMachine>();

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0) || Input.GetMouseButton(0)) {
			targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);


			Collider2D hitCollider = Physics2D.OverlapPoint(targetPosition);
			if (hitCollider != null) {
				canMove = hitCollider.gameObject.tag != "InteractiveObject" && EventSystem.current.currentSelectedGameObject.tag != "IMButton";
			}
			else {
				if (EventSystem.current.currentSelectedGameObject != null) {
					canMove = EventSystem.current.currentSelectedGameObject.tag != "IMButton";	
				}
				else {
					canMove = true;
				}

			}

		}


		if (canMove) {
			xAxisOnlyPosition.x = targetPosition.x;
			xAxisOnlyPosition.y = yOriginalPos;
			transform.position = Vector2.MoveTowards(transform.position, xAxisOnlyPosition, Time.deltaTime * PlayerMovementSpeed);

			animStateMachine.SetState(PlayerStateMachine.PlayerStates.PlayerWalk);
		}

		if (transform.position.x == targetPosition.x && animStateMachine.GetCurrentState() != PlayerStateMachine.PlayerStates.PlayerIdle) {
			canMove = false;

			if (shouldPickUpItem) {
				animStateMachine.SetState(PlayerStateMachine.PlayerStates.PlayePickUp);

				PutItemInInventory(itemToPickUp);

				shouldPickUpItem = false;
			}
			else {
				animStateMachine.SetState(PlayerStateMachine.PlayerStates.PlayerIdle);
			}

		}
	}

	public void MoveTo(Vector2 newPosition) {
		targetPosition = newPosition;
		canMove = true;
	}

	public void MoveToAndPickUp(Vector2 newPosition, GameObject itemToPickUp) {
		this.shouldPickUpItem = true;
		this.itemToPickUp = itemToPickUp;

		MoveTo(newPosition);
	}

	public void PutItemInInventory(GameObject theItem) {
		// DO SOMETHING?
	}
}
