using UnityEngine;
using System.Collections;

public class InteractiveObject : MonoBehaviour {

	public Transform Item;
	public string Caption;


	void Awake() {
		
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0)) {
			Vector2 targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Collider2D hitCollider = Physics2D.OverlapPoint(targetPosition);
			if (hitCollider != null && hitCollider == this.GetComponent<BoxCollider2D>()) {
				InteractiveMenu intMenuComp = this.GetComponent<InteractiveMenu>();
				intMenuComp.ToggleMenu();	
			}
		}

	}
}
