using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Usable : IMenuRenderableTrait {
	void Awake() {
		this.associatedTraitAction = TraitType.Use;
	}
}
