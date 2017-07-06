﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxScrolling : MonoBehaviour
{

	public List<Transform> backLayers;
	public float backLayersSpeed = 0.5f;

	public List<Transform> middleLayers;
	public float middleLayersSpeed = 1.0f;

	public List<Transform> frontLayers;
	public float frontLayersSpeed = 1.5f;

	private MovementController movementController;

	// Use this for initialization
	void Start ()
	{
		movementController = GameObject.Find ("MovementController").GetComponent<MovementController> ();
		if (movementController == null) {
			Debug.LogError ("PARALLAX: NO MOVEMENT CONTROLLER FOUND IN SCENE!");
		}
	}

	// Update is called once per frame
	void Update ()
	{
		if (movementController.IsMoving ()) {
			foreach (Transform backLayer in backLayers) {
				MoveLayer (backLayer, backLayersSpeed);
			}

			foreach (Transform middleLayer in middleLayers) {
				MoveLayer(middleLayer, middleLayersSpeed);
			}

			foreach (Transform frontLayer in frontLayers) {
				MoveLayer(frontLayer, frontLayersSpeed);
			}
		}
	}

	private void MoveLayer (Transform layer, float speed)
	{
		if (movementController.GetMovingDirection () == Character.MovingDirection.MovingRight) {
			layer.position -= new Vector3 (1 * speed * Time.deltaTime, 0, 0);	
		} else {
			layer.position += new Vector3 (1 * speed * Time.deltaTime, 0, 0);
		}

		CheckIfLayerIsWithinViewport (layer);
	}

	private void CheckIfLayerIsWithinViewport (Transform layer)
	{
		//This is the background moving direction. If the player moves ->, the background should move <-
		Character.MovingDirection movDir = movementController.GetMovingDirection () == Character.MovingDirection.MovingLeft ? Character.MovingDirection.MovingRight : Character.MovingDirection.MovingLeft;
		SpriteRenderer spRenderer = layer.GetComponent<SpriteRenderer> ();

		var dist = (transform.position - Camera.main.transform.position).z;
		float leftBorder = Camera.main.ViewportToWorldPoint (new Vector3 (0, 0, dist)).x;
		float rightBorder = Camera.main.ViewportToWorldPoint (new Vector3 (1, 0, dist)).x;


		// Determine entry and exit border using direction
		Vector3 exitBorder = Vector3.zero;
		Vector3 entryBorder = Vector3.zero;

		if (movDir == Character.MovingDirection.MovingLeft) {
			exitBorder.x = leftBorder;
			entryBorder.x = rightBorder;
		} else {
			exitBorder.x = rightBorder;
			entryBorder.x = leftBorder;
		}

		if (layer != null) {

			if ((movDir == Character.MovingDirection.MovingLeft && (layer.position.x + spRenderer.bounds.size.x/2 < exitBorder.x))) {
				layer.position = new Vector2(entryBorder.x + spRenderer.bounds.size.x/2, layer.position.y);
			}

			if ((movDir == Character.MovingDirection.MovingRight && (layer.position.x - spRenderer.bounds.size.x/2 > exitBorder.x))) {
				layer.position = new Vector2(entryBorder.x - spRenderer.bounds.size.x/2, layer.position.y);
			}
		}
	}

}