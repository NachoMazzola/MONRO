using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Tappable))]
public class ItemDroppable : IMenuRenderableTrait {
	public Transform InventroyItem;

	void Awake() {
		this.associatedTraitAction = TraitType.Pickup;
		this.AssociatedMenuCommandType = CommandType.PutItemInInventoryCommandType;
	}
}
