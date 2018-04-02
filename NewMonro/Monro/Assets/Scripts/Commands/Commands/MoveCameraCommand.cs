using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCameraCommand : ICommand {

	public Vector3 moveToTarget = new Vector3();
	public Vector3 fromPosition;
	public float movementSpeed;


	private Transform cameraObj;
	private Bounds screenBounds;

	public MoveCameraCommand() {
		
	}

	public MoveCameraCommand(Vector2 to, Vector2 from, float atSpeed) {
		this.moveToTarget = to;
		this.movementSpeed = atSpeed;
		this.fromPosition = from;

		this.shouldActivateCameraFollow(false);
	}

	public override void Prepare() {
		this.cameraObj = WorldObjectsHelper.getMainCamera().transform;
		if (this.cameraObj != null) {
			this.cameraObj.transform.position = new Vector2(this.fromPosition.x, this.cameraObj.transform.position.y);
		}
	}
		
	public override void WillStart() {

	}

	public override void UpdateCommand ()
	{
		var newPosition = Vector3.Lerp (this.cameraObj.position, moveToTarget, movementSpeed * Time.deltaTime);

		//newPosition.x = Mathf.Clamp (newPosition.x, minPosition, maxPosition);
		newPosition.y = this.cameraObj.position.y;
		newPosition.z = this.cameraObj.position.z;

		this.cameraObj.position = newPosition;

		if (Mathf.RoundToInt(this.cameraObj.position.x) == Mathf.RoundToInt(this.moveToTarget.x)) {
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
