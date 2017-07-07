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
	private float cameraLeftBorder;
	private float cameraRightBorder;

	// Use this for initialization
	void Start ()
	{
		float dist = (transform.position - Camera.main.transform.position).z;
		cameraLeftBorder = Camera.main.ViewportToWorldPoint (new Vector3 (0, 0, dist)).x;
		cameraRightBorder = Camera.main.ViewportToWorldPoint (new Vector3 (1, 0, dist)).x;
	
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
		
	private void MoveLayer (Transform layer, float speed, bool loop = true)
	{
		if (movementController.GetMovingDirection () == Character.MovingDirection.MovingRight) {
			layer.position -= new Vector3 (1 * speed * Time.deltaTime, 0, 0);	
		} else {
			layer.position += new Vector3 (1 * speed * Time.deltaTime, 0, 0);
		}

		CheckIfLayerIsWithinViewport (layer, loop);
	}

	/*
	 * Checks if the layer is withing viewport and moves it to the next section if it is not, so it can
	 * simulate a "continued" effect
	*/
	private void CheckIfLayerIsWithinViewport (Transform layer, bool loop = true)
	{
		//This is the background moving direction. If the player moves ->, the background should move <-
		Character.MovingDirection movDir = movementController.GetMovingDirection () == Character.MovingDirection.MovingLeft ? Character.MovingDirection.MovingRight : Character.MovingDirection.MovingLeft;
		SpriteRenderer spRenderer = layer.GetComponent<SpriteRenderer> ();

		float dist = (transform.position - Camera.main.transform.position).z;
		cameraLeftBorder = Camera.main.ViewportToWorldPoint (new Vector3 (0, 0, dist)).x;
		cameraRightBorder = Camera.main.ViewportToWorldPoint (new Vector3 (1, 0, dist)).x;

		if (layer != null) {

			if (loop) {
				if ((movDir == Character.MovingDirection.MovingLeft && (layer.position.x + spRenderer.bounds.size.x/2 < cameraLeftBorder))) {
					layer.position = new Vector2(cameraRightBorder + spRenderer.bounds.size.x/2, layer.position.y);
				}

				if ((movDir == Character.MovingDirection.MovingRight && (layer.position.x - spRenderer.bounds.size.x/2 > cameraRightBorder))) {
					layer.position = new Vector2(cameraLeftBorder - spRenderer.bounds.size.x/2, layer.position.y);
				}	
			}
		}
	}

}
