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
	public TraitType associatedTraitAction;
	[HideInInspector]
	public GameEntity gameEntity;

	void Awake() {
		this.OnAwake();
	}

	public virtual void OnAwake() {
		this.gameEntity = this.GetComponent<GameEntity>();
	}
}
