using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenHelpers : MonoBehaviour {

	public static Bounds ScreenBounds() {
		float screenAspect = (float)Screen.width / (float)Screen.height;
		float cameraHeight = Camera.main.orthographicSize * 2;
		return new Bounds(
			Camera.main.transform.position,
			new Vector3(cameraHeight * screenAspect, cameraHeight, 0));
	}
}
