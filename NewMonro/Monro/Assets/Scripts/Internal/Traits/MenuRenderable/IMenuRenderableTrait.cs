using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IMenuRenderableTrait: MonoBehaviour {
	[HideInInspector]
	public TraitType associatedTraitAction;

	public Transform MenuIconPrefab;

	[HideInInspector]
	public CommandType AssociatedMenuCommandType;
}
