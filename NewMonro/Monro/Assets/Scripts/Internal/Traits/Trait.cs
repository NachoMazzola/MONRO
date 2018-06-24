using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TraitType {
	Pickup,
	LookAt,
	Talk,
	Use,
	None
}


public class Trait : MonoBehaviour {
	[HideInInspector]
	public TraitType traitType;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
