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

	public int optionalDistanceFromTarget = 0;

	private MovementController movementController;


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
		GameObject inst = WorldObjectsHelper.InstantiatePrefabFromResources("MovementController", null);
		movementController = inst.GetComponent<MovementController>();
		movementController.SetMovementOptions(controlledGameObject: this.targetObject, targetDestination: this.targetPosition);
	}

	public override void WillStart() {}

	public override void UpdateCommand() {
		if (this.movementController == null) {
			return;
		}
		this.movementController.UpdateMovement();
		this.finished = this.movementController.reachedDestination;
	}

	public override void Stop() {
		this.movementController.SetMovementOptions(null, Vector2.zero);
		this.movementController = null;
	}

	public override bool Finished() {
		return finished;
	}

	public override CommandType GetCommandType() { 
		return CommandType.MoveGameObjectCommandType; 
	}
}
