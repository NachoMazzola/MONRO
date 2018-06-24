using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Lookable : IMenuRenderableTrait {
	public string Caption;

	void Awake() {
		this.AssociatedMenuCommandType = CommandType.LookAtCommandType;
	}
}
