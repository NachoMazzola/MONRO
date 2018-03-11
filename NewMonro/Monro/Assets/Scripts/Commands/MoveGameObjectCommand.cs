﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveGameObjectCommand : ICommand {

	private GameObject targetObject;
	private Vector2 targetPosition;
	private float movementSpeed;
	private bool willMoveToTheRight = false;

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
}
