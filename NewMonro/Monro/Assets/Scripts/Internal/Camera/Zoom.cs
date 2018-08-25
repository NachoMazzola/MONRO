using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zoom : MonoBehaviour
{
	public float zoomSpeed = 1;
	public float targetOrtho;
	public float smoothSpeed = 2.0f;
	public float minOrtho = 100.0f;
	public float maxOrtho = 320.0f;

	public bool startZoom = false;

	void Start ()
	{
		targetOrtho = Camera.main.orthographicSize;
	}

	void Update ()
	{
		if (this.startZoom) {
			targetOrtho -= 1 * zoomSpeed;
			targetOrtho = Mathf.Clamp (targetOrtho, minOrtho, maxOrtho);
			Camera.main.orthographicSize = Mathf.MoveTowards (Camera.main.orthographicSize, targetOrtho, smoothSpeed * Time.deltaTime);
			if (targetOrtho == minOrtho || targetOrtho == maxOrtho) {
				this.startZoom = false;
			}
		}
	}
}
