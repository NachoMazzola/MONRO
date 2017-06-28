using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovementController : MonoBehaviour {
	
	bool movingRight;
	bool movingLeft;
	Player thePlayer;

	// Use this for initialization
	void Start () {
		GameObject playerObj = GameObject.Find("PlayerViking");
		if (playerObj) {
			thePlayer = playerObj.GetComponent<Player>();
		}

		if (thePlayer == null) {
			Debug.LogError("WARNING: CONTROLLER CAND FIND PLAYER TO CONTROL!");
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (movingRight) {
			thePlayer.SwapFacingDirectionTo(Character.MovingDirection.MovingRight);
			thePlayer.transform.position += new Vector3(1 * thePlayer.MovementSpeed * Time.deltaTime, 0, 0);
		}
		else if (movingLeft) {
			thePlayer.SwapFacingDirectionTo(Character.MovingDirection.MovingLeft);
			thePlayer.transform.position -= new Vector3(1 * thePlayer.MovementSpeed * Time.deltaTime, 0, 0);
		}
	}

	public void StartMovingRight() {
		movingRight = true;
		movingLeft = false;

		thePlayer.ChangeToState(PlayerStateMachine.PlayerStates.PlayerWalk);
	}

	public void StartMovingLeft() {
		movingRight = false;
		movingLeft = true;
		thePlayer.ChangeToState(PlayerStateMachine.PlayerStates.PlayerWalk);
	}

	public void StopMoving() {
		movingLeft = false;
		movingRight = false;

		thePlayer.ChangeToState(PlayerStateMachine.PlayerStates.PlayerIdle);
	}


}
