using UnityEngine;
using System.Collections;

public class PlayerMouseInput : MonoBehaviour {

	private Vector2 targetPosition;
	private Vector2 xAxisOnlyPosition;
	private bool canMove = false;
	private float yOriginalPos;

	public float PlayerMovementSpeed = 4.0f;

	// Use this for initialization
	void Start () {
		xAxisOnlyPosition = new Vector2();
		yOriginalPos = transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0) || Input.GetMouseButton(0)) {
			targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

			Collider2D hitCollider = Physics2D.OverlapPoint(targetPosition);
			if (hitCollider != null) {
				canMove = hitCollider.gameObject.tag != "InteractiveObject";
			}
			else {
				canMove = true;
			}

		}



		if (canMove) {
			xAxisOnlyPosition.x = targetPosition.x;
			xAxisOnlyPosition.y = yOriginalPos;
			transform.position = Vector2.MoveTowards(transform.position, xAxisOnlyPosition, Time.deltaTime * PlayerMovementSpeed);
		}
			
	}


}
