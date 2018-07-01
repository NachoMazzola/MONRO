using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Lookable : IMenuRenderableTrait {
	
	public override void OnAwake () {
		base.OnAwake();
		this.associatedTraitAction = TraitType.LookAt;
		this.AssociatedMenuCommandType = CommandType.LookAtCommandType;
	}
}
