using UnityEngine;
using System.Collections;
using System.Collections.Generic;


	
public class CameraFollow : MonoBehaviour
{

	public Transform target;

	[HideInInspector]
	public float minPosition = -5.3f;
	[HideInInspector]
	public float maxPosition = 5.3f;
		
	private float moveSpeed;
	private Player player;

	void Start() {
		player = WorldObjectsHelper.getPlayerGO().GetComponent<Player>();
		moveSpeed = player.MovementSpeed;

		SetupMaxAndMinLimits();
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
//		Transform floor = WorldObjectsHelper.getFloorGO().transform;
//
//		Transform lastSection = floor.GetChild(floor.childCount-1);
//		Transform firstSection = floor.GetChild(0);
//
//		this.maxPosition = lastSection.position.x;
//		this.minPosition = firstSection.position.x;
//
//		float lastSectionSpriteWidth = lastSection.GetComponent<SpriteRenderer>().bounds.size.x;
//		float firstSectionSpriteWidth = firstSection.GetComponent<SpriteRenderer>().bounds.size.x;
//
//		MovementController movCtr = WorldObjectsHelper.getMovementControllerGO().GetComponent<MovementController>();
//		movCtr.movementLimitLeft = this.minPosition - firstSectionSpriteWidth/2 + player.characterSprite.bounds.size.x/2;
//		movCtr.movementLimitRight = this.maxPosition + lastSectionSpriteWidth/2 - player.characterSprite.bounds.size.x/2;
	}

	public bool ReachedLimitPosition() {
		return this.transform.position.x == maxPosition || this.transform.position.x == minPosition;
	}
}


