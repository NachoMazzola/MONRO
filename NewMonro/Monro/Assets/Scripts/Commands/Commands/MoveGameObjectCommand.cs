using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveGameObjectCommand : ICommand {

	public GameObject targetObject;
	public Vector2 targetPosition;
	public float movementSpeed;
	private bool willMoveToTheRight = false;

	public MoveGameObjectCommand() {
		
	}

	public MoveGameObjectCommand(GameObject target, Vector2 toPosition, float speed) {
		this.targetObject = target;
		this.targetPosition = toPosition;
		this.movementSpeed = speed;
	}

	public override void Prepare() {
		this.willMoveToTheRight = this.targetPosition.x > this.targetObject.transform.position.x;
	}

	public override void WillStart() {

	}

	public override void UpdateCommand() {
		Debug.Log("PLAYER POS: " + Mathf.RoundToInt(this.targetObject.transform.position.x));
		Debug.Log("VALKIRIE POS: " + Mathf.RoundToInt(this.targetPosition.x));


		if (Mathf.RoundToInt(this.targetObject.transform.position.x) == Mathf.RoundToInt(this.targetPosition.x)) {
			this.finished = true;
			return;
		}

		if (this.willMoveToTheRight) {
			this.targetObject.transform.position += new Vector3(1 * this.movementSpeed * Time.deltaTime, 0, 0);			
		}
		else {
			this.targetObject.transform.position -= new Vector3(1 * this.movementSpeed * Time.deltaTime, 0, 0);		
		}


	}

	public override bool Finished() {
		return finished;
	}

	public override CommandType GetCommandType() { 
		return CommandType.MoveGameObjectCommandType; 
	}
}
