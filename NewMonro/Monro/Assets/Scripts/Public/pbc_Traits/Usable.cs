using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Usable : IMenuRenderableTrait {
	public override void OnAwake () {
		base.OnAwake();
		this.associatedTraitAction = TraitType.Use;
    }
}
