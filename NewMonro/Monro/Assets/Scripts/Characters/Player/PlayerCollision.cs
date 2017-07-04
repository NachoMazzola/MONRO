using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour {

	private Player thePlayer;
	private MovementController movementController;

	void Start() {
		thePlayer = this.GetComponent<Player>();
		movementController = GameObject.Find("MovementController").GetComponent<MovementController>();
	}


	void OnTriggerEnter2D(Collider2D other) {
		if (other is CircleCollider2D) {
			if (other.transform == movementController.targetTransform && 
				((State)thePlayer.currentState).optionalStateToTransitionOnEnd == PlayerStateMachine.PlayerStates.PlayerTalk) {
				movementController.StopMoving();
			}
		}
		else if (other is BoxCollider2D) {
			if (other.transform == movementController.targetTransform) {
				movementController.StopMoving();
			}	
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		
	}
}
