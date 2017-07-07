using UnityEngine;
using System.Collections;
using System.Collections.Generic;


	
public class CameraFollow : MonoBehaviour
{

	public Transform target;

	private float minPosition = -5.3f;
	private float maxPosition = 5.3f;
		
	private float moveSpeed = 1.0f;

	void Start() {
		SetupMaxAndMinLimits();

		Player pl = GameObject.Find("PlayerViking").GetComponent<Player>();
		moveSpeed = pl.MovementSpeed;
	}

	// Update is called once per frame
	void Update ()
	{
		if (target == null) {
			return;
		}
		var newPosition = Vector3.Lerp (transform.position, target.position, moveSpeed * Time.deltaTime);

		newPosition.x = Mathf.Clamp (newPosition.x, minPosition, maxPosition);
		newPosition.y = transform.position.y;
		newPosition.z = transform.position.z;

		transform.position = newPosition;
	}

	private void SetupMaxAndMinLimits() {
		Transform floor = GameObject.Find("Floor").transform;

		Transform lastSection = floor.GetChild(floor.childCount-1);
		Transform firstSection = floor.GetChild(0);

		this.maxPosition = lastSection.position.x;
		this.minPosition = firstSection.position.x;
	}

}


