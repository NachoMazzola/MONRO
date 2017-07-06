using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovementController : MonoBehaviour {
	[HideInInspector]
	public bool movingRight;
	[HideInInspector]
	public bool movingLeft;

	private Player thePlayer;
	private Transform floor;

	/*
	 * TRUE => the player gets moved.
	 * FALSE => The floor gets moved.
	*/
	public bool MovePlayer = false; 

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
		else {
			floor = GameObject.Find("Floor").transform;	
			if (floor == null) {
				Debug.LogError("WARNING: CONTROLLER CAND FIND FLOOR TO CONTROL!");
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		UpdateMovement();
	}

	public void UpdateMovement() {
		//TODO: Hace que el piso una vez que el sultimo segmento esta dentro de la camara, ya no se deberia scrollear mas!
		//probablemente haya que mover el codigo del chequeo de bordes de la camara en ParallaxScrolling.cs a un script
		//propio dentro de la camara!!


		if (movingRight) {
			thePlayer.SwapFacingDirectionTo(Character.MovingDirection.MovingRight);
			if (MovePlayer) {
				thePlayer.transform.position += new Vector3(1 * thePlayer.MovementSpeed * Time.deltaTime, 0, 0);	
			}
			else {
				floor.transform.position -= new Vector3(1 * thePlayer.MovementSpeed * Time.deltaTime, 0, 0);	
			}
		}
		else if (movingLeft) {
			thePlayer.SwapFacingDirectionTo(Character.MovingDirection.MovingLeft);
			if (MovePlayer) {
				thePlayer.transform.position -= new Vector3(1 * thePlayer.MovementSpeed * Time.deltaTime, 0, 0);	
			}
			else {
				floor.transform.position += new Vector3(1 * thePlayer.MovementSpeed * Time.deltaTime, 0, 0);	
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
}
