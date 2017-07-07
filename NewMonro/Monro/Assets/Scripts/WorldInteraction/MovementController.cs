using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovementController : MonoBehaviour {
	[HideInInspector]
	public bool movingRight;
	[HideInInspector]
	public bool movingLeft;
	[HideInInspector]
	public bool MovePlayer = true; 

	public float movementLimitRight;
	public float movementLimitLeft;

	private Player thePlayer;


	[HideInInspector]
	public Transform targetTransform;

	// Use this for initialization
	void Start () {

		GameObject playerObj = GameObject.Find("PlayerViking");
		if (playerObj) {
			thePlayer = playerObj.GetComponent<Player>();
		}

		if (MovePlayer) {
			if (thePlayer == null) {
				Debug.LogError("WARNING: CONTROLLER CAND FIND PLAYER TO CONTROL!");
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		UpdateMovement();
	}

	public void UpdateMovement() {
		
		if (movingRight) {
			thePlayer.SwapFacingDirectionTo(Character.MovingDirection.MovingRight);
			if (MovePlayer) {
				if (thePlayer.transform.position.x + thePlayer.characterSprite.bounds.size.x/2 >= movementLimitRight) {
					StopMoving();
				}
				else {
					thePlayer.transform.position += new Vector3(1 * thePlayer.MovementSpeed * Time.deltaTime, 0, 0);		
				}

			}
		}
		else if (movingLeft) {
			thePlayer.SwapFacingDirectionTo(Character.MovingDirection.MovingLeft);
			if (MovePlayer) {
				if (thePlayer.transform.position.x - thePlayer.characterSprite.bounds.size.x/2 <= movementLimitLeft) {
					StopMoving();
				}
				else {
					thePlayer.transform.position -= new Vector3(1 * thePlayer.MovementSpeed * Time.deltaTime, 0, 0);		
				}
			}
		}
	}

	public void StartMovingRight() {
		movingRight = true;
		movingLeft = false;

		thePlayer.StartMoving(Character.MovingDirection.MovingRight);
	}

	public void StartMovingLeft() {
		movingRight = false;
		movingLeft = true;

		thePlayer.StartMoving(Character.MovingDirection.MovingLeft);
	}

	public void StopMoving() {
		movingLeft = false;
		movingRight = false;

		thePlayer.StopMoving();
	}

	public bool IsMoving() {
		return movingLeft || movingRight;
	}

	public Character.MovingDirection GetMovingDirection() {
		return thePlayer.currentFacingDirection;
	}

	public float GetPlayerPosition() {
		return thePlayer.transform.position.x;
	}

	public float GetMovementSpeed() {
		return thePlayer.MovementSpeed;
	}
}
