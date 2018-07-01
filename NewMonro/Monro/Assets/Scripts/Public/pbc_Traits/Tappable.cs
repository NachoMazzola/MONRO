using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Adds click/tap interaction to a GameObject
*/

//[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(BoxCollider2DSizeFitter))]
public class Tappable : Trait {

//	[HideInInspector]
//	public Rigidbody rigidBody;
	[HideInInspector]
	public BoxCollider2D boxCollider;

	public override void OnAwake () {
		base.OnAwake();
	//	this.rigidBody = GetComponent<Rigidbody>();
	//	this.rigidBody.mass = 0.0f;
		this.boxCollider = GetComponent<BoxCollider2D>();
		this.boxCollider.isTrigger = true;

		//TODO: Set collider frame to match sprite frame
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
