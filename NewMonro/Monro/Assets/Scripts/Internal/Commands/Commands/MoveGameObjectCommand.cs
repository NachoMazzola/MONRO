using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MoveGameObjectCommandParameters: ICommandParamters {
	public GameObject targetObject;
	public Vector2 targetPosition;
	public float movementSpeed;


	SerializeField slz_targetObject;

	public CommandType GetCommandType() {
		return CommandType.MoveGameObjectCommandType;
	}
}


public class MoveGameObjectCommand : ICommand {

	public GameObject targetObject;
	public Vector2 targetPosition;
	public float movementSpeed;

	private bool willMoveToTheRight = false;
	private Moveable movedGameObj;

	public MoveGameObjectCommand() {
		
	}

	public MoveGameObjectCommand(ICommandParamters parameters) {
		MoveGameObjectCommandParameters mgo = (MoveGameObjectCommandParameters)parameters;
		this.targetObject = mgo.targetObject;
		this.targetPosition = mgo.targetPosition;
		this.movementSpeed = mgo.movementSpeed;
	}

	public MoveGameObjectCommand(GameObject target, Vector2 toPosition, float speed) {
		this.targetObject = target;
		this.targetPosition = toPosition;
		this.movementSpeed = speed;
	}

	public override void Prepare() {
		this.movedGameObj = this.targetObject.GetComponent<Moveable>();
		Debug.Assert(this.movedGameObj != null, this.targetObject + ": You are trying to move an object that does not have Moveable trait. Add Moveable component trait to it.");

		this.willMoveToTheRight = this.targetPosition.x > this.targetObject.transform.position.x;
	}

	public override void WillStart() {
		this.movedGameObj.PlayAnimation();
	}

	public override void UpdateCommand() {
		if (Mathf.RoundToInt(this.targetObject.transform.position.x) == Mathf.RoundToInt(this.targetPosition.x)) {
			this.finished = true;
			this.movedGameObj.StopAnimation();
			return;
		}

		if (this.willMoveToTheRight) {
			this.movedGameObj.SwapFacingDirectionTo(Moveable.MovingDirection.MovingRight);
			this.targetObject.transform.position += new Vector3(1 * this.movementSpeed * Time.deltaTime, 0, 0);			
		}
		else {
			this.movedGameObj.SwapFacingDirectionTo(Moveable.MovingDirection.MovingLeft);
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
