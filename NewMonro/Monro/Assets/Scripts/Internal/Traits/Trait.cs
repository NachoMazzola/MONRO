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

	public static string toString(TraitType tType) {
		switch (tType) {
		case TraitType.LookAt:
			return "lookAt";
		case TraitType.Pickup:
			return "pickUp";
		case TraitType.Talk:
			return "talkTo";
		case TraitType.Use:
			return "use";
		}

		return "";
	}
}
