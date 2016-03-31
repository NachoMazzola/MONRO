using UnityEngine;
using System.Collections;

public class InteractiveObject : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0)) {
			Vector2 targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Collider2D hitCollider = Physics2D.OverlapPoint(targetPosition);
			if (hitCollider != null) {
				InteractiveMenu intMenuComp = this.GetComponent<InteractiveMenu>();
				intMenuComp.ToggleMenu();	
			}
		}

	}
}
