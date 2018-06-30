using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct MoveCameraCommandParameters: ICommandParamters {
	public Vector3 moveToTarget;
	public Vector3 fromPosition;
	public float movementSpeed;

	public Transform origin;
	public Transform destiny;

	public CommandType GetCommandType() {
		return CommandType.MoveCameraCommandType;
	}
}


public class MoveCameraCommand : ICommand {

	public Vector3 moveToTarget = new Vector3();
	public Vector3 fromPosition;
	public float movementSpeed;

	public Transform origin;
	public Transform destiny;

	private Transform cameraObj;
	private Bounds screenBounds;

	public MoveCameraCommand() {
		
	}

	public MoveCameraCommand(ICommandParamters parameters) {
		MoveCameraCommandParameters m = (MoveCameraCommandParameters)parameters;
		this.moveToTarget = m.moveToTarget;
		this.fromPosition = m.fromPosition;
		this.origin = m.origin;
		this.destiny = m.destiny;

		this.movementSpeed = m.movementSpeed;
	}

	public MoveCameraCommand(Vector2 to, Vector2 from, float atSpeed) {
		this.moveToTarget = to;
		this.movementSpeed = atSpeed;
		this.fromPosition = from;

		this.shouldActivateCameraFollow(false);
	}

	public MoveCameraCommand(Transform origin, Transform destiny, float atSpeed) {
		this.origin = origin;
		this.movementSpeed = atSpeed;
		this.destiny = destiny;

		this.cameraObj.position = origin.position;

		this.shouldActivateCameraFollow(false);
	}

	public override void Prepare() {
		this.cameraObj = WorldObjectsHelper.getMainCamera().transform;
		if (this.cameraObj != null) {
			if (this.origin != null) {
				this.cameraObj.position = this.origin.position;
			}
			else {
				this.cameraObj.transform.position = new Vector2(this.fromPosition.x, this.cameraObj.transform.position.y);	
			}
		}
	}
		
	public override void WillStart() {

	}

	public override void UpdateCommand ()
	{
		var newPosition = Vector3.Lerp (this.cameraObj.position, moveToTarget, movementSpeed * Time.deltaTime);
		if (this.origin != null && this.destiny != null) {
			newPosition = Vector3.Lerp (this.cameraObj.position, this.destiny.transform.position, movementSpeed * Time.deltaTime);
		}
			
		//newPosition.x = Mathf.Clamp (newPosition.x, minPosition, maxPosition);
		newPosition.y = this.cameraObj.position.y;
		newPosition.z = this.cameraObj.position.z;

		this.cameraObj.position = newPosition;

		Vector3 targetPos = this.destiny != null ? this.destiny.position : this.moveToTarget;

		if (Mathf.RoundToInt(this.cameraObj.position.x) == Mathf.RoundToInt(targetPos.x)) {
			this.finished = true;
			this.shouldActivateCameraFollow(true);
		}
	}

	public override bool Finished() {
		return finished;
	}

	private void shouldActivateCameraFollow(bool activate) {
		CameraFollow cf = Camera.main.GetComponent<CameraFollow>();
		cf.enabled = activate;
	}

	public override CommandType GetCommandType() { 
		return CommandType.MoveCameraCommandType; 
	}
}
